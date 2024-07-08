using AutoMapper;
using Bank.Application.Common.Exceptions;
using Bank.Application.Common.Interfaces;
using Bank.Core.Models;
using Bank.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Accounts.Create;

public sealed class CreateAccountCommandHandler(
    BankDbContext context,
    IAccountService accountService,
    IMapper mapper) : IRequestHandler<CreateAccountCommand, int>
{
    public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await context.Users
            .Include(u => u.Accounts)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(u => u.UserId == request.UserId,
                cancellationToken);

        if (existingUser is null)
            throw new NotFoundException(nameof(User), request.UserId);

        if (existingUser.Accounts.Count >= 5)
            throw new Exception("Пользователь не может иметь более 5 открытых счетов");

        var newAccount = mapper.Map<Account>(request);

        newAccount.Number = await GenerateUniqueAccountNumber(accountService, context);

        await context.Accounts.AddAsync(newAccount, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newAccount.AccountId;
    }

    private async Task<string> GenerateUniqueAccountNumber(IAccountService accountService, BankDbContext context)
    {
        string number;
        bool isNumberExists;

        do
        {
            number = accountService.GenerateAccountNumber();
            isNumberExists = await context.Accounts
                .AsNoTrackingWithIdentityResolution()
                .AnyAsync(a => a.Number == number);
        } while (isNumberExists);

        return number;
    }
}
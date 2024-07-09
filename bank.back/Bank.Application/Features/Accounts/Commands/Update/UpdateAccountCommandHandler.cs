using Bank.Application.Common.Exceptions;
using Bank.Core.Models;
using Bank.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Accounts.Commands.Update;

public sealed class UpdateAccountCommandHandler(BankDbContext context) : IRequestHandler<UpdateAccountCommand, int>
{
    public async Task<int> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database
            .BeginTransactionAsync(cancellationToken);

        var account = await context.Accounts
            .FindAsync(request.AccountId);

        if (account is null)
            throw new NotFoundException(nameof(Account), request.AccountId);

        account.Name = request.Name;

        if (request.Balance.HasValue)
            account.Balance = request.Balance.Value;

        if (request.CurrencyId is not null &&
            request.CurrencyId != account.CurrencyId)
        {
            var exchangeRate = await context.ExchangeRates
                .AsNoTrackingWithIdentityResolution()
                .OrderBy(r => r.CreatedDate)
                .Where(er => er.CurrencyTo == request.CurrencyId && er.CurrencyFrom == account.CurrencyId)
                .FirstOrDefaultAsync(cancellationToken);

            if (exchangeRate is null)
                throw new NotFoundException(nameof(ExchangeRate), request.CurrencyId.Value);

            var convertedBalance = account.Balance * exchangeRate.Rate;

            account.CurrencyId = request.CurrencyId.Value;
            account.Balance = convertedBalance;
        }

        context.Update(account);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return account.AccountId;
    }
}
using Bank.Application.Common.Exceptions;
using Bank.Core.Models;
using Bank.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Accounts.Update;

public class UpdateAccountCommandHandler(BankDbContext context) : IRequestHandler<UpdateAccountCommand, int>
{
    public async Task<int> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database
            .BeginTransactionAsync(cancellationToken);

        var account = await context.Accounts
            .FindAsync(request.AccountId);

        if (account is null)
            throw new NotFoundException(nameof(Account), request.AccountId);

        if (!string.IsNullOrWhiteSpace(request.Name))
            account.Name = request.Name;

        if (request.CurrencyId is not null)
        {
            if (request.CurrencyId == account.CurrencyId)
                throw new Exception("Валюта уже сконвертирована");

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

        if (request.Balance is not null)
            account.Balance = request.Balance.Value;

        context.Update(account);
        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return account.AccountId;
    }
}
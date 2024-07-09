using Bank.Application.Common.Exceptions;
using Bank.Core.Models;
using Bank.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Transactions.Commands.Execute;

public sealed class ExecuteTransactionCommandHandler(BankDbContext context)
    : IRequestHandler<ExecuteTransactionCommand, int>
{
    public async Task<int> Handle(ExecuteTransactionCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database
            .BeginTransactionAsync(cancellationToken);

        var fromAccount = await context.Accounts
            .FirstOrDefaultAsync(a => a.AccountId == request.FromAccountId,
                cancellationToken);

        if (fromAccount is null)
            throw new NotFoundException(nameof(Account), request.FromAccountId);

        if (fromAccount.Balance < request.Amount)
            throw new Exception("Недостаточно средств");

        var toAccount = await context.Accounts
            .FirstOrDefaultAsync(a => a.Number == request.ToAccountNumber,
                cancellationToken);

        if (toAccount is null)
            throw new Exception($"Счет с номером: {request.ToAccountNumber} не найден");

        fromAccount.Balance -= request.Amount;

        if (fromAccount.CurrencyId == toAccount.CurrencyId)
        {
            toAccount.Balance += request.Amount;
        }
        else
        {
            var exchangeRate = await context.ExchangeRates
                .AsNoTrackingWithIdentityResolution()
                .OrderBy(r => r.CreatedDate)
                .Where(er => er.CurrencyTo == toAccount.CurrencyId &&
                             er.CurrencyFrom == fromAccount.CurrencyId)
                .FirstOrDefaultAsync(cancellationToken);

            if (exchangeRate is null)
                throw new NotFoundException(nameof(ExchangeRate));

            var convertedBalance = request.Amount * exchangeRate.Rate;

            toAccount.Balance = convertedBalance;
        }

        context.Update(fromAccount);
        context.Update(toAccount);

        var action = new Transaction
        {
            Amount = request.Amount,
            FromAccountId = fromAccount.AccountId,
            ToAccountId = toAccount.AccountId,
            FromCurrencyId = fromAccount.CurrencyId,
            ToCurrencyId = toAccount.CurrencyId
        };

        await context.Transactions.AddAsync(action,
            cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return action.TransactionId;
    }
}
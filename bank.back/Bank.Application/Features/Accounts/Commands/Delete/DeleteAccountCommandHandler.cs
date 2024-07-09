using Bank.Application.Common.Exceptions;
using Bank.Core.Models;
using Bank.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Accounts.Commands.Delete;

public class DeleteAccountCommandHandler(BankDbContext context) : IRequestHandler<DeleteAccountCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await context.Accounts
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(u => u.AccountId == request.AccountId,
                cancellationToken);

        if (account is null)
            throw new NotFoundException(nameof(Account), request.AccountId);

        context.Accounts.Remove(account);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
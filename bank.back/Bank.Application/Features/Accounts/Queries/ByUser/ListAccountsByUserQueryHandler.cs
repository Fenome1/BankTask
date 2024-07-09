using AutoMapper;
using Bank.Application.Common.Exceptions;
using Bank.Application.ViewModels;
using Bank.Core.Models;
using Bank.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Accounts.Queries.ByUser;

public sealed class ListAccountsByUserQueryHandler(BankDbContext context, IMapper mapper)
    : IRequestHandler<ListAccountsByUserQuery, List<AccountViewModel>>
{
    public async Task<List<AccountViewModel>> Handle(ListAccountsByUserQuery request,
        CancellationToken cancellationToken)
    {
        var isUserExist = await context.Users
            .AnyAsync(u => u.UserId == request.UserId,
                cancellationToken);

        if (!isUserExist)
            throw new NotFoundException(nameof(User), request.UserId);

        return await context.Accounts
            .Include(a => a.Currency)
            .Include(a => a.Owner)
            .AsNoTrackingWithIdentityResolution()
            .Where(a => a.OwnerId == request.UserId)
            .OrderBy(a => a.AccountId)
            .Select(a => mapper.Map<AccountViewModel>(a))
            .ToListAsync(cancellationToken);
    }
}
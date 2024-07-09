using AutoMapper;
using Bank.Application.ViewModels;
using Bank.Application.ViewModels.Base;
using Bank.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Transactions.Queries.ByUser;

public sealed class ListTransactionsByUserQueryHandler(BankDbContext context, IMapper mapper)
    : IRequestHandler<ListTransactionsByUserQuery, PagedList<TransactionViewModel>>
{
    public async Task<PagedList<TransactionViewModel>> Handle(ListTransactionsByUserQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Transactions
            .Include(t => t.ToAccount)
            .ThenInclude(a => a.Owner)
            .Include(t => t.FromAccount)
            .ThenInclude(a => a.Owner)
            .Include(t => t.FromCurrency)
            .Include(t => t.ToCurrency)
            .Where(t => t.FromAccount.OwnerId == request.UserId || t.ToAccount.OwnerId == request.UserId)
            .AsQueryable();

        if (request.StartDate.HasValue)
            query = query.Where(t => t.TransferDate >= request.StartDate.Value);

        if (request.EndDate.HasValue)
            query = query.Where(t => t.TransferDate <= request.EndDate.Value);

        if (request.CurrencyId.HasValue)
            query = query.Where(t => t.FromCurrency.CurrencyId == request.CurrencyId.Value);

        if (request.AccountId.HasValue)
            query = query.Where(t => t.FromAccountId == request.AccountId.Value
                                     || t.ToAccountId == request.AccountId.Value);

        var paginatedTransactions = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(t => mapper.Map<TransactionViewModel>(t))
            .ToListAsync(cancellationToken);

        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<TransactionViewModel>
        {
            PageSize = request.PageSize,
            CurrentPage = request.Page,
            TotalCount = totalCount,
            Items = paginatedTransactions
        };
    }
}
using Bank.Application.ViewModels;
using Bank.Application.ViewModels.Base;
using MediatR;

namespace Bank.Application.Features.Transactions.Queries.ByUser;

public sealed record ListTransactionsByUserQuery : IRequest<PagedList<TransactionViewModel>>
{
    public required int UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? CurrencyId { get; set; }
    public int? AccountId { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
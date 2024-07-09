using MediatR;

namespace Bank.Application.Features.Transactions.Commands.Execute;

public sealed record ExecuteTransactionCommand : IRequest<int>
{
    public required int FromAccountId { get; set; }
    public required string ToAccountNumber { get; set; }
    public required decimal Amount { get; set; }
}
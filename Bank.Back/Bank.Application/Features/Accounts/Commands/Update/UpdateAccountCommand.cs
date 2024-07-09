using MediatR;

namespace Bank.Application.Features.Accounts.Commands.Update;

public sealed record UpdateAccountCommand : IRequest<int>
{
    public required int AccountId { get; set; }
    public string? Name { get; set; }
    public int? CurrencyId { get; set; }
    public decimal? Balance { get; set; }
}
using MediatR;

namespace Bank.Application.Features.Accounts.Delete;

public sealed record DeleteAccountCommand : IRequest<Unit>
{
    public required int AccountId { get; set; }
}
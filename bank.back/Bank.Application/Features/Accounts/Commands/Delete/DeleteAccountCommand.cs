using MediatR;

namespace Bank.Application.Features.Accounts.Commands.Delete;

public sealed record DeleteAccountCommand(int AccountId) : IRequest<Unit>;
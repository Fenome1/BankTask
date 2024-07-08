using Bank.Application.ViewModels;
using MediatR;

namespace Bank.Application.Features.Accounts.Queries.ByUser;

public sealed record ListAccountsByUserQuery(int UserId) : IRequest<List<AccountViewModel>>;
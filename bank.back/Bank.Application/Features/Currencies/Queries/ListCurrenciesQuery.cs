using Bank.Application.ViewModels;
using MediatR;

namespace Bank.Application.Features.Currencies.Queries;

public sealed record ListCurrenciesQuery : IRequest<List<CurrencyViewModel>>;
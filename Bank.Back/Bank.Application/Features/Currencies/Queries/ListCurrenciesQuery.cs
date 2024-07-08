using Bank.Application.ViewModels;
using MediatR;

namespace Bank.Application.Features.Currencies.Queries;

public class ListCurrenciesQuery : IRequest<List<CurrencyViewModel>>;
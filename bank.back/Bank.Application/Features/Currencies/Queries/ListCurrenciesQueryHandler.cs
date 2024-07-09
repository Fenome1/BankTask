using AutoMapper;
using Bank.Application.ViewModels;
using Bank.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Currencies.Queries;

public sealed class ListCurrenciesQueryHandler(BankDbContext context, IMapper mapper)
    : IRequestHandler<ListCurrenciesQuery, List<CurrencyViewModel>>
{
    public async Task<List<CurrencyViewModel>> Handle(ListCurrenciesQuery request, CancellationToken cancellationToken)
    {
        return await context.Currencies
            .AsNoTrackingWithIdentityResolution()
            .Select(c => mapper.Map<CurrencyViewModel>(c))
            .ToListAsync(cancellationToken);
    }
}
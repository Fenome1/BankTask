using Bank.Application.Common.Mappings;
using Bank.Core.Models;

namespace Bank.Application.ViewModels;

public sealed record CurrencyViewModel : IMapWith<Currency>
{
    public required int CurrencyId { get; set; }

    public required string Code { get; set; }

    public required string Name { get; set; }
}
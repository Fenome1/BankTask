using Bank.Application.Common.Mappings;
using Bank.Core.Models;

namespace Bank.Application.ViewModels;

public sealed record AccountViewModel : IMapWith<Account>
{
    public required int AccountId { get; set; }

    public required UserViewModel Owner { get; set; }

    public required string Number { get; set; }

    public string? Name { get; set; }

    public required decimal Balance { get; set; }

    public required CurrencyViewModel Currency { get; set; }
}
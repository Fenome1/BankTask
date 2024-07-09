using Bank.Application.Common.Mappings;
using Bank.Core.Models;

namespace Bank.Application.ViewModels;

public sealed record TransactionViewModel : IMapWith<Transaction>
{
    public required int TransactionId { get; set; }

    public required decimal Amount { get; set; }

    public required AccountViewModel FromAccount { get; set; }

    public required AccountViewModel ToAccount { get; set; }

    public required DateTime TransferDate { get; set; }

    public required CurrencyViewModel FromCurrency { get; set; }

    public required CurrencyViewModel ToCurrency { get; set; }
}
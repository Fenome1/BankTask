namespace Bank.Core.Models;

public class Transaction
{
    public int TransactionId { get; set; }

    public decimal Amount { get; set; }

    public int FromAccountId { get; set; }

    public int ToAccountId { get; set; }

    public DateTime TransferDate { get; set; }

    public int FromCurrencyId { get; set; }

    public int ToCurrencyId { get; set; }

    public virtual Account FromAccount { get; set; } = null!;

    public virtual Currency FromCurrency { get; set; } = null!;

    public virtual Account ToAccount { get; set; } = null!;

    public virtual Currency ToCurrency { get; set; } = null!;
}
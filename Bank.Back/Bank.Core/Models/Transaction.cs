namespace Bank.Core.Models;

public class Transaction
{
    public int TransactionId { get; set; }

    public decimal Amount { get; set; }

    public int CurrencyId { get; set; }

    public int FromAccountId { get; set; }

    public int ToAccountId { get; set; }

    public DateTime TransferDate { get; set; }

    public virtual Account FromAccount { get; set; } = null!;

    public virtual Account ToAccount { get; set; } = null!;
}
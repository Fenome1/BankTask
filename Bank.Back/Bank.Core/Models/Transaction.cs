namespace Bank.Core.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public decimal Amount { get; set; }

    public int CurrencyTypeId { get; set; }

    public int FromAccountId { get; set; }

    public int ToAccountId { get; set; }

    public DateTime TransferDate { get; set; }

    public virtual CurrencyType CurrencyType { get; set; } = null!;

    public virtual PersonalAccount FromAccount { get; set; } = null!;

    public virtual PersonalAccount ToAccount { get; set; } = null!;
}

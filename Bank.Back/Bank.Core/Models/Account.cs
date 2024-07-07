namespace Bank.Core.Models;

public class Account
{
    public int PersonalAccountId { get; set; }

    public int OwnerId { get; set; }

    public string Number { get; set; } = null!;

    public string? Name { get; set; }

    public decimal Balance { get; set; }

    public int CurrencyId { get; set; }

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Transaction> TransactionFromAccounts { get; set; } = new List<Transaction>();

    public virtual ICollection<Transaction> TransactionToAccounts { get; set; } = new List<Transaction>();
}
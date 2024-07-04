namespace Bank.Core.Models;

public partial class CurrencyType
{
    public int CurrencyTypeId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual ICollection<PersonalAccount> PersonalAccounts { get; set; } = new List<PersonalAccount>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

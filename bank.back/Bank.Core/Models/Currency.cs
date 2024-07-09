namespace Bank.Core.Models;

public class Currency
{
    public int CurrencyId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<ExchangeRate> ExchangeRateCurrencyFromNavigations { get; set; } =
        new List<ExchangeRate>();

    public virtual ICollection<ExchangeRate> ExchangeRateCurrencyToNavigations { get; set; } = new List<ExchangeRate>();

    public virtual ICollection<Transaction> TransactionFromCurrencies { get; set; } = new List<Transaction>();

    public virtual ICollection<Transaction> TransactionToCurrencies { get; set; } = new List<Transaction>();
}
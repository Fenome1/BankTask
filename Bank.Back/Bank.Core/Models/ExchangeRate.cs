namespace Bank.Core.Models;

public class ExchangeRate
{
    public int ExchangeRateId { get; set; }

    public int CurrencyFrom { get; set; }

    public decimal Rate { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CurrencyTo { get; set; }

    public virtual Currency CurrencyFromNavigation { get; set; } = null!;

    public virtual Currency CurrencyToNavigation { get; set; } = null!;
}
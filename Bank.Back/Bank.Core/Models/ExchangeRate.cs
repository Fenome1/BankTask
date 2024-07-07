namespace Bank.Core.Models;

public class ExchangeRate
{
    public int ExchangeRateId { get; set; }

    public int CurrencyId { get; set; }

    public decimal Rate { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Currency Currency { get; set; } = null!;
}
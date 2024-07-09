using Bank.Application.Common.Interfaces;

namespace Bank.Application.Services;

public class AccountService : IAccountService
{
    public string GenerateAccountNumber()
    {
        var random = new Random();
        return new string(Enumerable
            .Range(0, 20)
            .Select(_ => random.Next(0, 10).ToString()[0])
            .ToArray());
    }
}
namespace Bank.Application.Common.Interfaces;

public interface IPasswordHasher
{
    public string Hash(string password);

    public bool Check(string password, string hash);
}
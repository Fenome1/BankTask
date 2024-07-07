namespace Bank.Application.Common.Jwt;

public sealed record JwtOptions
{
    public string SecretKey { get; set; } = string.Empty;
    public int ExpiresMinutes { get; set; }
}
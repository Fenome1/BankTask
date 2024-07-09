using System.Security.Claims;
using Bank.Core.Models;

namespace Bank.Application.Common.Interfaces;

public interface ITokenService
{
    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);

    string GenerateAccessToken(User user);

    RefreshToken GenerateRefreshToken(int userId);
}
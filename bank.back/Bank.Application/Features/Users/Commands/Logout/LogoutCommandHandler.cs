using System.Security.Claims;
using AutoMapper;
using Bank.Application.Common.Exceptions;
using Bank.Application.Common.Interfaces;
using Bank.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Users.Commands.Logout;

public sealed class LogoutCommandHandler(
    BankDbContext context,
    ITokenService tokenService,
    IMapper mapper) : IRequestHandler<LogoutCommand, bool>
{
    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.AccessToken) || string.IsNullOrWhiteSpace(request.RefreshToken))
            throw new NotFoundException(nameof(request));

        var principal = tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        var nameIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

        if (nameIdClaim is null || !int.TryParse(nameIdClaim.Value, out var userId))
            throw new NotFoundException(nameof(nameIdClaim));

        var user = await context.Users
            .Include(u => u.RefreshToken)
            .FirstOrDefaultAsync(u => u.UserId == userId,
                cancellationToken);

        if (user is null)
            throw new NotFoundException(nameof(user));

        if (user.RefreshToken is null)
            throw new NotFoundException(nameof(user.RefreshToken));

        context.RefreshTokens.Remove(user.RefreshToken);

        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}
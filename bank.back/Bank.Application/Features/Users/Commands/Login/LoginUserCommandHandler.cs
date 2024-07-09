using AutoMapper;
using Bank.Application.Common.Interfaces;
using Bank.Application.ViewModels;
using Bank.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Users.Commands.Login;

public sealed class LoginUserCommandHandler(
    BankDbContext context,
    IMapper mapper,
    IPasswordHasher passwordHasher,
    ITokenService tokenService)
    : IRequestHandler<LoginUserCommand, AuthResultViewModel>
{
    public async Task<AuthResultViewModel> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Include(u => u.RefreshToken)
            .FirstOrDefaultAsync(u => u.Login == command.Login,
                cancellationToken);

        if (user is null)
            throw new Exception("Не верный логин или пароль");

        if (!passwordHasher.Check(command.Password, user.Password))
            throw new Exception("Не верный логин или пароль");

        var token = tokenService.GenerateAccessToken(user);
        var refreshToken = tokenService.GenerateRefreshToken(user.UserId);

        await context.AddAsync(refreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var userViewModel = mapper.Map<UserViewModel>(user);

        return new AuthResultViewModel
        {
            AccessToken = token,
            RefreshToken = user.RefreshToken!.Token,
            User = userViewModel
        };
    }
}
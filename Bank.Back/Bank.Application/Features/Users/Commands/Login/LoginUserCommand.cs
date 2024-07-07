using Bank.Application.ViewModels;
using MediatR;

namespace Bank.Application.Features.Users.Commands.Login;

public record LoginUserCommand : IRequest<AuthResultViewModel>
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}
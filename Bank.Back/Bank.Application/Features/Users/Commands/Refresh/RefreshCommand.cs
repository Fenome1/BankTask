using Bank.Application.ViewModels;
using MediatR;

namespace Bank.Application.Features.Users.Commands.Refresh;

public record RefreshCommand : IRequest<AuthResultViewModel>
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
using Bank.Application.Common.Mappings;
using Bank.Core.Models;
using MediatR;

namespace Bank.Application.Features.Users.Commands.Create;

public record CreateUserCommand : IRequest<int>, IMapWith<User>
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}
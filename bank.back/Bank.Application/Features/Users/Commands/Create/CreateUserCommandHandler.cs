using AutoMapper;
using Bank.Application.Common.Interfaces;
using Bank.Core.Models;
using Bank.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Application.Features.Users.Commands.Create;

public sealed class CreateUserCommandHandler(
    BankDbContext context,
    IMapper mapper,
    IPasswordHasher passwordHasher,
    IMediator mediator) : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isLoginExist = await context.Users
            .AnyAsync(u => u.Login == request.Login,
                cancellationToken);

        if (isLoginExist)
            throw new Exception("Пользователь с таким логином уже существует");

        var user = mapper.Map<User>(request);

        user.Password = passwordHasher.Hash(request.Password);

        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return user.UserId;
    }
}
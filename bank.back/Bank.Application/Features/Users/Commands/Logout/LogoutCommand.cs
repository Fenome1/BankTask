﻿using MediatR;

namespace Bank.Application.Features.Users.Commands.Logout;

public sealed record LogoutCommand : IRequest<bool>
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
using Bank.Application.Common.Exceptions;
using Bank.Application.Features.Users.Commands.Login;
using Bank.Application.Features.Users.Commands.Logout;
using Bank.Application.Features.Users.Commands.Refresh;
using Bank.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Back.Controllers;

public class AuthController : BaseController
{
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<UserViewModel>> Login([FromBody] LoginUserCommand command)
    {
        try
        {
            return Ok(await Mediator.Send(command));
        }
        catch (Exception e)
        {
            return BadRequest($"{e.Message}");
        }
    }

    [AllowAnonymous]
    [HttpPost("Refresh")]
    public async Task<ActionResult<AuthResultViewModel>> Refresh(RefreshCommand command)
    {
        try
        {
            return await Mediator.Send(command);
        }
        catch (NotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
    }

    [AllowAnonymous]
    [HttpPut("Logout")]
    public async Task<IActionResult> Logout(LogoutCommand command)
    {
        try
        {
            return Ok(await Mediator.Send(command));
        }
        catch
        {
            return BadRequest("Ошибка при выходе из аккаунта");
        }
    }
}
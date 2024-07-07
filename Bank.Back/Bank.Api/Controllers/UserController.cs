using Bank.Application.Features.Users.Commands.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Back.Controllers;

public class UserController : BaseController
{
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ActionResult<int>> Register([FromBody] CreateUserCommand command)
    {
        try
        {
            return Created(string.Empty, await Mediator.Send(command));
        }
        catch (Exception e)
        {
            return BadRequest($"{e.Message}");
        }
    }
}
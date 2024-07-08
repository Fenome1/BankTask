using Bank.Application.Features.Accounts.Commands.Create;
using Bank.Application.Features.Accounts.Commands.Delete;
using Bank.Application.Features.Accounts.Commands.Update;
using Bank.Application.Features.Accounts.Queries.ByUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Back.Controllers;

public class AccountController : BaseController
{
    [Authorize]
    [HttpPost("Create")]
    public async Task<ActionResult<int>> Create([FromBody] CreateAccountCommand command)
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

    [Authorize]
    [HttpPut("Update")]
    public async Task<ActionResult<int>> Update([FromBody] UpdateAccountCommand command)
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

    [Authorize]
    [HttpDelete("Delete/{accountId}")]
    public async Task<ActionResult<int>> Delete(int accountId)
    {
        try
        {
            return Ok(await Mediator.Send(new DeleteAccountCommand(accountId)));
        }
        catch (Exception e)
        {
            return BadRequest($"{e.Message}");
        }
    }

    [Authorize]
    [HttpGet("User/{userId}")]
    public async Task<ActionResult<int>> GetByUser(int userId)
    {
        try
        {
            return Ok(await Mediator.Send(new ListAccountsByUserQuery(userId)));
        }
        catch (Exception e)
        {
            return BadRequest($"{e.Message}");
        }
    }
}
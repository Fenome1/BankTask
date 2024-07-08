using Bank.Application.Features.Accounts.Create;
using Bank.Application.Features.Accounts.Delete;
using Bank.Application.Features.Accounts.Queries.ByUser;
using Bank.Application.Features.Accounts.Update;
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
    [HttpDelete("Delete")]
    public async Task<ActionResult<int>> Delete([FromBody] DeleteAccountCommand command)
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

    /*[Authorize]*/
    [HttpGet("User/")]
    public async Task<ActionResult<int>> GetByUser([FromQuery] ListAccountsByUserQuery query)
    {
        try
        {
            return Ok(await Mediator.Send(query));
        }
        catch (Exception e)
        {
            return BadRequest($"{e.Message}");
        }
    }
}
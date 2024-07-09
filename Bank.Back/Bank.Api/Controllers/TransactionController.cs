using Bank.Application.Features.Transactions.Commands.Execute;
using Bank.Application.Features.Transactions.Queries.ByUser;
using Bank.Application.ViewModels;
using Bank.Application.ViewModels.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Back.Controllers;

public class TransactionController : BaseController
{
    [Authorize]
    [HttpPost("Execute")]
    public async Task<ActionResult<int>> Execute([FromBody] ExecuteTransactionCommand command)
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

    /*[Authorize]*/
    [HttpGet("User")]
    public async Task<ActionResult<PagedList<TransactionViewModel>>> GetByUser(
        [FromQuery] ListTransactionsByUserQuery query)
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
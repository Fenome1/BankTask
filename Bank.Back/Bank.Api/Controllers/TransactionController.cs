using Bank.Application.Features.Transactions.Execute;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Back.Controllers;

public class TransactionController : BaseController
{
    /*[Authorize]*/
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
}
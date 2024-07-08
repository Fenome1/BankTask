using Bank.Application.Features.Currencies.Queries;
using Bank.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Back.Controllers;

public class CurrencyController : BaseController
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<CurrencyViewModel>>> Get()
    {
        try
        {
            return Ok(await Mediator.Send(new ListCurrenciesQuery()));
        }
        catch (Exception e)
        {
            return BadRequest($"{e.Message}");
        }
    }
}
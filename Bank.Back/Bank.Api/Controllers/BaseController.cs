using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Back.Controllers;

[ApiController]
[EnableCors("CORS")]
[Route("/Api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
}
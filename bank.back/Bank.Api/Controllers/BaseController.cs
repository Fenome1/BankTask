using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Back.Controllers;

[ApiController]
[Route("/Api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
}
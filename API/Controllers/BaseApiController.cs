using Application.Common.Core;
using Application.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator 
        => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        return result.ReturnType switch
        {
            ReturnTypes.Ok => Ok(result.Data),
            ReturnTypes.NotFound => NotFound(result.Message),
            ReturnTypes.Unauthorized => Unauthorized(result.Message),
            ReturnTypes.Forbidden => Forbid(result.Message),
            ReturnTypes.BadRequest => BadRequest(result.Message),
            ReturnTypes.NoContent => NoContent(),
            _ => StatusCode(500, result.Message)
        };
    }
}
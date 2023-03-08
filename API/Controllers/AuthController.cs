using System.Security.Claims;
using Application.Auth.Commands;
using Application.Auth.Queries;
using Application.Common.DTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[AllowAnonymous]
public class AuthController : BaseApiController
{
    [HttpPost("Login")]
    public async Task<ActionResult<AuthUserDto>> Login([FromBody] LoginDto loginDto, CancellationToken token)
    {
        var res = await Mediator.Send(new LoginCommand(LoginDto: loginDto), token);
        return HandleResult(res);
    }

    [HttpPost("Register")]
    public async Task<ActionResult<AuthUserDto>> Register([FromBody] RegisterDto registerDto, CancellationToken token)
    {
        var res = await Mediator.Send(new RegisterCommand(RegisterDto: registerDto), token);
        return HandleResult(res);
    }

    [Authorize]
    [HttpGet("GetCurrentUser")]
    public async Task<ActionResult<AuthUserDto>> GetCurrentUser(CancellationToken token)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var res = await Mediator.Send(new GetCurrentUserQuery(Email: email), token);
        return HandleResult(res);
    }
}
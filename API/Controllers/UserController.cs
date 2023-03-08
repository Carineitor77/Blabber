using Application.Common.DTO.User;
using Application.User.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController : BaseApiController
{
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers(CancellationToken token)
    {
        var res = await Mediator.Send(new GetAllUsersQuery(), token);
        return HandleResult(res);
    }

    [HttpGet("GetUserById")]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id, CancellationToken token)
    {
        var res = await Mediator.Send(new GetUserByIdQuery(Id: id), token);
        return HandleResult(res);
    }
}
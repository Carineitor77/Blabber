using Application.Common.DTO.Profile;
using Application.Profile.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfileController : BaseApiController
{
    [HttpGet("GetProfileInfo")]
    public async Task<ActionResult<ProfileDto>> GetProfileInfo(CancellationToken token)
    {
        var res = await Mediator.Send(new GetProfileInfoQuery(), token);
        return HandleResult(res);
    }
}
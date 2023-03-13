using Application.Common.DTO.Profile;
using Application.Profile.Commands;
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

    [HttpPost("EditProfile")]
    public async Task<ActionResult<ProfileDto>> EditProfile(EditProfileDto editProfileDto, CancellationToken token)
    {
        var res = await Mediator.Send(new EditProfileCommand(EditProfile: editProfileDto), token);
        return HandleResult(res);
    }
}
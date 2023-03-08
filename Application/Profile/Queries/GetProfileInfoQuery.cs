using Application.Common.Core;
using Application.Common.DTO.Profile;
using Application.Common.Enums;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profile.Queries;

public record GetProfileInfoQuery : IRequest<Result<ProfileDto>>;

public class GetProfileInfoQueryHandler : IRequestHandler<GetProfileInfoQuery, Result<ProfileDto>>
{
    private readonly BlabberContext _context;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public GetProfileInfoQueryHandler(BlabberContext context, IMapper mapper, IUserService userService)
    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
    }
    
    public async Task<Result<ProfileDto>> Handle(GetProfileInfoQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetUserId();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString(), cancellationToken);

        if (user is null)
        {
            return Result<ProfileDto>.Return(ReturnTypes.NotFound, message: "User not found");
        }

        var profileDto = _mapper.Map<ProfileDto>(user);

        return Result<ProfileDto>.Return(ReturnTypes.Ok, profileDto);
    }
}
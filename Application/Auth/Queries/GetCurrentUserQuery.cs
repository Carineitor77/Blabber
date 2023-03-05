using Application.Common.Core;
using Application.Common.DTO;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Queries;

public record GetCurrentUserQuery(string Email) : IRequest<Result<UserDto>>;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Result<UserDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public GetCurrentUserQueryHandler(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    
    public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        if (request.Email is null)
        {
            return Result<UserDto>.Return(ReturnTypes.BadRequest, message: "Failed to get a current user");
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        
        var token = _tokenService.CreateToken(user);

        var userDto = new UserDto(Token: token);

        return Result<UserDto>.Return(ReturnTypes.Ok, userDto);
    }
}
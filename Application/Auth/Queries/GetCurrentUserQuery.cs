using Application.Common.Core;
using Application.Common.DTO.Auth;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Queries;

public record GetCurrentUserQuery(string Email) : IRequest<Result<AuthUserDto>>;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Result<AuthUserDto>>
{
    private readonly UserManager<AuthUser> _userManager;
    private readonly ITokenService _tokenService;

    public GetCurrentUserQueryHandler(UserManager<AuthUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    
    public async Task<Result<AuthUserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        if (request.Email is null)
        {
            return Result<AuthUserDto>.Return(ReturnTypes.BadRequest, message: "Failed to get a current user");
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        
        var token = _tokenService.CreateToken(user);

        var userDto = new AuthUserDto(Token: token);

        return Result<AuthUserDto>.Return(ReturnTypes.Ok, userDto);
    }
}
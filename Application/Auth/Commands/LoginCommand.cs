using Application.Auth.Validators;
using Application.Common.Core;
using Application.Common.DTO.Auth;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands;

public record LoginCommand(LoginDto LoginDto) : IRequest<Result<AuthUserDto>>;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(l => l.LoginDto).SetValidator(new LoginValidator());
    }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthUserDto>>
{
    private readonly UserManager<AuthUser> _userManager;
    private readonly SignInManager<AuthUser> _signInManager;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    
    public async Task<Result<AuthUserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);

        if (user is null)
        {
            return Result<AuthUserDto>.Return(ReturnTypes.Unauthorized, message: "User not found");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, false);

        var token = _tokenService.CreateToken(user);

        var userDto = new AuthUserDto(Token: token);

        return result.Succeeded
            ? Result<AuthUserDto>.Return(ReturnTypes.Ok, userDto)
            : Result<AuthUserDto>.Return(ReturnTypes.BadRequest, message: "Failed to login");
    }
}
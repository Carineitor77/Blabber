using Application.Auth.Validators;
using Application.Common.Core;
using Application.Common.DTO;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands;

public record LoginCommand(LoginDto LoginDto) : IRequest<Result<UserDto>>;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(l => l.LoginDto).SetValidator(new LoginValidator());
    }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<UserDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    
    public async Task<Result<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.LoginDto.Email);

        if (user is null)
        {
            return Result<UserDto>.Return(ReturnTypes.Unauthorized, message: "User not found");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, false);

        var token = _tokenService.CreateToken(user);

        var userDto = new UserDto(Token: token);

        return result.Succeeded
            ? Result<UserDto>.Return(ReturnTypes.Ok, userDto)
            : Result<UserDto>.Return(ReturnTypes.BadRequest, message: "Failed to login");
    }
}
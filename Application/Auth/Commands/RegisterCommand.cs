using Application.Auth.Validators;
using Application.Common.Core;
using Application.Common.DTO;
using Application.Common.Enums;
using Application.Common.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Auth.Commands;

public record RegisterCommand(RegisterDto RegisterDto) : IRequest<Result<UserDto>>;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(r => r.RegisterDto).SetValidator(new RegisterValidator());
    }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<UserDto>>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(UserManager<User> userManager, ITokenService tokenService, 
        IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }
    
    public async Task<Result<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isEmailTaken = await _userManager.Users
            .AnyAsync(u => u.Email == request.RegisterDto.Email, cancellationToken);

        if (isEmailTaken)
        {
            return Result<UserDto>.Return(ReturnTypes.BadRequest, message: "Email taken");
        }

        var isUserNameTaken = await _userManager.Users
            .AnyAsync(u => u.UserName == request.RegisterDto.UserName, cancellationToken);

        if (isUserNameTaken)
        {
            return Result<UserDto>.Return(ReturnTypes.BadRequest, message: "Username taken");
        }

        var user = _mapper.Map<User>(request.RegisterDto);
        
        var token = _tokenService.CreateToken(user);

        var result = await _userManager.CreateAsync(user, request.RegisterDto.Password);

        var userDto = new UserDto(Token: token);

        return result.Succeeded
            ? Result<UserDto>.Return(ReturnTypes.Ok, userDto)
            : Result<UserDto>.Return(ReturnTypes.BadRequest, message: "Failed to register a new user");
    }
}
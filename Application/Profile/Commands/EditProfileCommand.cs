using Application.Common.Core;
using Application.Common.DTO.Profile;
using Application.Common.Enums;
using Application.Common.Interfaces;
using Application.Profile.Validators;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profile.Commands;

public record EditProfileCommand(EditProfileDto EditProfile) : IRequest<Result<ProfileDto>>;

public class EditProfileCommandValidator : AbstractValidator<EditProfileCommand>
{
    public EditProfileCommandValidator()
    {
        RuleFor(e => e.EditProfile).SetValidator(new EditProfileValidator());
    }
}

public class EditProfileCommandHandler : IRequestHandler<EditProfileCommand, Result<ProfileDto>>
{
    private readonly BlabberContext _context;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public EditProfileCommandHandler(BlabberContext context, IUserService userService, IMapper mapper)
    {
        _context = context;
        _userService = userService;
        _mapper = mapper;
    }
    
    public async Task<Result<ProfileDto>> Handle(EditProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetUserId();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString(), cancellationToken);
        
        if (user is null)
        {
            return Result<ProfileDto>.Return(ReturnTypes.NotFound, message: "User not found");
        }

        _mapper.Map(request.EditProfile, user);

        var profileDto = _mapper.Map<ProfileDto>(user);
        
        var isSuccess = await _context.SaveChangesAsync(cancellationToken) > 0;

        return isSuccess
            ? Result<ProfileDto>.Return(ReturnTypes.Ok, profileDto)
            : Result<ProfileDto>.Return(ReturnTypes.BadRequest, message: "Failed to edit a profile");
    }
}
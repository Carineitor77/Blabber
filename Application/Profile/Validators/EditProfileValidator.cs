using Application.Common.DTO.Profile;
using FluentValidation;

namespace Application.Profile.Validators;

public class EditProfileValidator : AbstractValidator<EditProfileDto>
{
    public EditProfileValidator()
    {
        RuleFor(e => e.Bio).MaximumLength(200);
        RuleFor(e => e.FirstName).MaximumLength(50);
        RuleFor(e => e.SecondName).MaximumLength(50);
        RuleFor(e => e.Age).GreaterThan(17).LessThan(100);
    }
}
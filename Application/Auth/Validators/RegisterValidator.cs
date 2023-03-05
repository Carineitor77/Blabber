using Application.Common.DTO;
using FluentValidation;

namespace Application.Auth.Validators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(r => r.UserName).NotEmpty().MaximumLength(50);
        RuleFor(r => r.Email).NotEmpty().EmailAddress().MaximumLength(50);
        RuleFor(r => r.Password).NotEmpty();
        RuleFor(r => r.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(r => r.SecondName).NotEmpty().MaximumLength(50);
        RuleFor(r => r.Age).NotEmpty().GreaterThan(17).LessThan(100);
    }
}
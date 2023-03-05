using Application.Common.DTO;
using FluentValidation;

namespace Application.Auth.Validators;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(l => l.Email).NotEmpty().EmailAddress().MaximumLength(50);
        RuleFor(l => l.Password).NotEmpty();
    }
}
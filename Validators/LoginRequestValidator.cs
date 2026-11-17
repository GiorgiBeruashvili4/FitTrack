using FitTrack.DTOs.Auth;
using FluentValidation;

namespace FitTrack.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is in wrong format.");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}

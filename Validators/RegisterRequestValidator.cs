using FitTrack.DTOs.Auth;
using FluentValidation;

namespace FitTrack.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(r => r.Email)
           .NotEmpty().WithMessage("Email is required.")
           .EmailAddress().WithMessage("Email is in wrong format.")
           .MaximumLength(200).WithMessage("Email must contain at most 200 characters.");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must contain at least 8 characters.")
            .MaximumLength(72).WithMessage("Password must contain at most 72 characters.")
            .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Password must contain at least one number.")
            .Matches(@"[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(r => r.FullName)
            .NotEmpty().WithMessage("FullName is required.")
            .MaximumLength(100).WithMessage("FullName must contain at most 100 characters.");
    }
}

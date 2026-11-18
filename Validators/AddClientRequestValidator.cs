using FitTrack.DTOs.Trainer;
using FluentValidation;

namespace FitTrack.Validators;

public class AddClientRequestValidator : AbstractValidator<AddClientRequest>
{
    public AddClientRequestValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is in wrong format.")
            .MaximumLength(200).WithMessage("Email must contain at most 200 characters.");
    }
}

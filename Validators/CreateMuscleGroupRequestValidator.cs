using FitTrack.DTOs.MuscleGroups;
using FluentValidation;

namespace FitTrack.Validators;

public class CreateMuscleGroupRequestValidator : AbstractValidator<CreateMuscleGroupRequest>
{
    public CreateMuscleGroupRequestValidator()
    {
        RuleFor(mg => mg.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must contain at least 2 characters.")
            .MaximumLength(50).WithMessage("Name must contain at most 50 characters.");
    }
}

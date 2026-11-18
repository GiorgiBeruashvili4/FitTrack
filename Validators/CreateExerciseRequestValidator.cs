using FitTrack.DTOs.Exercises;
using FluentValidation;

namespace FitTrack.Validators;

public class CreateExerciseRequestValidator : AbstractValidator<CreateExerciseRequest>
{
    public CreateExerciseRequestValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 80).WithMessage("Name must be between 2 and 80 characters.");

        RuleFor(e => e.Description)
            .MaximumLength(500).WithMessage("Description must contain at most 500 characters.");

        RuleFor(e => e.Equipment)
            .IsInEnum().WithMessage("Equipment must be None, Barbell, Dumbbell, Machine or Bodyweight.");

        RuleFor(e => e.MuscleGroupIds)
            .NotEmpty().WithMessage("At least one muscle group is required.")
            .Must(ids => ids == null || ids.Distinct().Count() == ids.Count)
            .WithMessage("Muscle group ids must not contain duplicates.");
    }
}

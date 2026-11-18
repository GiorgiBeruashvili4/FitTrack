using FitTrack.DTOs.Workouts;
using FluentValidation;

namespace FitTrack.Validators;

public class WorkoutSetRequestValidator : AbstractValidator<WorkoutSetRequest>
{
    public WorkoutSetRequestValidator()
    {
        RuleFor(s => s.ExerciseId)
            .GreaterThan(0).WithMessage("Exercise id is required.");

        RuleFor(s => s.SetNumber)
            .InclusiveBetween(1, 20).WithMessage("Set number must be between 1 and 20.");

        RuleFor(s => s.Reps)
            .InclusiveBetween(1, 100).WithMessage("Reps must be between 1 and 100.");

        RuleFor(s => s.WeightKg)
            .InclusiveBetween(0, 500).WithMessage("Weight must be between 0 and 500 kg.");
    }
}

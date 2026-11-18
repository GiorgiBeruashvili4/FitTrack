using FitTrack.DTOs.Workouts;
using FluentValidation;

namespace FitTrack.Validators;

public class UpdateWorkoutRequestValidator : AbstractValidator<UpdateWorkoutRequest>
{
    public UpdateWorkoutRequestValidator()
    {
        RuleFor(w => w.Date)
            .Must(d => d <= DateTime.UtcNow).WithMessage("Date cannot be in the future.")
            .Must(d => d >= DateTime.UtcNow.AddYears(-1)).WithMessage("Date cannot be older than 1 year.");

        RuleFor(w => w.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(2, 80).WithMessage("Title must be between 2 and 80 characters.");

        RuleFor(w => w.DurationMinutes)
            .InclusiveBetween(1, 600).WithMessage("Duration must be between 1 and 600 minutes.");

        RuleFor(w => w.Notes)
            .MaximumLength(1000).WithMessage("Notes must contain at most 1000 characters.");

        RuleFor(w => w.Sets)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("At least one set is required.")
            .Must(sets => sets.Count <= 100).WithMessage("A workout can have at most 100 sets.")
            .Must(sets => sets.GroupBy(s => new { s.ExerciseId, s.SetNumber }).All(g => g.Count() == 1))
            .WithMessage("Set numbers must be unique within each exercise.");

        RuleForEach(w => w.Sets).SetValidator(new WorkoutSetRequestValidator());
    }
}

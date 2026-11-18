using FitTrack.DTOs.Workouts;
using FluentValidation;

namespace FitTrack.Validators;

public class WorkoutQueryValidator : AbstractValidator<WorkoutQuery>
{
    public WorkoutQueryValidator()
    {
        RuleFor(q => q.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page must be at least 1.");

        RuleFor(q => q.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");

        RuleFor(q => q)
            .Must(q => !q.From.HasValue || !q.To.HasValue || q.From.Value <= q.To.Value)
            .WithMessage("From date must not be later than To date.")
            .OverridePropertyName("from");
    }
}

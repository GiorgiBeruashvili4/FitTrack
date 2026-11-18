using FitTrack.DTOs.Exercises;
using FluentValidation;

namespace FitTrack.Validators;

public class ExerciseQueryValidator : AbstractValidator<ExerciseQuery>
{
    public ExerciseQueryValidator()
    {
        RuleFor(q => q.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page must be at least 1.");

        RuleFor(q => q.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");

        RuleFor(q => q.Equipment)
            .IsInEnum().WithMessage("Equipment must be None, Barbell, Dumbbell, Machine or Bodyweight.");

        RuleFor(q => q.Search)
            .MaximumLength(80).WithMessage("Search must contain at most 80 characters.");
    }
}

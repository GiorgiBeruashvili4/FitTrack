using FitTrack.DTOs.Stats;
using FluentValidation;

namespace FitTrack.Validators;

public class WeeklyStatsQueryValidator : AbstractValidator<WeeklyStatsQuery>
{
    public WeeklyStatsQueryValidator()
    {
        RuleFor(q => q.Weeks)
            .InclusiveBetween(1, 52).WithMessage("Weeks must be between 1 and 52.");
    }
}

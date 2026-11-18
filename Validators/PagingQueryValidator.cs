using FitTrack.DTOs.Common;
using FluentValidation;

namespace FitTrack.Validators;

public class PagingQueryValidator : AbstractValidator<PagingQuery>
{
    public PagingQueryValidator()
    {
        RuleFor(p => p.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page must be at least 1.");

        RuleFor(p => p.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
    }
}

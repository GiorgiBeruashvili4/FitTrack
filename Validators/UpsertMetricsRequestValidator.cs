using FitTrack.DTOs.Metrics;
using FluentValidation;

namespace FitTrack.Validators;

public class UpsertMetricsRequestValidator : AbstractValidator<UpsertMetricsRequest>
{
    public UpsertMetricsRequestValidator()
    {
        RuleFor(bm => bm.HeightCm)
            .InclusiveBetween(100, 250).WithMessage("Height must be between 100 and 250 cm.");

        RuleFor(bm => bm.WeightKg)
            .InclusiveBetween(30.0, 300.0).WithMessage("Weight must be between 30 and 300 kg.");

        RuleFor(bm => bm.Goal)
            .IsInEnum().WithMessage("Goal must be LoseWeight, GainMuscle or Maintain.");
    }
}

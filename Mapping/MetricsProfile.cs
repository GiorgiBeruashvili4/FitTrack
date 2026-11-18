using AutoMapper;
using FitTrack.DTOs.Metrics;
using FitTrack.Entities;
using FitTrack.Helpers;

namespace FitTrack.Mapping;

public class MetricsProfile : Profile
{
    public MetricsProfile()
    {
        CreateMap<BodyMetrics, BodyMetricsDto>()
            .ForMember(d => d.Bmi,
                o => o.MapFrom(
                    s => BmiCalculator.CalculateBmi(s.HeightCm, s.WeightKg)))
            .ForMember(d => d.BmiCategory,
                o => o.MapFrom(
                    s => BmiCalculator.GetCategory(BmiCalculator.CalculateBmi(s.HeightCm, s.WeightKg))));

        CreateMap<UpsertMetricsRequest, BodyMetrics>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.UserId, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore());
    }
}

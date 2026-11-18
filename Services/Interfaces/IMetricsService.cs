using FitTrack.DTOs.Metrics;

namespace FitTrack.Services.Interfaces;

public interface IMetricsService
{
    Task<BodyMetricsDto> GetMetricsAsync(int userId);
    Task<BodyMetricsDto> UpsertMetricsAsync(int userId, UpsertMetricsRequest request);
}

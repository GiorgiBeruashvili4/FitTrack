using AutoMapper;
using FitTrack.Data;
using FitTrack.DTOs.Metrics;
using FitTrack.Entities;
using FitTrack.Exceptions;
using FitTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Services.Implementations;

public class MetricsService : IMetricsService
{
    public readonly Database _db;
    private readonly IMapper _mapper;

    public MetricsService(Database db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<BodyMetricsDto> GetMetricsAsync(int userId)
    {
        var metrics = await _db.BodyMetrics.FirstOrDefaultAsync(m => m.UserId == userId);

        if (metrics == null)
        {
            throw new NotFoundException("Body metrics have not been set yet.");
        }

        return _mapper.Map<BodyMetricsDto>(metrics);
    }

    public async Task<BodyMetricsDto> UpsertMetricsAsync(int userId, UpsertMetricsRequest request)
    {
        var metrics = await _db.BodyMetrics.FirstOrDefaultAsync(m => m.UserId == userId);

        if (metrics == null)
        {
            metrics = _mapper.Map<BodyMetrics>(request);
            metrics.UserId = userId;
            _db.BodyMetrics.Add(metrics);
        }
        else
        {
            _mapper.Map(request, metrics);
        }

        metrics.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return _mapper.Map<BodyMetricsDto>(metrics);
    }
}

using FitTrack.DTOs.Metrics;
using FitTrack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Controllers;

[Route("api/me/metrics")]
[ApiController]
[Authorize(Roles = "Member")]
public class MetricsController : ControllerBase
{
    private readonly IMetricsService _metricsService;
    private readonly ICurrentUserService _currentUser;

    public MetricsController(IMetricsService metricsService, ICurrentUserService currentUser)
    {
        _metricsService = metricsService;
        _currentUser = currentUser;
    }

    [HttpGet]
    public async Task<ActionResult<BodyMetricsDto>> Get()
    {
        var result = await _metricsService.GetMetricsAsync(_currentUser.UserId);

        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<BodyMetricsDto>> Upsert(UpsertMetricsRequest request)
    {
        var result = await _metricsService.UpsertMetricsAsync(_currentUser.UserId, request);

        return Ok(result);
    }
}

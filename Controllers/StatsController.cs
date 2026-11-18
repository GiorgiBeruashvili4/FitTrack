using FitTrack.DTOs.Stats;
using FitTrack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Controllers;

[Route("api/stats")]
[ApiController]
[Authorize(Roles = "Member")]
public class StatsController : ControllerBase
{
    private readonly IStatsService _statsService;
    private readonly ICurrentUserService _currentUser;

    public StatsController(IStatsService statsService, ICurrentUserService currentUser)
    {
        _statsService = statsService;
        _currentUser = currentUser;
    }

    [HttpGet("weekly")]
    public async Task<ActionResult<List<WeeklyStatDto>>> GetWeekly([FromQuery] WeeklyStatsQuery query)
    {
        var result = await _statsService.GetWeeklyAsync(_currentUser.UserId, query.Weeks);
        return Ok(result);
    }

    [HttpGet("exercises/{exerciseId:int}")]
    public async Task<ActionResult<List<ExerciseProgressDto>>> GetExerciseProgress(int exerciseId)
    {
        var result = await _statsService.GetExerciseProgressAsync(_currentUser.UserId, exerciseId);
        return Ok(result);
    }
}

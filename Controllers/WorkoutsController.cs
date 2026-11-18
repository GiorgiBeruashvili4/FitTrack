using FitTrack.DTOs.Common;
using FitTrack.DTOs.Workouts;
using FitTrack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Controllers;

[Route("api/workouts")]
[ApiController]
[Authorize]
public class WorkoutsController : ControllerBase
{
    private readonly IWorkoutService _workoutService;
    private readonly ICurrentUserService _currentUser;

    public WorkoutsController(IWorkoutService workoutService, ICurrentUserService currentUser)
    {
        _workoutService = workoutService;
        _currentUser = currentUser;
    }

    [HttpGet]
    [Authorize(Roles = "Member")]
    public async Task<ActionResult<PagedResult<WorkoutDto>>> GetAll([FromQuery] WorkoutQuery query)
    {
        var result = await _workoutService.GetAllAsync(_currentUser.UserId, query);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<WorkoutDto>> GetById(int id)
    {
        var result = await _workoutService.GetByIdAsync(id, _currentUser.UserId);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Member")]
    public async Task<ActionResult<WorkoutDto>> Create(CreateWorkoutRequest request)
    {
        var result = await _workoutService.CreateAsync(_currentUser.UserId, request);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Member")]
    public async Task<ActionResult<WorkoutDto>> Update(int id, UpdateWorkoutRequest request)
    {
        var result = await _workoutService.UpdateAsync(id, _currentUser.UserId, request);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> Delete(int id)
    {
        await _workoutService.DeleteAsync(id, _currentUser.UserId);

        return NoContent();
    }
}

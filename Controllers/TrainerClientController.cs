using FitTrack.DTOs.Common;
using FitTrack.DTOs.Trainer;
using FitTrack.DTOs.Workouts;
using FitTrack.Services.Implementations;
using FitTrack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Controllers;

[Route("api/trainer/clients")]
[ApiController]
[Authorize(Roles = "Trainer")]
public class TrainerClientController : ControllerBase
{
    private readonly ITrainerService _trainerService;
    private readonly IWorkoutService _workoutService;
    private readonly ICurrentUserService _currentUser;

    public TrainerClientController(ITrainerService trainerService, IWorkoutService workoutService, ICurrentUserService currentUser)
    {
        _trainerService = trainerService;
        _workoutService = workoutService;
        _currentUser = currentUser;
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> AddClient(AddClientRequest request)
    {
        var result = await _trainerService.AddClientAsync(_currentUser.UserId, request);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ClientDto>>> GetClients([FromQuery] PagingQuery query)
    {
        var result = await _trainerService.GetClientsAsync(_currentUser.UserId, query);

        return Ok(result);
    }

    [HttpGet("{clientId:int}/workouts")]
    public async Task<ActionResult<PagedResult<WorkoutDto>>> GetClientWorkouts(
    int clientId, [FromQuery] WorkoutQuery query)
    {
        var result = await _workoutService.GetClientWorkoutsAsync(_currentUser.UserId, clientId, query);
        return Ok(result);
    }
}

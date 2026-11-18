using FitTrack.DTOs.Common;
using FitTrack.DTOs.MuscleGroups;
using FitTrack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Controllers;

[Route("api/muscle-groups")]
[ApiController]
[Authorize]
public class MuscleGroupController : ControllerBase
{
    private readonly IMuscleGroupService _muscleGroupService;

    public MuscleGroupController(IMuscleGroupService musicGroupService)
    {
        _muscleGroupService = musicGroupService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<MuscleGroupDto>>> GetAll([FromQuery] PagingQuery query)
    {
        var result = await _muscleGroupService.GetAllAsync(query.Page, query.PageSize);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Trainer")]
    public async Task<ActionResult<MuscleGroupDto>> Create(CreateMuscleGroupRequest request)
    {
        var result = await _muscleGroupService.CreateAsync(request);

        return StatusCode(StatusCodes.Status201Created, result);
    }
}

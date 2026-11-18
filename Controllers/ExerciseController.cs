using FitTrack.DTOs.Common;
using FitTrack.DTOs.Exercises;
using FitTrack.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitTrack.Controllers;

[Route("api/exercises")]
[ApiController]
[Authorize]
public class ExerciseController : ControllerBase
{
    private readonly IExerciseService _exerciseService;

    public ExerciseController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ExerciseDto>>> GetAll([FromQuery] ExerciseQuery query)
    {
        var result = await _exerciseService.GetAllAsync(query);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ExerciseDto>> GetById(int id)
    {
        var result = await _exerciseService.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Trainer")]
    public async Task<ActionResult<ExerciseDto>> Create(CreateExerciseRequest request)
    {
        var result = await _exerciseService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Trainer")]
    public async Task<ActionResult<ExerciseDto>> Update(int id, UpdateExerciseRequest request)
    {
        var result = await _exerciseService.UpdateAsync(id, request);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Trainer")]
    public async Task<IActionResult> Delete(int id)
    {
        await _exerciseService.DeleteAsync(id);

        return NoContent();
    }
}

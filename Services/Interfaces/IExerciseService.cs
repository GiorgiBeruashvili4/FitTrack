using FitTrack.DTOs.Common;
using FitTrack.DTOs.Exercises;

namespace FitTrack.Services.Interfaces;

public interface IExerciseService
{
    Task<PagedResult<ExerciseDto>> GetAllAsync(ExerciseQuery query);
    Task<ExerciseDto> GetByIdAsync(int id);
    Task<ExerciseDto> CreateAsync(CreateExerciseRequest request);
    Task<ExerciseDto> UpdateAsync(int id, UpdateExerciseRequest request);
    Task DeleteAsync(int id);
}

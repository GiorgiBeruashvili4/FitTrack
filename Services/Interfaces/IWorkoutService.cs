using FitTrack.DTOs.Common;
using FitTrack.DTOs.Workouts;

namespace FitTrack.Services.Interfaces;

public interface IWorkoutService
{
    Task<PagedResult<WorkoutDto>> GetAllAsync(int userId, WorkoutQuery query);
    Task<PagedResult<WorkoutDto>> GetClientWorkoutsAsync(int trainerId, int clientId, WorkoutQuery query);
    Task<WorkoutDto> GetByIdAsync(int id, int currentUserId);
    Task<WorkoutDto> CreateAsync(int userId, CreateWorkoutRequest request);
    Task<WorkoutDto> UpdateAsync(int id, int userId, UpdateWorkoutRequest request);
    Task DeleteAsync(int id, int userId);
}

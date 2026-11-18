using FitTrack.DTOs.Stats;

namespace FitTrack.Services.Interfaces;

public interface IStatsService
{
    Task<List<WeeklyStatDto>> GetWeeklyAsync(int userId, int weeks);
    Task<List<ExerciseProgressDto>> GetExerciseProgressAsync(int userId, int exerciseId);
}

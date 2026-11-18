using FitTrack.Data;
using FitTrack.DTOs.Stats;
using FitTrack.Entities;
using FitTrack.Exceptions;
using FitTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Services.Implementations;

public class StatsService : IStatsService
{
    private readonly Database _db;

    public StatsService(Database db)
    {
        _db = db;
    }

    public async Task<List<WeeklyStatDto>> GetWeeklyAsync(int userId, int weeks)
    {
        var currentWeekStart = GetWeekStart(DateTime.UtcNow);
        var firstWeekStart = currentWeekStart.AddDays(-7 * (weeks - 1));

        var workouts = await _db.Workouts
            .AsNoTracking()
            .Where(w => w.UserId == userId && w.Date >= firstWeekStart)
            .Select(w => new
            {
                w.Date,
                w.DurationMinutes,
                Volume = (double)w.WorkoutSets.Sum(s => s.Reps * s.WeightKg)
            })
            .ToListAsync();

        var byWeek = workouts
            .GroupBy(w => GetWeekStart(w.Date))
            .ToDictionary(g => g.Key, g => g.ToList());

        var result = new List<WeeklyStatDto>();

        for (var i = 0; i < weeks; i++)
        {
            var weekStart = firstWeekStart.AddDays(7 * i);

            if (byWeek.TryGetValue(weekStart, out var weekWorkouts))
            {
                result.Add(new WeeklyStatDto
                {
                    WeekStart = weekStart,
                    WorkoutsCount = weekWorkouts.Count,
                    TotalVolume = Math.Round(weekWorkouts.Sum(w => w.Volume), 2),
                    TotalMinutes = weekWorkouts.Sum(w => w.DurationMinutes)
                });
            }
            else
            {
                result.Add(new WeeklyStatDto
                {
                    WeekStart = weekStart,
                    WorkoutsCount = 0,
                    TotalVolume = 0,
                    TotalMinutes = 0
                });
            }
        }

        return result;
    }

    public async Task<List<ExerciseProgressDto>> GetExerciseProgressAsync(int userId, int exerciseId)
    {
        var exerciseExists = await _db.Exercises.AnyAsync(e => e.Id == exerciseId);

        if (!exerciseExists)
        {
            throw new NotFoundException(nameof(Exercise), exerciseId);
        }

        var sets = await _db.Workouts
            .AsNoTracking()
            .Where(w => w.UserId == userId)
            .SelectMany(w => w.WorkoutSets
                .Where(s => s.ExerciseId == exerciseId)
                .Select(s => new { w.Date, s.WeightKg }))
            .ToListAsync();

        return sets
            .GroupBy(x => x.Date.Date)
            .OrderBy(g => g.Key)
            .Select(g => new ExerciseProgressDto
            {
                Date = g.Key,
                MaxWeightKg = g.Max(x => x.WeightKg)
            })
            .ToList();
    }

    private static DateTime GetWeekStart(DateTime date)
    {
        var day = date.Date;
        var daysSinceMonday = ((int)day.DayOfWeek + 6) % 7;
        return day.AddDays(-daysSinceMonday);
    }
}

using AutoMapper;
using FitTrack.Data;
using FitTrack.DTOs.Common;
using FitTrack.DTOs.Workouts;
using FitTrack.Entities;
using FitTrack.Exceptions;
using FitTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Services.Implementations;

public class WorkoutService : IWorkoutService
{
    private readonly Database _db;
    private readonly IMapper _mapper;

    public WorkoutService(Database db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PagedResult<WorkoutDto>> GetAllAsync(int userId, WorkoutQuery query)
    {
        var workouts = _db.Workouts
            .AsNoTracking()
            .Where(w => w.UserId == userId);

        if (query.From.HasValue)
        {
            var from = query.From.Value.Date;
            workouts = workouts.Where(w => w.Date >= from);
        }

        if (query.To.HasValue)
        {
            var toExclusive = query.To.Value.Date.AddDays(1);
            workouts = workouts.Where(w => w.Date < toExclusive);
        }

        var totalCount = await workouts.CountAsync();

        var items = await workouts
            .Include(w => w.WorkoutSets)
                .ThenInclude(s => s.Exercise)
            .OrderByDescending(w => w.Date)
            .ThenByDescending(w => w.Id)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PagedResult<WorkoutDto>
        {
            Items = _mapper.Map<List<WorkoutDto>>(items),
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }

    public async Task<PagedResult<WorkoutDto>> GetClientWorkoutsAsync(
        int trainerId, int clientId, WorkoutQuery query)
    {
        var isMyClient = await _db.TrainerClients
            .AnyAsync(tc => tc.TrainerId == trainerId && tc.ClientId == clientId);

        if (!isMyClient)
        {
            throw new ForbiddenException("This user is not your client.");
        }

        return await GetAllAsync(clientId, query);
    }

    public async Task<WorkoutDto> GetByIdAsync(int id, int currentUserId)
    {
        var workout = await LoadWorkoutAsync(id);

        if (workout == null)
        {
            throw new NotFoundException(nameof(Workout), id);
        }

        if (workout.UserId != currentUserId)
        {
            var isTrainerOfOwner = await _db.TrainerClients
                .AnyAsync(tc => tc.TrainerId == currentUserId && tc.ClientId == workout.UserId);

            if (!isTrainerOfOwner)
            {
                throw new ForbiddenException("You do not have access to this workout.");
            }
        }

        return _mapper.Map<WorkoutDto>(workout);
    }

    public async Task<WorkoutDto> CreateAsync(int userId, CreateWorkoutRequest request)
    {
        await EnsureExercisesExistAsync(request.Sets);

        var workout = _mapper.Map<Workout>(request);
        workout.UserId = userId;
        workout.WorkoutSets = _mapper.Map<List<WorkoutSet>>(request.Sets);

        _db.Workouts.Add(workout);
        await _db.SaveChangesAsync();

        var created = await LoadWorkoutAsync(workout.Id);
        return _mapper.Map<WorkoutDto>(created);
    }

    public async Task<WorkoutDto> UpdateAsync(int id, int userId, UpdateWorkoutRequest request)
    {
        var workout = await _db.Workouts
            .Include(w => w.WorkoutSets)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (workout == null)
        {
            throw new NotFoundException(nameof(Workout), id);
        }

        if (workout.UserId != userId)
        {
            throw new ForbiddenException("You can only edit your own workouts.");
        }

        await EnsureExercisesExistAsync(request.Sets);

        _mapper.Map(request, workout);

        _db.WorkoutSets.RemoveRange(workout.WorkoutSets.ToList());
        workout.WorkoutSets = _mapper.Map<List<WorkoutSet>>(request.Sets);

        await _db.SaveChangesAsync();

        var updated = await LoadWorkoutAsync(id);
        return _mapper.Map<WorkoutDto>(updated);
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var workout = await _db.Workouts.FirstOrDefaultAsync(w => w.Id == id);

        if (workout == null)
        {
            throw new NotFoundException(nameof(Workout), id);
        }

        if (workout.UserId != userId)
        {
            throw new ForbiddenException("You can only delete your own workouts.");
        }

        _db.Workouts.Remove(workout);
        await _db.SaveChangesAsync();
    }

    private async Task<Workout?> LoadWorkoutAsync(int id)
    {
        return await _db.Workouts
            .AsNoTracking()
            .Include(w => w.WorkoutSets)
                .ThenInclude(s => s.Exercise)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    private async Task EnsureExercisesExistAsync(List<WorkoutSetRequest> sets)
    {
        var requestedIds = sets.Select(s => s.ExerciseId).Distinct().ToList();

        var existingIds = await _db.Exercises
            .Where(e => requestedIds.Contains(e.Id))
            .Select(e => e.Id)
            .ToListAsync();

        var missing = requestedIds.Except(existingIds).ToList();

        if (missing.Count > 0)
        {
            throw new BusinessRuleException($"Exercises not found: {string.Join(", ", missing)}.");
        }
    }
}

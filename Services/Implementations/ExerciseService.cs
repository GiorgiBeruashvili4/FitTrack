using AutoMapper;
using Azure.Core;
using FitTrack.Data;
using FitTrack.DTOs.Common;
using FitTrack.DTOs.Exercises;
using FitTrack.Entities;
using FitTrack.Exceptions;
using FitTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Services.Implementations;

public class ExerciseService : IExerciseService
{
    private readonly Database _db;
    private readonly IMapper _mapper;

    public ExerciseService(Database db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PagedResult<ExerciseDto>> GetAllAsync(ExerciseQuery query)
    {
        IQueryable<Exercise> exercises = _db.Exercises
            .AsNoTracking()
            .Include(e => e.ExerciseMuscleGroups)
            .ThenInclude(x => x.MuscleGroup);

        if (query.MuscleGroupId.HasValue)
        {
            exercises = exercises.Where(
                e => e.ExerciseMuscleGroups
                .Any(x => x.MuscleGroupId == query.MuscleGroupId.Value));
        }

        if (query.Equipment.HasValue)
        {
            exercises = exercises.Where(e => e.Equipment == query.Equipment.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            var search = query.Search.Trim();
            exercises = exercises.Where(e => e.Name.Contains(search));
        }

        var totalCount = await exercises.CountAsync();

        var items = await exercises
            .OrderBy(e => e.Name)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return new PagedResult<ExerciseDto>
        {
            Items = _mapper.Map<List<ExerciseDto>>(items),
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }

    public async Task<ExerciseDto> GetByIdAsync(int id)
    {
        var exercise = await _db.Exercises
            .AsNoTracking()
            .Include(e => e.ExerciseMuscleGroups)
            .ThenInclude(x => x.MuscleGroup)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (exercise == null)
        {
            throw new NotFoundException(nameof(Exercise), id);
        }

        return _mapper.Map<ExerciseDto>(exercise);
    }

    public async Task<ExerciseDto> CreateAsync(CreateExerciseRequest request)
    {
        var name = request.Name.Trim();

        var exists = await _db.Exercises.AnyAsync(e => e.Name == name);

        if (exists)
        {
            throw new ConflictException("An exercise with this name already exists.");
        }

        var muscleGroups = await LoadMuscleGroupsAsync(request.MuscleGroupIds);

        var exercise = _mapper.Map<Exercise>(request);
        exercise.Name = name;
        exercise.ExerciseMuscleGroups = muscleGroups
            .Select(m => new ExerciseMuscleGroup { MuscleGroup = m })
            .ToList();

        _db.Exercises.Add(exercise);
        await _db.SaveChangesAsync();

        return _mapper.Map<ExerciseDto>(exercise);
    }

    public async Task<ExerciseDto> UpdateAsync(int id, UpdateExerciseRequest request)
    {
        var exercise = await _db.Exercises
            .Include(e => e.ExerciseMuscleGroups)
            .ThenInclude(x => x.MuscleGroup)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (exercise == null)
        {
            throw new NotFoundException(nameof(Exercise), id);
        }

        var name = request.Name.Trim();

        var nameTaken = await _db.Exercises.AnyAsync(e => e.Name == name && e.Id != id);

        if (nameTaken)
        {
            throw new ConflictException("An exercise with this name already exists.");
        }

        var muscleGroups = await LoadMuscleGroupsAsync(request.MuscleGroupIds);
        var newIds = muscleGroups.Select(m => m.Id).ToList();

        _mapper.Map(request, exercise);
        exercise.Name = name;

        var toRemove = exercise.ExerciseMuscleGroups
            .Where(x => !newIds.Contains(x.MuscleGroupId))
            .ToList();

        _db.RemoveRange(toRemove);

        var existingIds = exercise.ExerciseMuscleGroups
            .Select(x => x.MuscleGroupId)
            .ToList();

        foreach (var muscleGroup in muscleGroups.Where(m => !existingIds.Contains(m.Id)))
        {
            exercise.ExerciseMuscleGroups.Add(new ExerciseMuscleGroup { MuscleGroup = muscleGroup });
        }

        await _db.SaveChangesAsync();

        return _mapper.Map<ExerciseDto>(exercise);
    }

    public async Task DeleteAsync(int id)
    {
        var exercise = await _db.Exercises
            .Include(e => e.ExerciseMuscleGroups)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (exercise == null)
        {
            throw new NotFoundException(nameof(Exercise), id);
        }

        var inUse = await _db.WorkoutSets.AnyAsync(s => s.ExerciseId == id);

        if (inUse)
        {
            throw new BusinessRuleException("Exercise is used in workouts and cannot be deleted.");
        }

        _db.RemoveRange(exercise.ExerciseMuscleGroups);
        _db.Exercises.Remove(exercise);

        await _db.SaveChangesAsync();
    }

    private async Task<List<MuscleGroup>> LoadMuscleGroupsAsync(List<int> ids)
    {
        var distinctIds = ids.Distinct().ToList();

        var muscleGroups = await _db.MuscleGroups
            .Where(m => distinctIds.Contains(m.Id))
            .ToListAsync();

        if (muscleGroups.Count != distinctIds.Count)
        {
            throw new BusinessRuleException("One or more muscle groups do not exist.");
        }

        return muscleGroups;
    }
}

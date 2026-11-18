using AutoMapper;
using FitTrack.Data;
using FitTrack.DTOs.Common;
using FitTrack.DTOs.MuscleGroups;
using FitTrack.Entities;
using FitTrack.Exceptions;
using FitTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Services.Implementations;

public class MuscleGroupService : IMuscleGroupService
{
    private readonly Database _db;
    private readonly IMapper _mapper;

    public MuscleGroupService(Database db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<PagedResult<MuscleGroupDto>> GetAllAsync(int page, int pageSize)
    {
        var query = _db.MuscleGroups.AsNoTracking().OrderBy(mg => mg.Name);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<MuscleGroupDto>
        {
            Items = _mapper.Map<List<MuscleGroupDto>>(items),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<MuscleGroupDto> CreateAsync(CreateMuscleGroupRequest request)
    {
        var name = request.Name.Trim();

        var exists = await _db.MuscleGroups.AnyAsync(mg => mg.Name == name);

        if (exists)
        {
            throw new ConflictException("A muscle group with this name already exists.");
        }

        var muscleGroup = _mapper.Map<MuscleGroup>(request);
        muscleGroup.Name = name;

        _db.MuscleGroups.Add(muscleGroup);
        await _db.SaveChangesAsync();

        return _mapper.Map<MuscleGroupDto>(muscleGroup);
    }
}

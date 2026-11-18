using FitTrack.DTOs.Common;
using FitTrack.DTOs.MuscleGroups;

namespace FitTrack.Services.Interfaces;

public interface IMuscleGroupService
{
    Task<PagedResult<MuscleGroupDto>> GetAllAsync(int page, int pageSize);
    Task<MuscleGroupDto> CreateAsync(CreateMuscleGroupRequest request);
}

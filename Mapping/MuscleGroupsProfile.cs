using AutoMapper;
using FitTrack.DTOs.MuscleGroups;
using FitTrack.Entities;

namespace FitTrack.Mapping;

public class MuscleGroupsProfile : Profile
{
    public MuscleGroupsProfile()
    {
        CreateMap<MuscleGroup, MuscleGroupDto>();
        CreateMap<CreateMuscleGroupRequest, MuscleGroup>(MemberList.Source);
    }
}

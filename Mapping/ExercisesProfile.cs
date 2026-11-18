using AutoMapper;
using FitTrack.DTOs.Exercises;
using FitTrack.Entities;

namespace FitTrack.Mapping;

public class ExercisesProfile : Profile
{
    public ExercisesProfile()
    {
        CreateMap<Exercise, ExerciseDto>()
            .ForMember(d => d.MuscleGroups,
                o => o.MapFrom(
                    s => s.ExerciseMuscleGroups.Select(x => x.MuscleGroup.Name)));

        CreateMap<CreateExerciseRequest, Exercise>(MemberList.Source)
            .ForSourceMember(s => s.MuscleGroupIds, o => o.DoNotValidate());

        CreateMap<UpdateExerciseRequest, Exercise>(MemberList.Source)
            .ForSourceMember(s => s.MuscleGroupIds, o => o.DoNotValidate());
    }
}

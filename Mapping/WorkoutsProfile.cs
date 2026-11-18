using AutoMapper;
using FitTrack.DTOs.Workouts;
using FitTrack.Entities;

namespace FitTrack.Mapping;

public class WorkoutsProfile : Profile
{
    public WorkoutsProfile()
    {
        CreateMap<Workout, WorkoutDto>()
            .ForMember(d => d.TotalVolume,
                o => o.MapFrom(s => s.WorkoutSets.Sum(x => x.Reps * x.WeightKg)))
            .ForMember(d => d.Exercises,
                o => o.MapFrom(s => GroupByExercise(s.WorkoutSets)));

        CreateMap<WorkoutSetRequest, WorkoutSet>(MemberList.Source);

        CreateMap<CreateWorkoutRequest, Workout>(MemberList.Source)
            .ForSourceMember(s => s.Sets, o => o.DoNotValidate());

        CreateMap<UpdateWorkoutRequest, Workout>(MemberList.Source)
            .ForSourceMember(s => s.Sets, o => o.DoNotValidate());
    }

    private static List<WorkoutExerciseDto> GroupByExercise(IEnumerable<WorkoutSet> sets)
    {
        return sets
            .GroupBy(s => s.ExerciseId)
            .OrderBy(g => g.Min(s => s.Id))
            .Select(g => new WorkoutExerciseDto
            {
                ExerciseName = g.First().Exercise.Name,
                Sets = g
                    .OrderBy(s => s.SetNumber)
                    .Select(s => new WorkoutSetDto
                    {
                        SetNumber = s.SetNumber,
                        Reps = s.Reps,
                        WeightKg = s.WeightKg
                    })
                    .ToList()
            })
            .ToList();
    }
}

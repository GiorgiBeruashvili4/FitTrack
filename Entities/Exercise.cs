using FitTrack.Enums;

namespace FitTrack.Entities;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Equipment Equipment { get; set; }

    public List<ExerciseMuscleGroup> ExerciseMuscleGroups { get; set; } = new List<ExerciseMuscleGroup>();
    public List<WorkoutSet> WorkoutSets { get; set; } = new List<WorkoutSet>();
}

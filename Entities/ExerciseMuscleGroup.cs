namespace FitTrack.Entities;

public class ExerciseMuscleGroup
{
    public Exercise Exercise { get; set; } = null!;
    public int ExerciseId { get; set; }

    public MuscleGroup MuscleGroup { get; set; } = null!;
    public int MuscleGroupId { get; set; }
}

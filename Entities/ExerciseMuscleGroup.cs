namespace FitTrack.Entities;

public class ExerciseMuscleGroup
{
    public int Id { get; set; }

    public Exercise Exercise { get; set; } = null!;
    public int ExerciseId { get; set; }

    public MuscleGroup MuscleGroup { get; set; } = null!;
    public int MuscleGroupId { get; set; }
}

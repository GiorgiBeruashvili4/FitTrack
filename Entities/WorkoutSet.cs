namespace FitTrack.Entities;

public class WorkoutSet
{
    public int Id { get; set; }
    public byte SetNumber { get; set; }
    public byte Reps { get; set; }
    public int WeightKg { get; set; }

    public Workout Workout { get; set; } = null!;
    public int WorkoutId { get; set; }

    public Exercise Exercise { get; set; } = null!;
    public int ExerciseId { get; set; }
}

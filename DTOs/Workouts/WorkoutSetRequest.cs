namespace FitTrack.DTOs.Workouts;

public class WorkoutSetRequest
{
    public int ExerciseId { get; set; }
    public int SetNumber { get; set; }
    public int Reps { get; set; }
    public double WeightKg { get; set; }
}

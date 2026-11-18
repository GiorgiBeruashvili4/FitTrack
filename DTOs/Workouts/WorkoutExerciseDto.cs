namespace FitTrack.DTOs.Workouts;

public class WorkoutExerciseDto
{
    public string ExerciseName { get; set; } = string.Empty;
    public List<WorkoutSetDto> Sets { get; set; } = new();
}

namespace FitTrack.DTOs.Workouts;

public class UpdateWorkoutRequest
{
    public DateTime Date { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public int DurationMinutes { get; set; }
    public List<WorkoutSetRequest> Sets { get; set; } = new();
}

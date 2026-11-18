namespace FitTrack.DTOs.Workouts;

public class WorkoutDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public int DurationMinutes { get; set; }
    public double TotalVolume { get; set; }
    public List<WorkoutExerciseDto> Exercises { get; set; } = new();
}

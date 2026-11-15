namespace FitTrack.Entities;

public class Workout
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public int DurationMinutes { get; set; }

    public User User { get; set; } = null!;
    public int UserId { get; set; }

    public List<WorkoutSet> WorkoutSets { get; set; } = new List<WorkoutSet>(); 
}

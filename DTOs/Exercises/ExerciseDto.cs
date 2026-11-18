using FitTrack.Enums;

namespace FitTrack.DTOs.Exercises;

public class ExerciseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Equipment Equipment { get; set; }
    public List<string> MuscleGroups { get; set; } = new();
}

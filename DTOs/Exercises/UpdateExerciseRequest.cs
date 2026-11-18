using FitTrack.Enums;

namespace FitTrack.DTOs.Exercises;

public class UpdateExerciseRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Equipment Equipment { get; set; }
    public List<int> MuscleGroupIds { get; set; } = new();
}

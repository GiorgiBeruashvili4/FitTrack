using FitTrack.DTOs.Common;
using FitTrack.Enums;

namespace FitTrack.DTOs.Exercises;

public class ExerciseQuery : PagingQuery
{
    public int? MuscleGroupId { get; set; }
    public Equipment? Equipment { get; set; }
    public string? Search { get; set; }
}

using FitTrack.DTOs.Common;

namespace FitTrack.DTOs.Workouts;

public class WorkoutQuery : PagingQuery
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}

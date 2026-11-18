namespace FitTrack.DTOs.Stats;

public class WeeklyStatDto
{
    public DateTime WeekStart { get; set; }
    public int WorkoutsCount { get; set; }
    public double TotalVolume { get; set; }
    public int TotalMinutes { get; set; }
}

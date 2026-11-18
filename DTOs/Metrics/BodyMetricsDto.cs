using FitTrack.Enums;

namespace FitTrack.DTOs.Metrics;

public class BodyMetricsDto
{
    public int HeightCm { get; set; }
    public double WeightKg { get; set; }
    public Goal Goal { get; set; }
    public double Bmi { get; set; }
    public string BmiCategory { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
}

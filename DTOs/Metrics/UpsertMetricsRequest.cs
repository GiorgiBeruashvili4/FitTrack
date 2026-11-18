using FitTrack.Enums;

namespace FitTrack.DTOs.Metrics;

public class UpsertMetricsRequest
{
    public int HeightCm { get; set; }
    public double WeightKg { get; set; }
    public Goal Goal { get; set; }
}

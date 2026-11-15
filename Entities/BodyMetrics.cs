using FitTrack.Enums;

namespace FitTrack.Entities;

public class BodyMetrics
{
    public int Id { get; set; }
    public int HeightCm { get; set; }
    public double WeightKg { get; set; }
    public Goal Goal { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public int UserId { get; set; }
}

namespace FitTrack.Entities;

public class TrainerClient
{
    public DateOnly Since { get; set; }

    public User Trainer { get; set; } = null!;
    public int TrainerId { get; set; }

    public User Client { get; set; } = null!;
    public int ClientId { get; set; }
}

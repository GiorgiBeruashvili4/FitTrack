namespace FitTrack.DTOs.Trainer;

public class ClientDto
{
    public int ClientId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly Since { get; set; }
    public DateTime? LastWorkoutDate { get; set; }
}

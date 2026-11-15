using FitTrack.Enums;

namespace FitTrack.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public Role Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Workout> Workouts { get; set; } = new List<Workout>();
    public BodyMetrics? BodyMetrics { get; set; }

    public List<User> Trainers { get; set; } = new List<User>();
    public List<User> Clients { get; set; } = new List<User>();
}

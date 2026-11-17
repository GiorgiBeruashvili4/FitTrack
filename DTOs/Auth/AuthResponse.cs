namespace FitTrack.DTOs.Auth;

public class AuthResponse
{
    public string accessToken { get; set; } = string.Empty;
    public DateTime createdAt { get; set; }
}

namespace FitTrack.DTOs.Auth;

public class AuthResponse
{
    public string CccessToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}

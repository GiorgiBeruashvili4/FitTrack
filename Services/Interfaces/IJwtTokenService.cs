using FitTrack.DTOs.Auth;
using FitTrack.Entities;

namespace FitTrack.Services.Interfaces;

public interface IJwtTokenService
{
    AuthResponse GenerateToken(User user);
}

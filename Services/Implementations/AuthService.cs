using FitTrack.Data;
using FitTrack.DTOs.Auth;
using FitTrack.Entities;
using FitTrack.Exceptions;
using FitTrack.Helpers;
using FitTrack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly Database _db;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(Database db, IJwtTokenService jwtTokenService)
    {
        _db = db;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var email = request.Email.Trim().ToLower();
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null || !PasswordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid email or password.");
        }

        return _jwtTokenService.GenerateToken(user);
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var email = request.Email.Trim().ToLower();
        var emailExists = await _db.Users.AnyAsync(u => u.Email == email);
        
        if (emailExists)
        {
            throw new ConflictException("Registration failed. Please check your details or try signing in.");
        }

        var newUser = new User()
        {
            Email = email,
            PasswordHash = PasswordHasher.Hash(request.Password),
            FullName = request.FullName.Trim(),
            Role = Enums.Role.Member,
            CreatedAt = DateTime.UtcNow,
        };

        _db.Users.Add(newUser);
        await _db.SaveChangesAsync();

        return _jwtTokenService.GenerateToken(newUser);
    }
}

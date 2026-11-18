using System.Security.Claims;
using FitTrack.Exceptions;
using FitTrack.Services.Interfaces;

namespace FitTrack.Services.Implementations;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User.FindFirstValue("sub");

            if (value == null || !int.TryParse(value, out var id))
            {
                throw new UnauthorizedException("User is not authenticated.");
            }

            return id;
        }
    }

    public string Role
    {
        get
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue("role") ?? string.Empty;
        }
    }
}

using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _accessor;

    public UserService(IHttpContextAccessor accessor)
    {
        this._accessor = accessor;
    }
    
    public Guid GetUserId()
    {
        var currentUserId = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse(currentUserId);
        return userId;
    }
}
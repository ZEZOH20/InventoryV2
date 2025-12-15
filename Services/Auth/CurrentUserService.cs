
using System.Security.Claims;
using InventoryV2.Interfaces.IServices;

namespace InventoryV2.Services.Auth
{
    public class CurrentUserService : ICurrentUserService
    {
        readonly IHttpContextAccessor _context;
        public CurrentUserService(IHttpContextAccessor context)
        {
            _context = context;
        }
        public string? UserId
            => _context.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? UserRole
         => _context.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);

        public string? UserIp
        => _context.HttpContext?.Connection?.RemoteIpAddress?.ToString();
    }
}


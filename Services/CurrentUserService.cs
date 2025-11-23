
using System.Security.Claims;

namespace InventoryV2.Services
{
    public class CurrentUserService
    {
        readonly IHttpContextAccessor _context;
        public CurrentUserService(IHttpContextAccessor context)
        {
            _context = context;
        }
        public string? UserId()
            => _context.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? UserRole()
         => _context.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
            
    }
}

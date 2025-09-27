using InventoryV2.Models;
using System.IdentityModel.Tokens.Jwt;

namespace InventoryV2.Interfaces.IServices
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateToken(ApplicationUser user, string role);
    }
}

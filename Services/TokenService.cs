using InventoryV2.Interfaces.IServices;
using InventoryV2.Models;
using InventoryV2.Shares;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryV2.Services
{
    public class TokenService: ITokenService
    {

        readonly JwtSettings _Jwt;
        public TokenService(IOptions<JwtSettings> Jwt) {
            _Jwt = Jwt.Value;
        }


        /*
         Token 3 Parts :
         header : hash algo , type of library token
         payload  : claims, expiredate
         signture : hash(hash(header) + hash(payload) + hash(secret Key))

         iat : issued Time (creation Time)
         jti : Jwt ID 
         Sub : user ID
         issuer : who create the token (your server url)
         audience : who will capable to use it (client url)
         SigningCredentials ensure key is compatible with creditionl

         
         */
        public JwtSecurityToken GenerateToken(ApplicationUser user , string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                          DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("roles",role)   
            };

            var token = new JwtSecurityToken(
                issuer: _Jwt.Issuer,
                audience: _Jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_Jwt.DurationInHours),
                signingCredentials: creds);

            return token;
        }

        //public bool ValidateToken()
        //{
        //    //check if token exists in request header  

        //}
    }
}

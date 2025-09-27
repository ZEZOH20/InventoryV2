using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InventoryV2.Dtos.AuthDtos.Requests;

namespace InventoryV2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthCookieController : ControllerBase
    {

        [HttpGet("LoginView")]
        public IActionResult Login()
        {
            return Ok("login view");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("username or password icorrect");
            }

            //check first if user email and password exists in database

            //create identity
            ClaimsPrincipal principal = new ClaimsPrincipal();
            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            Claim[] claims = new Claim[] {
                new Claim(ClaimTypes.Role,"officer"),
                new Claim(ClaimTypes.Email,dto.Email)
            };

            foreach (Claim claim in claims)
                identity.AddClaim(claim);

            principal.AddIdentity(identity);

            await HttpContext.SignInAsync(principal);

            return Ok("Successfuly Login");
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> LogOut()
        {
            //HttpContext.Request.Cookies[".AspNetCore.Cookies"] != null
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("Login");
            }
            return Ok("You Already Logout");
        }
    }
}

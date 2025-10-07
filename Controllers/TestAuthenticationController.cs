using InventoryV2.Seeders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "officer")]
    public class TestAuthenticationController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Welcome to my Website");
        }


        [Authorize(Roles = SystemRoles.Manager)]
        [HttpGet("testAuthorize")]
        public IActionResult TestAuthorize()
        {
            return Ok("User Authorize");
        }

    }
}

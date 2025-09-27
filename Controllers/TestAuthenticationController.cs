using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "officer")]
    public class TestAuthenticationController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Welcome to my Website");
        }
    }
}

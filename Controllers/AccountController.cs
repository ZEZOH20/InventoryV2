using FluentValidation;
using InventoryV2.Dtos.AuthDtos.Requests;
using InventoryV2.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;


namespace InventoryV2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("SlidingPolicy")]
    public class AccountController : ControllerBase
    {
      
        public IAuthService _authService;
        readonly IServiceProvider _serviceProvider;
        public AccountController(
            IAuthService authService,
            IServiceProvider serviceProvider
            )
        {
            _authService = authService; 
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Authenticates a user with email and password.
        /// </summary>
        /// <param name="dto">The login request containing email and password.</param>
        /// <returns>A JWT token if login is successful.</returns>
        /// 

        //[Authorize(Roles = SystemRoles.Manager)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<LoginDto>>();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _authService.LoginAsync(dto);

                return StatusCode((int)response.StatusCode, new
                {
                    IsAuthenticated = response.IsSuccess,
                    message = response.Message,
                    statusCode = response.StatusCode,
                    data = response.Data
                });

        }

       
        [HttpPost("register")] 
        public async Task<IActionResult> Register([FromBody] RegisterDto dto , CancellationToken cancellationToken)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<RegisterDto>>();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);


            var response = await _authService.RegisterAsync(dto , cancellationToken);

      
                return StatusCode((int) response.StatusCode, new
                {
                    IsAuthenticated = response.IsSuccess,
                    message = response.Message,
                    statusCode = response.StatusCode,
                    data = response.Data
                });

        }
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto, CancellationToken cancellationToken)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<ResetPasswordDto>>();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _authService.ResetPasswordAsync(dto , cancellationToken);

    
            return StatusCode((int)response.StatusCode, new
               {
                    IsAuthenticated = response.IsSuccess,
                    message = response.Message,
                    statusCode = response.StatusCode,
               });

        }

        [HttpPost("verifyEmail")]
        public async Task<IActionResult> SendVerificationEmail([FromBody] SendVerificationEmailRqDto dto , CancellationToken cancellationToken)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<SendVerificationEmailRqDto>>();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _authService.SendVerificationEmailAsync(dto, cancellationToken);

         
            return StatusCode((int)response.StatusCode, new
              {
                 message = response.Message,
                 statusCode = response.StatusCode,
                 data = response.Data
            });
        }


    }
}

using FluentValidation;
using InventoryV2.Dtos;
using InventoryV2.Dtos.AuthDtos.Requests;
using InventoryV2.Dtos.AuthDtos.Responses;
using InventoryV2.Dtos.AuthDtos.Validators;
using InventoryV2.Interfaces.IServices;
using InventoryV2.Models;
using InventoryV2.Seeders;
using InventoryV2.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using System.Security.Claims;

namespace InventoryV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [Authorize(Roles = SystemRoles.Manager)]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            string token = _authService.Login(dto);
          
            return Ok(token);
        }

       
        [HttpPost("register")] 
        public async Task<IActionResult> Register([FromBody] RegisterDto dto , CancellationToken cancellationToken)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<RegisterDto>>();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);


            var response = await _authService.RegisterAsync(dto , cancellationToken);


            //Failure
            if (!response.IsSuccess)
                return StatusCode((int) response.StatusCode, new
                {
                    IsAuthenticated = false,
                    message = response.Message,
                    statusCode = response.StatusCode,
                });

       
            //Success
            return Ok( new
            {
                IsAuthenticated = true,
                message = response.Message,
                statusCode = response.StatusCode,
                data = response.Data

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

            //Failure
            if (!response.IsSuccess)
                return StatusCode((int)response.StatusCode, new
                {
                    message = response.Message,
                    statusCode = response.StatusCode,
                });


            //Success
            return Ok(new
            {
                message = response.Message,
                statusCode = response.StatusCode,
                data = response.Data
            });
        }


    }
}

using AutoMapper;
using FluentValidation;
using InventoryV2.Dtos.ProfileDto.Requests;
using InventoryV2.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryV2.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController:ControllerBase
    {
        readonly IProfileService _profileS;
        readonly IServiceProvider _serviceProvider;
        public ProfileController(IProfileService profileS , IServiceProvider serviceProvider)
        {
            _profileS = profileS;
            _serviceProvider = serviceProvider;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetProfile()
        {
            var response = await _profileS.Get();
            return StatusCode((int)response.StatusCode, new
            {
                message = response.Message,
                statusCode = response.StatusCode,
                data = response.Data
            });

        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto dto)
        {
            var validator = _serviceProvider.GetRequiredService<IValidator<UpdateUserProfileDto>>();
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var response = await _profileS.Update(dto);
            return StatusCode((int)response.StatusCode, response.Message);
        }
        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteProfile([FromRoute] string userId)
        {
            var response = await _profileS.Delete(userId);
            return StatusCode((int)response.StatusCode, response.Message);
        }
       
    }
}

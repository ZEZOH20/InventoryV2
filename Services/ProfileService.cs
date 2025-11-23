using InventoryV2.Dtos.ProfileDto.Requests;
using InventoryV2.Dtos.ProfileDto.Responses;
using InventoryV2.Interfaces.IServices;
using InventoryV2.Models;
using InventoryV2.Shares;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace InventoryV2.Services
{
    public class ProfileService : IProfileService
    {
        readonly UserManager<ApplicationUser> _manager;
        readonly CurrentUserService _user;
        public ProfileService(UserManager<ApplicationUser> manager , CurrentUserService user)
        {
            _manager = manager;
            _user = user;
        }
       

        public async Task<Response<GetUserProfileDto>> Get()
        {
            
            var user = await _manager.FindByIdAsync(_user.UserId());
            var userRole = _user.UserRole();
            var userProfile = new GetUserProfileDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = userRole
            };

            return user is null
                ? Response<GetUserProfileDto>.Failure($"User does not exist.")
                : Response<GetUserProfileDto>.Success(userProfile, "User retrieved successfully.");
        }
        
        // Need to Update this function build according to Identity logic that can't Change UserName if it's already taken
        public async Task<Response> Update(UpdateUserProfileDto dto)
        {
            var user = await _manager.FindByIdAsync(_user.UserId());
            if (user == null) return Response.Failure("User Doesn't Exists", HttpStatusCode.NotFound);
            
            var errors = new List<string>();
            
            if (!string.IsNullOrWhiteSpace(dto.UserName) && dto.UserName != user.UserName)
            {
                var existingUserName = await _manager.FindByNameAsync(dto.UserName);
                if (existingUserName != null)
                {
                    return Response.Failure("Username is already taken", HttpStatusCode.NotFound);
                }
                var setUserNameResult = await _manager.SetUserNameAsync(user, dto.UserName);
                if (!setUserNameResult.Succeeded)
                    errors.AddRange(setUserNameResult.Errors.Select(e => e.Description));
            }
                
            
            // Update phone number if provided and changed
            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber) && dto.PhoneNumber != user.PhoneNumber)
            {
                var setPhoneResult = await _manager.SetPhoneNumberAsync(user, dto.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                    errors.AddRange(setPhoneResult.Errors.Select(e => e.Description));
            }

            if (errors.Any())
            {
                var message = string.Join("; ", errors);
                return Response.Failure(message, HttpStatusCode.BadRequest);
            }

            return Response.Success("Profile updated successfully.");
        }

        public async Task<Response> Delete(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Response.Failure("User Id is required.", HttpStatusCode.BadRequest);
            var user = await _manager.FindByIdAsync(userId);
            if(user==null)
                return Response.Failure("User not found.", HttpStatusCode.NotFound);
            var result = await _manager.DeleteAsync(user);
            
            if (!result.Succeeded)
                return Response.Failure(string.Join("; ", result.Errors.Select(e => e.Description)));
            
            return Response.Success("User deleted successfully.");
        }
        
    }
}

using InventoryV2.Dtos.ProfileDto.Requests;
using InventoryV2.Dtos.ProfileDto.Responses;
using InventoryV2.Models;
using InventoryV2.Shares;

namespace InventoryV2.Interfaces.IServices
{
    public interface IProfileService
    {
        Task<Response<GetUserProfileDto>> Get();
        Task<Response> Update(UpdateUserProfileDto dto);
        Task<Response> Delete(string userId);
    }
}

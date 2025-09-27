using InventoryV2.Dtos.AuthDtos.Responses;

namespace InventoryV2.Interfaces.IServices
{
    public interface IOtpService
    {
        Task<SendVerificationEmailRsDto> GenerateAndStoreOtpAsync(string userId , CancellationToken cancellationToken);
        Task<bool> ValidateOtpAsync(string userKey, string otp, CancellationToken cancellationToken);
    }
}

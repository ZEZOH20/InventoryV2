using InventoryV2.Dtos.AuthDtos.Requests;
using InventoryV2.Dtos.AuthDtos.Responses;
using InventoryV2.Shares;

namespace InventoryV2.Interfaces.IServices
{
    public interface IAuthService
    {
        string Login(LoginDto dto);
        Task<Response<AuthDto>> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken);
        Task<Response<SendVerificationEmailRsDto>> SendVerificationEmailAsync(SendVerificationEmailRqDto dto, CancellationToken cancellationToken);
        Task<bool> IsVerifiedEmail(string userKey, string otp, CancellationToken cancellationToken);
    }
}

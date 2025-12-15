using System.Text.Json;
using InventoryV2.Dtos.AuthDtos.Responses;
using InventoryV2.Interfaces.IServices;
using InventoryV2.Shares;
using Microsoft.Extensions.Caching.Distributed;

namespace InventoryV2.Services.Auth
{
    public class OtpService : IOtpService
    {
        readonly IDistributedCache _cache;
        readonly IConfiguration _config;
        public OtpService(
            IDistributedCache cache,
            IConfiguration config,
            ISendEmailService sendEmailService
            ) {
            _cache = cache;
            _config = config;
        }
        public async Task<SendVerificationEmailRsDto> GenerateAndStoreOtpAsync(string userEmail, CancellationToken cancellationToken)
        {
            string userKey = Hashing.Generate(userEmail);

            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString();
            string hashedOtp = Hashing.Generate(otp);

            TimeSpan expiry = TimeSpan.FromMinutes(long.Parse(_config["Otp:expiry"])); // OTP valid for 5 minutes
            DateTimeOffset availableUntil = DateTimeOffset.UtcNow + expiry;
            var options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(availableUntil);

            await _cache.SetStringAsync(userKey, JsonSerializer.Serialize(hashedOtp),options, cancellationToken );
            // should return otp and userKey

            return new SendVerificationEmailRsDto{
                Otp = otp,
                UserKey = userKey,
                AvailableUntil = availableUntil,
            }; 
        }

        public async Task<bool> ValidateOtpAsync(string userKey, string otp, CancellationToken cancellationToken)
        {
            string cachedOtp = await _cache.GetStringAsync(userKey, cancellationToken); 
            if (cachedOtp is null) return false;

            string hashedOtp = Hashing.Generate(otp);
            cachedOtp = JsonSerializer.Deserialize<string>(cachedOtp);

            if (cachedOtp != hashedOtp) return false;
            return true;
            
        }
    }
}

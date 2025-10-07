namespace InventoryV2.Dtos.AuthDtos.Requests
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string UserKey { get; set; }
        public string Otp { get; set; }
    }
}

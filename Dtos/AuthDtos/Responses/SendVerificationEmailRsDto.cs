namespace InventoryV2.Dtos.AuthDtos.Responses
{
    public class SendVerificationEmailRsDto
    {
        public string Otp {  get; set; }
        public string UserKey { get; set; }
        public DateTimeOffset AvailableUntil {  get; set; }
    }
}

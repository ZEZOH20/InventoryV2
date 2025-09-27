namespace InventoryV2.Interfaces.IServices
{
    public interface ISendEmailService
    {
        Task<bool> SendVerificationEmail(string recipientEmail, string code);
    }
}

using FluentEmail.Core;
using FluentEmail.Core.Models;
using InventoryV2.Interfaces.IServices;

namespace InventoryV2.Services
{
    public class SendEmailService : ISendEmailService
    {
        readonly IConfiguration _config;
        readonly IFluentEmail _email;
        public SendEmailService(IConfiguration config , IFluentEmail email) { 
            _config = config;
            _email = email;
        }
        public async Task<bool> SendVerificationEmail(string recipientEmail, string code)
        {
                //.From(_config["Email:SenderEmail"])

            var email = await _email
                .To(recipientEmail)
                .Subject(_config["Email:Subject"])
                .Body(code)
                .SendAsync();

            return email.Successful;
        }
    }
}

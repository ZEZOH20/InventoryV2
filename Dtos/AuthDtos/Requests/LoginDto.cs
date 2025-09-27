using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InventoryV2.Dtos.AuthDtos.Requests
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
    
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}

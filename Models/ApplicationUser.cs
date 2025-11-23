using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace InventoryV2.Models
{
    public class ApplicationUser : IdentityUser
    {
       public Guid SuperviserId { get; set; }
       public ApplicationUser SupervisedBy { get; set; } 
       public ICollection<ApplicationUser> Subordinates { get; set; } = new List<ApplicationUser>();
    }
}

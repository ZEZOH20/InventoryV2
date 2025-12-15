using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace InventoryV2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey(nameof(SupervisedBy))]
        public string? SupervisorId { get; set; }
        public ApplicationUser SupervisedBy { get; set; }  =  null!;
        
        public ICollection<ApplicationUser> Subordinates { get; set; } = [];

        [ForeignKey("Warehouse")]
        public int? WorkingWarehouseNumber { get; set; }
        public Warehouse Working_Warehouse { get; set; } //Navigation Property
       
        public ICollection<Warehouse> Owner_Warehouses { get; set; } = []; //Navigation
        public ICollection<Warehouse> Managed_Warehouses { get; set; } = []; //Navigation
    }
}

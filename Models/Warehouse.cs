
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryV2.Models
{
    public class Warehouse:AuditableEntity
    {
        [Key]
        public int Number {  get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        public string? Region {  get; set; }
        [MaxLength(100)]
        public string? City { get; set; }
        [MaxLength(100)]
        public string? Street { get; set; }

        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; } = null!; //Navigation Property

        [ForeignKey(nameof(Manager))]
        public string ManagerId { get; set; }
        public ApplicationUser Manager { get; set; } = null!; //Navigation Property

        public ICollection<ApplicationUser> Employees { get; set; } = [];

        public ICollection<Warehouse_Product> Warehouse_Products { get; set; } = [];

    }
}

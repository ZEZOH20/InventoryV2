using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryV2.Models
{
    public class Release_Order:AuditableEntity
    {
        [Key]
        public int Number { get; set; }

        [ForeignKey("Customer")]
        public required int Customer_ID { get; set; }

        [ForeignKey("Warehouse")]
        public required int War_Number { get; set; }

        // [DataType(DataType.Date)]
        // public DateTime R_Date { get; set; }


        //Navigation
        public Customer Customer { get; set; } = null!;
        public Warehouse Warehouse { get; set; } = null!;
        public ICollection<RO_Product> RO_Products { get; set; } = [];
    }
}

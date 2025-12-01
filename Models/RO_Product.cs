using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryV2.Models
{
    public class RO_Product:AuditableEntity
    {
        [Range(0, double.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public double RO_Amount { get; set; } //

        [EnumDataType(typeof(WeightUnit), ErrorMessage = "Unit Doesn't Acceptable")]
        [MaxLength(50)]
        public required string RO_Unit { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public double RO_Price { get; set; }


        [ForeignKey("Release_Order")]
        public int RO_Id { get; set; }

        [ForeignKey("Product")]
        public int Product_Id { get; set; }


        //Navigation
        public Release_Order Release_Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryV2.Models

{
    public enum WeightUnit
    {
        Kilogram,
        Ton,
        Pound,
        Pack,
        Dozen,
        Liter,
        Piece
    }
    public class Product:AuditableEntity
    {
        [Key]
        public int Code { get; set; }
        
        [MaxLength(100)]
        public required string Name { get; set; }

        [EnumDataType(typeof(WeightUnit),ErrorMessage = "Unit Doesn't Acceptable")]
        [MaxLength(50)]
        public required string Unit { get; set; }

        public ICollection<Warehouse_Product> Warehouse_Products { get; set; } = [];
        public ICollection<SO_Product> SO_Products { get; set; } = [];
        public ICollection<RO_Product> RO_Products { get; set; } = [];
        public ICollection<TO_Product> TO_Products { get; set; } = [];
    }
}

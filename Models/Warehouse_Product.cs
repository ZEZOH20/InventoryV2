 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InventoryV2.Models
{
    public class Warehouse_Product:AuditableEntity
    {
        [ForeignKey("Product")]
        public int Product_Id { get; set; }

        [ForeignKey("Warehouse")]
        public int War_Id { get; set; }

        [ForeignKey("Supplier")]
        public int Supplier_ID { get; set; }

        [Required(ErrorMessage = "Please add Manufacturing (MD) Date")]
        [DataType(DataType.Date)]
        public DateTime MFD { get; set; }

        [Required(ErrorMessage = "Please add Expire (EXP) Date")]
        [DataType(DataType.Date)]
        public DateTime EXP { get; set; }

        // [DataType(DataType.Date)]
        // public DateTime Store_Date { get; set; } 

        [Range(0, double.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public double Total_Amount { get; set; } //

        [Range(0, double.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public double Total_Price { get; set; }

        
        //Navigation
        public Product Product { get; set; } = null!;
        public Warehouse Warehouse { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;

    }
}

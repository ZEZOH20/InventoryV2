namespace InventoryV2.Models
{
    public class Supplier: Person
    {
        public ICollection<Warehouse_Product> Warehouse_Products { get; set; } = [];

        public Supply_Order Supply_Order { get; set; } = null!;
    }
}

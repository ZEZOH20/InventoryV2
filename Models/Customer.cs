

namespace InventoryV2.Models
{
    public class Customer: Person
    {
        public Release_Order Release_Order { get; set; } = null!;
    }
}

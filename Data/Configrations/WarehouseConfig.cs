using InventoryV2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace InventoryV2.Data.Configrations
{
    public class WarehouseConfig : IEntityTypeConfiguration<Warehouse>
    {
       
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasIndex(p=>p.Number).IsUnique();
            // Each warehouse has only one manager
            builder.HasIndex(w=>w.ManagerId).IsUnique();
        }
    }
}

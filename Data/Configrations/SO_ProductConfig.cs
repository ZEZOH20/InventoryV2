using InventoryV2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryV2.Data.Configrations
{
    public class SO_ProductConfig : IEntityTypeConfiguration<SO_Product>
    {
        public void Configure(EntityTypeBuilder<SO_Product> builder)
        {
            // Relation between SO_Product and Supply_Order
            builder.HasOne(so => so.Supply_Order)
                .WithMany(s => s.SO_Products)
                .HasForeignKey(so => so.SO_Number);

            // Relation between SO_Product and Product
            builder.HasOne(so => so.Product)
                .WithMany(p => p.SO_Products)
                .HasForeignKey(so => so.Product_Code);
        }
    }
}
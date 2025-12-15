using InventoryV2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryV2.Data.Configrations
{
    public class RO_ProductConfig : IEntityTypeConfiguration<RO_Product>
    {
        public void Configure(EntityTypeBuilder<RO_Product> builder)
        {
            // Relation between RO_Product and Release_Order
            builder.HasOne(ro => ro.Release_Order)
                .WithMany(r => r.RO_Products)
                .HasForeignKey(ro => ro.RO_Number);

            // Relation between RO_Product and Product
            builder.HasOne(ro => ro.Product)
                .WithMany(p => p.RO_Products)
                .HasForeignKey(ro => ro.Product_Code);
        }
    }
}
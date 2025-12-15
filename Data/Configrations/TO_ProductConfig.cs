using InventoryV2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryV2.Data.Configrations
{
    public class TO_ProductConfig : IEntityTypeConfiguration<TO_Product>
    {
        public void Configure(EntityTypeBuilder<TO_Product> builder)
        {
            // Relation between TO_Product and Transfer_Order
            builder.HasOne(to => to.Transfer_Order)
                .WithMany(t => t.TO_Products)
                .HasForeignKey(to => to.TO_Number);

            // Relation between TO_Product and Product
            builder.HasOne(to => to.Product)
                .WithMany(p => p.TO_Products)
                .HasForeignKey(to => to.Product_Code);
        }
    }
}
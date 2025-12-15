using InventoryV2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryV2.Data.Configrations
{
    public class Warehouse_ProductConfig : IEntityTypeConfiguration<Warehouse_Product>
    {
        public void Configure(EntityTypeBuilder<Warehouse_Product> builder)
        {
            // Configure relationships
            builder.HasOne(sp => sp.Product)
                .WithMany(p => p.Warehouse_Products)
                .HasForeignKey(sc => sc.Product_Code);

            builder.HasOne(sp => sp.Warehouse)
                .WithMany(w => w.Warehouse_Products)
                .HasForeignKey(sp => sp.War_Number);

            builder.HasOne(sp => sp.Supplier)
                .WithMany(s => s.Warehouse_Products)
                .HasForeignKey(sp => sp.Supplier_ID);

            // Table name
            builder.ToTable("Warehouse_Products");
            
            // Required properties
            builder.Property(sp => sp.Product_Code).IsRequired();
        }
    }
}
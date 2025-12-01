using InventoryV2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryV2.Data.Configrations
{
    public class SO_ProductConfig : IEntityTypeConfiguration<SO_Product>
    {
        ModelBuilder _ModelBuilder;
        public SO_ProductConfig(ModelBuilder ModelBuilder)
        {
            _ModelBuilder = ModelBuilder;
            CreateTable();
        }

        private void CreateTable()
        {
            //Relation between SO_Product and Supply_Order
            _ModelBuilder.Entity<SO_Product>()
                .HasOne(so => so.Supply_Order)
                .WithMany(s => s.SO_Products)
                .HasForeignKey(so => so.SO_Id);

            //Relation between SO_Product and Product
            _ModelBuilder.Entity<SO_Product>()
               .HasOne(so => so.Product)
               .WithMany(p => p.SO_Products)
               .HasForeignKey(so => so.Product_Id);
        }

        public void Configure(EntityTypeBuilder<SO_Product> builder)
        {
            //
        }
    }
}

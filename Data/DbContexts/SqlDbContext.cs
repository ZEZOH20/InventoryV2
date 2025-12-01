
using InventoryV2.Data.Configrations;
using InventoryV2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryV2.Data.DbContexts
{
    public class SqlDbContext: IdentityDbContext<ApplicationUser>
    {
         public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<AuditableEntity>()
                .HasQueryFilter(e => !e.IsDeleted);
            
            builder.ApplyConfiguration(new WarehouseConfig());
            builder.ApplyConfiguration(new Warehouse_ProductConfig(builder));
            builder.ApplyConfiguration(new SO_ProductConfig(builder));
            builder.ApplyConfiguration(new RO_ProductConfig(builder));
            builder.ApplyConfiguration(new TO_ProductConfig(builder));

            builder.Entity<Product>()
                .HasIndex(p => p.Code)
                .IsUnique();
            
            builder.Entity<Release_Order>()
                .HasIndex(p=>p.Number)
                .IsUnique();
            
            builder.Entity<Supply_Order>()
                .HasIndex(p=>p.Number)
                .IsUnique();
            
            builder.Entity<Transfer_Order>()
                .HasIndex(p=>p.Number)
                .IsUnique();
            
            builder.Entity<Transfer_Order>()
                .HasOne(t => t.FromWarehouse)
                .WithMany()
                .HasForeignKey(t => t.From)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure ApplicationUser relationships
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.SupervisedBy)
                .WithMany(u => u.Subordinates)
                .HasForeignKey(u => u.SupervisorId);
            
            builder.Entity<Warehouse>()
                .HasOne(w => w.Owner)
                .WithMany(u => u.Owner_Warehouses)
                .HasForeignKey(w => w.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Warehouse>()
                .HasOne(w => w.Manager)
                .WithMany(u => u.Managed_Warehouses)
                .HasForeignKey(w => w.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<Warehouse>()
                .HasMany(w => w.Employees)
                .WithOne(u => u.Working_Warehouse)
                .HasForeignKey(u => u.WorkingWarehouseId);
        }
     
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse_Product> Warehouse_Products { get; set; }
        public DbSet<Supply_Order> Supply_Orders { get; set; }
        public DbSet<Release_Order> Release_Orders { get; set; }
        public DbSet<Transfer_Order> Transfer_Orders { get; set; }
        public DbSet<SO_Product> SO_Products { get; set; }
        public DbSet<RO_Product> RO_Products { get; set; }
        public DbSet<TO_Product> TO_Products { get; set; }
    }
}



//protected override void OnModelCreating(ModelBuilder builder)
//{
//    base.OnModelCreating(builder);

//    builder.Entity<ApplicationUser>()
//        .HasIndex(i => i.NormalizedUserName)
//        .IsUnique(false);

//}

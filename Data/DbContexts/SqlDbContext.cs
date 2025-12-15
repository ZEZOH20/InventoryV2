using InventoryV2.Data.Configrations;
using InventoryV2.Interfaces.IServices;
using InventoryV2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryV2.Data.DbContexts
{
    public class SqlDbContext : IdentityDbContext<ApplicationUser>
    {
        readonly ICurrentUserService _currentUserService;
        public SqlDbContext(DbContextOptions<SqlDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply configurations - NO ModelBuilder passed!
            builder.ApplyConfiguration(new WarehouseConfig());
            builder.ApplyConfiguration(new Warehouse_ProductConfig());
            builder.ApplyConfiguration(new SO_ProductConfig());
            builder.ApplyConfiguration(new RO_ProductConfig());
            builder.ApplyConfiguration(new TO_ProductConfig());

            // Query filters for soft delete
            builder.Entity<Supplier>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Customer>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Warehouse>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Product>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Warehouse_Product>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Supply_Order>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Release_Order>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<Transfer_Order>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<SO_Product>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<RO_Product>().HasQueryFilter(e => !e.IsDeleted);
            builder.Entity<TO_Product>().HasQueryFilter(e => !e.IsDeleted);

            // Unique indexes
            builder.Entity<Product>().HasIndex(p => p.Code).IsUnique();
            builder.Entity<Release_Order>().HasIndex(p => p.Number).IsUnique();
            builder.Entity<Supply_Order>().HasIndex(p => p.Number).IsUnique();
            builder.Entity<Transfer_Order>().HasIndex(p => p.Number).IsUnique();
            builder.Entity<Warehouse>().HasIndex(p => p.Number).IsUnique();

            // Transfer order configuration
            builder.Entity<Transfer_Order>()
                .HasOne(t => t.FromWarehouse)
                .WithMany()
                .HasForeignKey(t => t.From)
                .OnDelete(DeleteBehavior.Restrict);

            // ApplicationUser relationships
            builder.Entity<ApplicationUser>()
                .HasOne(u => u.SupervisedBy)
                .WithMany(u => u.Subordinates)
                .HasForeignKey(u => u.SupervisorId);

            // Warehouse relationships
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
                .HasForeignKey(u => u.WorkingWarehouseNumber);
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach(var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                var entity = entry.Entity;
                var userId = _currentUserService.UserId;
                var ip = _currentUserService.UserIp;
                if(entry.State == EntityState.Added)
                {
                    entity.SetCreated(userId, ip);
                }
                else if(entry.State == EntityState.Modified)
                {
                    entity.SetUpdated(userId, ip);
                }
                else if(entry.State == EntityState.Deleted)
                {
                    entity.SoftDelete(userId, ip); // Convert hard delete to soft delete
                    entry.State = EntityState.Modified; // Prevent actual DB delete
                }
            }
          return await base.SaveChangesAsync(cancellationToken);   
        }
    }
}
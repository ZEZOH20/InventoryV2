
using InventoryV2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryV2.Data.DbContexts
{
    public class SqlDbContext: IdentityDbContext<ApplicationUser>
    {
         public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) {
           
        }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<ApplicationUser>()
        //        .HasIndex(i => i.NormalizedUserName)
        //        .IsUnique(false);

        //}
    }
}

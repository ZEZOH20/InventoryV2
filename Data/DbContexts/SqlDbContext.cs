
using InventoryV2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryV2.Data.DbContexts
{
    public class SqlDbContext: IdentityDbContext<ApplicationUser>
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) {
            
        }
    }
}

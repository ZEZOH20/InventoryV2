using InventoryV2.Models;
using Microsoft.AspNetCore.Identity;

namespace InventoryV2.Seeders
{
    public struct SystemRoles
    {
        public const string Owner = "Owner";
        public const string Manager = "Manager";
        public const string Employee = "Employee";

        public static List<string> All = new List<string> {Manager, Owner, Employee };
    }
    public static class IdentitySeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // roles to seed
         /*   var roles = new[] { "Owner", "Manager", "Employee" };*/

            // Seed roles
            foreach (var role in SystemRoles.All) { 
                
                if(! await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            
            }

        }
    }
}

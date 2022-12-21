using EnumsNET;
using Microsoft.AspNetCore.Identity;
using Restaurant.Areas.Admin.Models;

namespace Restaurant.Data
{

    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Staff.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Public.ToString()));

        }
    }
}




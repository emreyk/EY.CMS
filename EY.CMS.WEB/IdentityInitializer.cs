using EY.CMS.CORE.Models;
using Microsoft.AspNetCore.Identity;

namespace EY.CMS.WEB
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("admin");
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "admin" });
            }

            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@eycms.com"
                };
                await userManager.CreateAsync(user, "1234*");
                await userManager.AddToRoleAsync(user, "admin");
            }
        }
    }
}

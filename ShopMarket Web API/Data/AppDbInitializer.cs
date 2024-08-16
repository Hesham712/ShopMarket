using Microsoft.AspNetCore.Identity;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole<int>(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole<int>(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@martket.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        Name = "Admin",
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@1");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}

using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity;

public static class Utils
{
    public static async Task AddAdminTools(this IServiceProvider services)
    {
        var users = services.GetRequiredService<UserManager<AppUser>>();
        var roles = services.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roles.RoleExistsAsync("Admin"))
        {
            await roles.CreateAsync(new IdentityRole("Admin"));
        }

        var admin = await users.FindByNameAsync("admin");
        if (admin == null)
        {
            admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                CreatedDate = DateTime.UtcNow
            };

            var result = await users.CreateAsync(admin, "admin");

            if (result.Succeeded)
            {
                await users.AddToRoleAsync(admin, "Admin");
            }
            else
            {
                throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors));
            }
        }
        else
        {
            if (!await users.IsInRoleAsync(admin, "Admin"))
            {
                await users.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
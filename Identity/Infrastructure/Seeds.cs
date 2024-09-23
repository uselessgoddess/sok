namespace Identity.Infrastructure;

using Identity.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class Seeds
{
    public static void AddAdminSeeds(this ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "d87ed9d0-af1c-4954-8cc9-926413b38423",
            Name = "Admin",
            NormalizedName = "ADMIN",
        });

        builder.Entity<AppUser>().HasData(new AppUser
        {
            Id = "34a7f629-76c0-463d-9385-c36fee02cb9e",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@admin.com",
            NormalizedEmail = "ADMIN@ADMIN.COM",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEPkax+Se8EFpvGthXcnyH515Fj2j4mNiEbZvpoxMMyOTdFPl074N+UYWYVV5JlQUKw==",
            CreatedDate = DateTime.UtcNow,
        });

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = "d87ed9d0-af1c-4954-8cc9-926413b38423",
            UserId = "34a7f629-76c0-463d-9385-c36fee02cb9e",
        });
    }
}
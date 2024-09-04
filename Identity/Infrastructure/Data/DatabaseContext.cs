namespace Identity.Infrastructure.Data;

using Identity.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.AddAdminSeeds();

        base.OnModelCreating(builder);
    }
}
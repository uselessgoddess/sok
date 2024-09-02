using Identity.Infrastructure.Models;

namespace Identity.Infrastructure.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class DatabaseCx(DbContextOptions<DatabaseCx> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.AddAdminSeeds();

        base.OnModelCreating(builder);
    }
}
using Identity.Core.Interfaces;
using Identity.Core.Models;
using Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public class RefreshRepository(DatabaseContext context) : IRefreshRepository
{
    public async Task<RefreshToken?> FromUser(string user, CancellationToken cancellation)
    {
        return await context.RefreshTokens.FirstOrDefaultAsync(refresh => refresh.UserId == user,
            cancellationToken: cancellation);
    }
    
    public async Task<RefreshToken?> FromToken(string token, CancellationToken cancellation)
    {
        return await context.RefreshTokens.FirstOrDefaultAsync(refresh => refresh.Token == token,
            cancellationToken: cancellation);
    }

    public async Task Add(RefreshToken token, CancellationToken cancellation)
    {
        context.RefreshTokens.Add(token);

        await context.SaveChangesAsync(cancellation);
    }

    public async Task Update(RefreshToken token, CancellationToken cancellation)
    {
        context.RefreshTokens.Update(token);

        await context.SaveChangesAsync(cancellation);
    }
}
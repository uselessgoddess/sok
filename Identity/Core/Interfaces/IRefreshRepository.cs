using Identity.Core.Models;

namespace Identity.Core.Interfaces;

public interface IRefreshRepository
{
    Task<RefreshToken?> FromUser(string user, CancellationToken cancellation);
    
    Task<RefreshToken?> FromToken(string token, CancellationToken cancellation);

    Task Add(RefreshToken token, CancellationToken cancellation);

    Task Update(RefreshToken token, CancellationToken cancellation);
}
using Identity.Core.Models;

namespace Identity.Core.Interfaces;

public interface ITokenService
{
    public string Access(AppUser user, IEnumerable<Role> roles);

    public RefreshToken Refresh(string user);
}
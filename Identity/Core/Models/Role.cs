namespace Identity.Infrastructure.Models;

using Microsoft.AspNetCore.Identity;

public enum Role
{
    Admin
}

public static class RoleImpl
{
    public static async Task<IEnumerable<Role>> GetEnumRolesAsync<T>(this UserManager<T> users, T user)
        where T : class => (await users.GetRolesAsync(user)).Select(Enum.Parse<Role>);
}
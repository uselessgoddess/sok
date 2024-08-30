using Identity.Infrastructure.Data;
using Identity.Infrastructure.Services;

namespace Identity.Core.Handlers;

using Identity.Core.Commands;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class LoginHandler(
    UserManager<AppUser> users,
    SignInManager<AppUser> sign,
    TokenService token,
    DatabaseCx cx) : IRequestHandler<Login, TokensPair?>
{
    public async Task<TokensPair?> Handle(Login req, CancellationToken cancellationToken)
    {
        var result = await sign.PasswordSignInAsync(req.Username, req.Password, isPersistent: false,
            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new UnauthorizedException("invalid username or password");
        }

        var user = await users.FindByNameAsync(req.Username);
        var roles = await users.GetEnumRolesAsync(user);
        var refresh = token.Refresh(user.Id);

        cx.RefreshTokens.Add(refresh);
        await cx.SaveChangesAsync(cancellationToken);

        return new TokensPair { Access = token.Access(user, roles), Refresh = refresh.Token };
    }
}
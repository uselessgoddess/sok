using Identity.Commands;
using Identity.Data;
using Identity.Models;
using Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Handlers;

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

        if (!result.Succeeded) return null;

        var user = await users.FindByNameAsync(req.Username);
        var refresh = token.Refresh(user.Id);
        
        cx.RefreshTokens.Add(refresh);
        await cx.SaveChangesAsync(cancellationToken);

        return new TokensPair { Access = token.Access(user), Refresh = refresh.Token };
    }
}
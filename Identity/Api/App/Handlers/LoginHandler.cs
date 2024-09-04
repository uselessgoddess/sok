namespace Identity.Api.App.Handlers;

using Identity.Api.Commands;
using Identity.Core;
using Identity.Core.Models;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class LoginHandler(
    UserManager<AppUser> users,
    SignInManager<AppUser> sign,
    TokenService token,
    DatabaseContext context) : IRequestHandler<Login, TokensPair?>
{
    public async Task<TokensPair?> Handle(Login req, CancellationToken cancellationToken)
    {
        var result =
            await sign.PasswordSignInAsync(req.Username, req.Password, isPersistent: false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new BadRequestException("invalid username or password");
        }

        var user = await users.FindByNameAsync(req.Username);
        var roles = await users.GetEnumRolesAsync(user);
        var refresh = token.Refresh(user.Id);

        context.RefreshTokens.Add(refresh);
        await context.SaveChangesAsync(cancellationToken);

        return new TokensPair { Access = token.Access(user, roles), Refresh = refresh.Token };
    }
}
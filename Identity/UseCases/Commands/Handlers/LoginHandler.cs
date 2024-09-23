namespace Identity.UseCases.Commands.Handlers;

using Identity.Core.Exceptions;
using Identity.Api.Commands;
using Identity.Core;
using Identity.Core.Interfaces;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class LoginHandler(
    UserManager<AppUser> users,
    SignInManager<AppUser> sign,
    ITokenService token,
    IRefreshRepository refresh) : IRequestHandler<Login, TokensPair?>
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
        var newRefresh = token.Refresh(user.Id);

        await refresh.Add(newRefresh, cancellationToken);

        return new TokensPair { Access = token.Access(user, roles), Refresh = newRefresh.Token };
    }
}
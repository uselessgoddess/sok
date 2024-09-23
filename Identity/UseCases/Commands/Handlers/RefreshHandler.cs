namespace Identity.UseCases.Commands.Handlers;

using Identity.Api.Commands;
using Identity.Core.Exceptions;
using Identity.Core.Interfaces;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class RefreshHandler(
    UserManager<AppUser> users,
    ITokenService token,
    IRefreshRepository refresh) : IRequestHandler<Refresh, TokensPair?>
{
    public async Task<TokensPair?> Handle(Refresh req, CancellationToken cancellationToken)
    {
        var old = await refresh.FromToken(req.Token, cancellationToken);

        if (old == null || old.Expire < DateTime.Now || old.IsRevoked)
        {
            throw new BadRequestException();
        }

        var user = await users.FindByIdAsync(old.UserId);
        var roles = await users.GetEnumRolesAsync(user);

        var newRefresh = token.Refresh(user.Id);
        old.IsRevoked = true;

        await refresh.Update(old, cancellationToken);

        await refresh.Add(newRefresh, cancellationToken);

        return new TokensPair
        {
            Access = token.Access(user, roles),
            Refresh = newRefresh.Token,
        };
    }
}
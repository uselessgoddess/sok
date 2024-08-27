using Identity.Commands;
using Identity.Data;
using Identity.Models;
using Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Handlers;

public class RefreshHandler(
    UserManager<AppUser> users,
    SignInManager<AppUser> sign,
    TokenService token,
    DatabaseCx cx) : IRequestHandler<Refresh, TokensPair?>
{
    public async Task<TokensPair?> Handle(Refresh req, CancellationToken cancellationToken)
    {
        var old =
            await cx.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == req.Token,
                cancellationToken: cancellationToken);

        if (old == null || old.Expire < DateTime.Now || old.IsRevoked)
            return null;

        var user = await users.FindByIdAsync(old.UserId);
        if (user == null)
            return null;

        var refresh = token.Refresh(user.Id);
        old.IsRevoked = true;
        cx.RefreshTokens.Update(old);
        cx.RefreshTokens.Add(refresh);
        await cx.SaveChangesAsync(cancellationToken);

        return new TokensPair
        {
            Access = token.Access(user),
            Refresh = refresh.Token
        };
    }

    public Task<TokensPair?> Handle(Login request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using Identity.Infrastructure.Models;

namespace Identity.Core.Handlers;

using Identity.Core.Commands;
using Identity.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class RevokeHandler(
    UserManager<AppUser> users,
    DatabaseCx cx) : IRequestHandler<Revoke>
{
    public async Task Handle(Revoke req, CancellationToken cancellationToken)
    {
        var user = await users.FindByNameAsync(req.Username) ?? throw new NotFoundException();
        var refresh =
            await cx.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == user.Id, cancellationToken);

        refresh!.IsRevoked = true;

        cx.RefreshTokens.Update(refresh);
        await cx.SaveChangesAsync(cancellationToken);
    }
}
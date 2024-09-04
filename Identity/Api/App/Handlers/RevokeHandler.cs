namespace Identity.Api.App.Handlers;

using Identity.Api.Commands;
using Identity.Core;
using Identity.Core.Exceptions;
using Identity.Core.Models;
using Identity.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class RevokeHandler(
    UserManager<AppUser> users,
    DatabaseContext context) : IRequestHandler<Revoke>
{
    public async Task Handle(Revoke req, CancellationToken cancellationToken)
    {
        var user = await users.FindByNameAsync(req.Username) ?? throw new NotFoundException();
        var refresh =
            await context.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == user.Id, cancellationToken);

        refresh!.IsRevoked = true;

        context.RefreshTokens.Update(refresh);
        await context.SaveChangesAsync(cancellationToken);
    }
}
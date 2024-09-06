using Identity.Api.Commands;
using Identity.Core;
using Identity.Core.Interfaces;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace UseCases.Commands.Handlers;

public class RevokeHandler(
    UserManager<AppUser> users,
    IRefreshRepository refresh) : IRequestHandler<Revoke>
{
    public async Task Handle(Revoke req, CancellationToken cancellationToken)
    {
        var user = await users.FindByNameAsync(req.Username) ?? throw new NotFoundException();

        var token = await refresh.FromUser(user.Id, cancellationToken);

        token!.IsRevoked = true;

        await refresh.Update(token, cancellationToken);
    }
}
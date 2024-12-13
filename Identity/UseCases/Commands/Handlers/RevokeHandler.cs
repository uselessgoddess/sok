namespace Identity.UseCases.Commands.Handlers;

using Identity.Api.Commands;
using Identity.Core.Exceptions;
using Identity.Core.Interfaces;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
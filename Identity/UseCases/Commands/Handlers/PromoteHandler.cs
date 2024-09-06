namespace Identity.Api.App.Handlers;

using Identity.Api.Commands;
using Identity.Core;
using Identity.Core.Exceptions;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class PromoteHandler(
    UserManager<AppUser> users) : IRequestHandler<Promote>
{
    public async Task Handle(Promote req, CancellationToken cancellationToken)
    {
        var user = await users.FindByNameAsync(req.Username) ?? throw new NotFoundException();
        await users.AddToRolesAsync(user, req.Roles);
    }
}
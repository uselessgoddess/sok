using Identity.Infrastructure.Models;

namespace Identity.Core.Handlers;

using Identity.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class DemoteHandler(
    UserManager<AppUser> users) : IRequestHandler<Demote>
{
    public async Task Handle(Demote req, CancellationToken cancellationToken)
    {
        var user = await users.FindByIdAsync(req.Id) ?? throw new NotFoundException();

        await users.RemoveFromRolesAsync(user, req.Roles.Select(r => r.ToString()));
    }
}
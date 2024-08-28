using Identity.Commands;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Handlers;

public class DemoteHandler(
    UserManager<AppUser> users) : IRequestHandler<Demote>
{
    public async Task Handle(Demote req, CancellationToken cancellationToken)
    {
        var user = await users.FindByIdAsync(req.Id) ?? throw new NotFoundException();
        await users.RemoveFromRolesAsync(user, req.Roles.Select(r => r.ToString()));
    }
}
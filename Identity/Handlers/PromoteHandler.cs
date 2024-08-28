using Identity.Commands;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Identity.Handlers;

public class PromoteHandler(
    UserManager<AppUser> users) : IRequestHandler<Promote>
{
    public async Task Handle(Promote req, CancellationToken cancellationToken)
    {
        var user = await users.FindByIdAsync(req.Id) ?? throw new NotFoundException();
        await users.AddToRolesAsync(user, req.Roles.Select(r => r.ToString()));
    }
}
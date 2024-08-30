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
        var user = await users.FindByNameAsync(req.Username) ?? throw new NotFoundException();
        await users.AddToRolesAsync(user, req.Roles);
    }
}
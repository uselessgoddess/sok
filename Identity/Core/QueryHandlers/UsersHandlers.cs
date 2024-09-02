using Identity.Core;

namespace Identity.Handlers;

using Identity.Core.Models;
using Identity.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserHandler(
    UserManager<AppUser> users) : IRequestHandler<User, AppUser>
{
    public async Task<AppUser> Handle(User req, CancellationToken cancellationToken)
    {
        return await users.FindByNameAsync(req.Username) ?? throw new NotFoundException();
    }
}

public class UsersHandler(
    UserManager<AppUser> users) : IRequestHandler<Users, List<AppUser>>
{
    public async Task<List<AppUser>> Handle(Users req, CancellationToken cancellationToken)
    {
        return await users.Users
            .Skip((int)(req.Page * req.Size))
            .Take((int)req.Size)
            .ToListAsync(cancellationToken);
    }
}
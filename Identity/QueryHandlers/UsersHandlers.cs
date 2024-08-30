using Identity.Commands;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Handlers;

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
            .Skip((int)(req.Page * req.Count))
            .Take((int)req.Count)
            .ToListAsync(cancellationToken);
    }
}
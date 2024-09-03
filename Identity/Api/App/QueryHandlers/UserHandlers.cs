namespace Identity.Core.QueryHandlers;

using Identity.Core.Queries;
using Identity.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class UserHandler(
    UserManager<AppUser> users) : IRequestHandler<User, AppUser>
{
    public async Task<AppUser> Handle(User req, CancellationToken cancellationToken)
    {
        return await users.FindByNameAsync(req.Username) ?? throw new NotFoundException();
    }
}
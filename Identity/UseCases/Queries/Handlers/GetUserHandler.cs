namespace Identity.Api.App.QueryHandlers;

using Identity.Api.Queries;
using Identity.Core.Exceptions;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class GetUserHandler(
    UserManager<AppUser> users) : IRequestHandler<User, AppUser>
{
    public async Task<AppUser> Handle(User req, CancellationToken cancellationToken)
    {
        return await users.FindByNameAsync(req.Username) ?? throw new NotFoundException();
    }
}
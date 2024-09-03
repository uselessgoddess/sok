using Identity.Core;

namespace Identity.Api.App.Handlers;

using Identity.Api.Commands;
using Identity.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class RegisterHandler(UserManager<AppUser> users) : IRequestHandler<Register>
{
    public async Task Handle(Register req, CancellationToken cancellationToken)
    {
        if (await users.FindByEmailAsync(req.Email) != null)
        {
            throw new BadRequestException("user with the same email already exists");
        }

        var user = new AppUser
            { UserName = req.Username, Email = req.Email, CreatedDate = DateTime.UtcNow };

        if (!(await users.CreateAsync(user, req.Password)).Succeeded)
        {
            throw new AlreadyExistsException("user already exists");
        }
    }
}
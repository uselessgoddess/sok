using Identity.Infrastructure.Models;

namespace Identity.Core.Handlers;

using Identity.Core.Commands;
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
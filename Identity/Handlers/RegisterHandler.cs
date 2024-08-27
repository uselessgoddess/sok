using Identity.Commands;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Handlers;

public class RegisterHandler(UserManager<AppUser> cx) : IRequestHandler<Register, EmptyResult?>
{
    public async Task<EmptyResult?> Handle(Register req, CancellationToken cancellationToken)
    {
        var user = new AppUser
            { UserName = req.Username, Email = req.Email, CreatedDate = DateTime.UtcNow };
        return (await cx.CreateAsync(user, req.Password)).Succeeded ? new EmptyResult() : null;
    }
}
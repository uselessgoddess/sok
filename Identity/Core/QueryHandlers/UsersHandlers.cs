﻿namespace Identity.Core.QueryHandlers;

using Identity.Core.Queries;
using Identity.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
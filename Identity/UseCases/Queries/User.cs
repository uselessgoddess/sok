﻿namespace Identity.Api.Queries;

using Identity.Core.Models;
using MediatR;

public class User : IRequest<AppUser>
{
    public string Username { get; set; }
}
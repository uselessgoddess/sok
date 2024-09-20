﻿using Identity.Infrastructure;

namespace Identity.Api.Controllers;

using Identity.Api.App.Controllers;
using Identity.Api.Commands;
using Identity.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UsersController(IMediator mediator)
    : ControllerBase
{
    [HttpPut("promote")]
    [Authorize(Policy.Admin)]
    [Walidate]
    public async Task<IActionResult> Promote([FromBody] Promote req)
    {
        return await req.RouteEmpty(mediator);
    }

    [HttpDelete("demote")]
    [Authorize(Policy.Admin)]
    [Walidate]
    public async Task<IActionResult> Demote([FromBody] Demote req)
    {
        return await req.RouteEmpty(mediator);
    }

    [HttpGet("{id}")]
    [Walidate]
    public async Task<IActionResult> User(string id)
    {
        return await new User { Username = id }.Route(mediator);
    }

    // TODO: later use automapper
    [HttpGet("users")]
    [Walidate]
    public async Task<IActionResult> Users(uint page, uint size)
    {
        return await new Users { Page = page, Size = size }.Route(mediator);
    }
}
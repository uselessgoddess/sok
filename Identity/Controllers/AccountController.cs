using Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController(IMediator mediator)
    : ControllerBase
{
    [HttpPost("register")]
    [Walidate]
    public async Task<IActionResult> Register([FromBody] Register req)
    {
        return await req.Route(mediator).Else(() => BadRequest("name already taken")).Result();
    }

    [HttpPost("login")]
    [Walidate]
    public async Task<IActionResult> Login([FromBody] Login req)
    {
        return await req.Route(mediator).Else(() => Unauthorized("username or password is incorrect")).Result();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] Refresh req)
    {
        return await req.Route(mediator).Else(() => Unauthorized("invalid refresh token")).Result();
    }
}
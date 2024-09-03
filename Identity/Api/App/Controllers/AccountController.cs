namespace Identity.Api.App.Controllers;

using Identity.Api.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/account")]
public class AccountController(IMediator mediator)
    : ControllerBase
{
    [HttpPost("register")]
    [Walidate]
    public async Task<IActionResult> Register([FromBody] Register req)
    {
        return await req.RouteEmpty(mediator);
    }

    [HttpPost("login")]
    [Walidate]
    public async Task<IActionResult> Login([FromBody] Login req)
    {
        return await req.Route(mediator);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] Refresh req)
    {
        return await req.Route(mediator);
    }

    [HttpPost]
    [Authorize]
    [Route("revoke")]
    public async Task<IActionResult> Refresh()
    {
        return await new Revoke { Username = HttpContext.User.Identity!.Name! }.RouteEmpty(mediator);
    }
}
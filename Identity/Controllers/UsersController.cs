using Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers;

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
    
    [HttpGet("user")]
    [Walidate]
    public async Task<IActionResult> Demote([FromBody] User req)
    {
        return await req.Route(mediator);
    }
    
    [HttpGet("users")]
    [Walidate]
    public async Task<IActionResult> Demote([FromBody] Users req)
    {
        return await req.Route(mediator);
    }
}
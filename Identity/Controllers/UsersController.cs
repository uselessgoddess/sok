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
    [HttpPost("promote")]
    [Authorize(Roles = "Admin")]
    [Walidate]
    public async Task<IActionResult> Promote([FromBody] Promote req)
    {
        return await req.RouteEmpty(mediator);
    }
    
    [HttpPost("demote")]
    [Authorize(Roles = "Admin")]
    [Walidate]
    public async Task<IActionResult> Demote([FromBody] Demote req)
    {
        return await req.RouteEmpty(mediator);
    }
}
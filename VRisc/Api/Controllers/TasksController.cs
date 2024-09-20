namespace VRisc.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VRisc.Core.Exceptions;
using VRisc.UseCases.Handlers;

[ApiController]
[Authorize]
[Route("api/tasks")]
public class TasksController(TasksHandler handler) : ControllerBase
{
    private string AuthUser => HttpContext.User.Identity!.Name!;

    [HttpPost("/start")]
    public async Task Start()
    {
        handler.Start(AuthUser);
    }

    [HttpPost("/stop")]
    public async Task Stop()
    {
        handler.Stop(AuthUser);
    }

    [HttpGet("/state")]
    public async Task<IActionResult> State()
    {
        return Ok(await handler.StateAsync(AuthUser));
    }

    [HttpGet("/completed-status")]
    public async Task<IActionResult> Completed()
    {
        return Ok(handler.Completed(AuthUser));
    }
}
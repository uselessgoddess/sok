using VRisc.UseCases.Commands;

namespace VRisc.Api.Controllers;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VRisc.Core.Exceptions;

[ApiController]
[Authorize]
[Route("api/tasks")]
public class TasksController(IMediator mediator) : ControllerBase
{
    private string AuthUser => HttpContext.User.Identity!.Name!;

    [HttpPost("/start")]
    public async Task Start()
    {
        await mediator.Send(new StartTask
        {
            User = AuthUser,
        });
    }

    [HttpPost("/stop")]
    public async Task Stop()
    {
        await mediator.Send(new StopTask
        {
            User = AuthUser,
        });
    }

    [HttpGet("/state")]
    public async Task<IActionResult> State()
    {
        return Ok(await mediator.Send(new TaskState { User = AuthUser }));
    }

    [HttpGet("/completed-status")]
    public async Task<IActionResult> Completed()
    {
        return Ok(await mediator.Send(new TaskCompleted { User = AuthUser }));
    }
}
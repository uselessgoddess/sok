using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VRisc.UseCases.Commands;
using VRisc.Api.DTOs;
using VRisc.Core.Entities;

namespace VRisc.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/tasks")]
public class TasksController(IMediator mediator, IMapper mapper) : ControllerBase
{
    private string AuthUser => HttpContext.User.Identity!.Name!;

    [HttpPost("start")]
    public async Task Start([FromBody] CpuStateDto dto)
    {
        await mediator.Send(new StartTask
        {
            User = AuthUser,
            State = mapper.Map<CpuState>(dto),
        });
    }

    [HttpPost("stop")]
    public async Task Stop()
    {
        await mediator.Send(new StopTask
        {
            User = AuthUser,
        });
    }

    [HttpGet("state")]
    public async Task<IActionResult> State()
    {
        return Ok(await mediator.Send(new TaskState { User = AuthUser }));
    }

    [HttpGet("completed-status")]
    public async Task<IActionResult> Completed()
    {
        return Ok(await mediator.Send(new TaskCompleted { User = AuthUser }));
    }
}
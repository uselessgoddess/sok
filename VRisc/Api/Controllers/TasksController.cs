using VRisc.UseCases.Emulation;

namespace VRisc.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VRisc.Core.Channels;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.Core.Interfaces;

[ApiController]
[Authorize]
[Route("api/tasks")]
public class TasksController(IEmulationTaskManager tasks, IEmulationStatesService states) : ControllerBase
{
    private string AuthUser => HttpContext.User.Identity!.Name!;

    [HttpPost("/start")]
    public async Task Start()
    {
        var state = states.GetState(AuthUser) ?? throw new NotFoundException();

        tasks.RunTask(
            AuthUser, new Emulator(state.Cpu), Single<CpuState>.CreateChannel(), TimeSpan.FromMilliseconds(100)
        );
    }

    [HttpPost("/stop")]
    public async Task Stop()
    {
        tasks.StopTask(AuthUser);
    }

    [HttpGet("/state")]
    public async Task<IActionResult> State()
    {
        var status = tasks.GetTask(AuthUser) ?? throw new NotFoundException();

        if (status.Error.Reader.TryRead(out var exception))
        {
            throw exception;
        }

        var rxState = status.Sync.Reader.PeekAsync().AsTask();

        if (await Task.WhenAny(rxState, Task.Delay(1000)) == rxState)
        {
            return Ok(rxState.Result);
        }

        throw new TimeoutException();
    }

    [HttpGet("/completed-status")]
    public async Task<IActionResult> Completed()
    {
        var task = tasks.GetTask(AuthUser)?.Task ?? throw new NotFoundException();

        return Ok(task.IsCompleted);
    }
}
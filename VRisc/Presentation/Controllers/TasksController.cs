namespace VRisc.Presentation.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VRisc.Core.Channels;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.Core.Interfaces;
using TaskStatus = VRisc.Core.Entities.TaskStatus;

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

        tasks.RunTask(AuthUser, state.Cpu, Single<CpuState>.CreateChannel(), TimeSpan.FromMilliseconds(100));
    }

    [HttpPost("/stop")]
    public async Task Stop()
    {
        tasks.StopTask(AuthUser);
    }
    
    [HttpGet("/state")]
    public async Task<IActionResult> State()
    {
        var sync = tasks.GetTask(AuthUser)?.Sync ?? throw new NotFoundException();

        var state = await sync.Reader.ReadAsync();

        return Ok(state);
    }

    [HttpPut("/status")]
    public async Task<IActionResult> Status()
    {
        var task = tasks.GetTask(AuthUser)?.Task ?? throw new NotFoundException();

        return task.IsCompleted ? Ok(task.Result.Fatal ? TaskStatus.Fatal : TaskStatus.Ready) : Ok(TaskStatus.Pending);
    }
}
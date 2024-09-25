namespace VRisc.UseCases.Handlers;

using VRisc.Core.Channels;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.Infrastructure.Interfaces;
using VRisc.UseCases.Emulation;

public class TasksHandler(
    IEmulationTaskManager tasks,
    IEmulationStatesService states)
{
    public void Start(string user)
    {
        var state = states.GetState(user) ?? throw new NotFoundException();

        tasks.RunTask(
            user, new Emulator(state.Cpu), Single<CpuState>.CreateChannel(), TimeSpan.FromMilliseconds(100));
    }

    public void Stop(string user)
    {
        tasks.StopTask(user);
    }

    public async Task<CpuState> StateAsync(string user)
    {
        var status = tasks.GetTask(user) ?? throw new NotFoundException();

        if (status.Error.Reader.TryRead(out var exception))
        {
            throw exception;
        }

        var rxState = status.Sync.Reader.PeekAsync().AsTask();

        if (await Task.WhenAny(rxState, Task.Delay(1000)) == rxState)
        {
            return rxState.Result;
        }

        throw new TimeoutException();
    }

    public bool Completed(string user)
    {
        var task = tasks.GetTask(user)?.Task ?? throw new NotFoundException();

        return task.IsCompleted;
    }
}
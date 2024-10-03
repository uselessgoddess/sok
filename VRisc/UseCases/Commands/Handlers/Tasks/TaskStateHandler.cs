namespace VRisc.UseCases.Commands.Handlers;

using MediatR;
using VRisc.Core.Channels;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.Infrastructure.Interfaces;

public class TaskStateHandler(IEmulationTaskManager tasks) : IRequestHandler<TaskState, CpuState>
{
    public async Task<CpuState> Handle(TaskState req, CancellationToken token)
    {
        var status = tasks.GetTask(req.User) ?? throw new NotFoundException();

        if (status.Error.Reader.TryRead(out var exception))
        {
            throw exception;
        }

        var rxState = status.Sync.Reader.PeekAsync(token).AsTask();

        if (await Task.WhenAny(rxState, Task.Delay(1000, token)) == rxState)
        {
            return rxState.Result;
        }

        throw new TimeoutException();
    }
}
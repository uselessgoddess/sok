namespace VRisc.UseCases.Commands.Handlers;

using MediatR;
using VRisc.Core.Channels;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.UseCases.Interfaces;
using VRisc.UseCases.Emulation;

public class TaskCompletedHandler(IEmulationTaskManager tasks) : IRequestHandler<TaskCompleted, bool>
{
    public async Task<bool> Handle(TaskCompleted req, CancellationToken token)
    {
        var task = tasks.GetTask(req.User)?.Task ?? throw new NotFoundException();

        return task.IsCompleted;
    }
}
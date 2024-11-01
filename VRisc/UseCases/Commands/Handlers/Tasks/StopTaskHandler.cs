namespace VRisc.UseCases.Commands.Handlers;

using MediatR;
using VRisc.Core.Channels;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.UseCases.Interfaces;
using VRisc.UseCases.Emulation;

public class StopTaskHandler(IEmulationTaskManager tasks) : IRequestHandler<StopTask>
{
    public async Task Handle(StopTask req, CancellationToken token)
    {
        tasks.StopTask(req.User);
    }
}
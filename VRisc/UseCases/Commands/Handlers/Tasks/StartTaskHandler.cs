using MediatR;
using VRisc.Core.Channels;
using VRisc.Core.Entities;
using VRisc.UseCases.Interfaces;
using VRisc.UseCases.Emulation;

namespace VRisc.UseCases.Commands.Handlers;

public class StartTaskHandler(IEmulationTaskManager tasks)
    : IRequestHandler<StartTask>
{
    public async Task Handle(StartTask req, CancellationToken token)
    {
        tasks.RunTask(
            req.User, new Emulator(req.State), Single<CpuState>.CreateChannel(), TimeSpan.FromMilliseconds(100));
    }
}
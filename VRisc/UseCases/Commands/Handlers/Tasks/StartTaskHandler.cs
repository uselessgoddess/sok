namespace VRisc.UseCases.Commands.Handlers;

using MediatR;
using VRisc.Core.Channels;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.UseCases.Interfaces;
using VRisc.UseCases.Emulation;

public class StartTaskHandler(IEmulationTaskManager tasks, IEmulationStatesService states)
    : IRequestHandler<StartTask>
{
    public async Task Handle(StartTask req, CancellationToken token)
    {
        var state = states.GetState(req.User) ?? throw new NotFoundException();

        tasks.RunTask(
            req.User, new Emulator(state.Cpu), Single<CpuState>.CreateChannel(), TimeSpan.FromMilliseconds(100));
    }
}
using MediatR;
using VRisc.Core.Entities;
using VRisc.Core.Interfaces;
using VRisc.UseCases.Interfaces;

namespace VRisc.UseCases.Commands.Handlers;

public class LoadStateHandler(IEmulationStateRepository repo)
    : IRequestHandler<LoadState, EmulationState?>
{
    public async Task<EmulationState?> Handle(LoadState req, CancellationToken token)
    {
        return await repo.LoadState(req.Id);
    }
}
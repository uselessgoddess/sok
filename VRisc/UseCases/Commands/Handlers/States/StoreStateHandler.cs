using MediatR;
using VRisc.Core.Interfaces;
using VRisc.Core.Exceptions;

namespace VRisc.UseCases.Commands.Handlers;

public class StoreStateHandler(IEmulationStateRepository repo)
    : IRequestHandler<StoreState>
{
    public async Task Handle(StoreState req, CancellationToken token)
    {
        await repo.StoreState(req.State);
    }
}
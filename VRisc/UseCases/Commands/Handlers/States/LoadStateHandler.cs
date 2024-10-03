namespace VRisc.UseCases.Commands.Handlers;

using MediatR;
using VRisc.Core.Interfaces;
using VRisc.Infrastructure.Interfaces;

public class LoadStateHandler(IEmulationStatesService states, IEmulationStateRepository repo)
    : IRequestHandler<LoadState>
{
    public async Task Handle(LoadState req, CancellationToken token)
    {
        var state = await repo.LoadState(req.Id);

        states.SetState(req.User, state);
    }
}
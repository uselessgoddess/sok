namespace VRisc.UseCases.Commands.Handlers;

using MediatR;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.Core.Interfaces;
using VRisc.Infrastructure.Interfaces;

public class SaveStateHandler(IEmulationStatesService states, IEmulationStateRepository repo)
    : IRequestHandler<SaveState, EmulationState>
{
    public async Task<EmulationState> Handle(SaveState req, CancellationToken token)
    {
        var state = states.GetState(req.User) ?? throw new NotFoundException();
        state.Modified = DateTime.Now;

        await repo.StoreState(state);

        return state;
    }
}
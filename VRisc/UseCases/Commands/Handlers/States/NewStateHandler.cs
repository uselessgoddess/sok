namespace VRisc.UseCases.Commands.Handlers;

using MediatR;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.UseCases.Interfaces;

public class NewStateHandler(IEmulationStatesService states) : IRequestHandler<NewState, EmulationState>
{
    public async Task<EmulationState> Handle(NewState req, CancellationToken token)
    {
        var state = new EmulationState(req.User);
        states.SetState(req.User, state);

        return state;
    }
}
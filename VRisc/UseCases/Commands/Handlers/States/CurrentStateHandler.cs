namespace VRisc.UseCases.Commands.Handlers;

using MediatR;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.UseCases.Interfaces;

public class CurrentStateHandler(IEmulationStatesService states) : IRequestHandler<CurrentState, EmulationState>
{
    public async Task<EmulationState> Handle(CurrentState req, CancellationToken token)
    {
        return states.GetState(req.User) ?? throw new NotFoundException();
    }
}
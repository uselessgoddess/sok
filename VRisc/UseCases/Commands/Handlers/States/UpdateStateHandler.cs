namespace VRisc.UseCases.Commands.Handlers;

using MediatR;
using VRisc.Infrastructure.Interfaces;

public class UpdateStateHandler(IEmulationStatesService states)
    : IRequestHandler<UpdateState>
{
    public async Task Handle(UpdateState req, CancellationToken token)
    {
        states.UpdateState(req.User, state =>
        {
            state = req.Update(state);
            state.Modified = DateTime.Now;
            return state;
        });
    }
}
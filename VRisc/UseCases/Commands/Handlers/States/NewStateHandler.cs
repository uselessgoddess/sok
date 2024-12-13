using MediatR;
using VRisc.Core.Interfaces;
using VRisc.Core.Entities;
using VRisc.UseCases.Interfaces;

namespace VRisc.UseCases.Commands.Handlers;

public class NewStateHandler(IEmulationStateRepository repo)
    : IRequestHandler<NewState, EmulationState>
{
    public async Task<EmulationState> Handle(NewState req, CancellationToken token)
    {
        var state = new EmulationState(req.User, Faker.Company.Name());

        await repo.StoreState(state);

        return state;
    }
}
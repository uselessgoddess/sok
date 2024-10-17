namespace VRisc.UseCases.Queries.Handlers;

using MediatR;
using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

public class StateSessionsHandler(IEmulationStateRepository repo)
    : IRequestHandler<StateSessions, IEnumerable<EmulationState>>
{
    public async Task<IEnumerable<EmulationState>> Handle(StateSessions req, CancellationToken token)
    {
        var (user, page, size) = (req.User, req.Page, req.Size);

        var list = await repo.LoadStates(user);

        return list.Skip((int)(page * size)).Take((int)size);
    }
}
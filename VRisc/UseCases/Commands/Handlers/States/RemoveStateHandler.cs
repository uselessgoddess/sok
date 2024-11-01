using MediatR;
using VRisc.Core.Exceptions;
using VRisc.Core.Interfaces;

namespace VRisc.UseCases.Commands.Handlers;

public class RemoveStateHandler(IEmulationStateRepository repo)
    : IRequestHandler<RemoveState>
{
    public async Task Handle(RemoveState req, CancellationToken token)
    {
        var state = await repo.LoadState(req.Id);

        if (state.User == req.User)
        {
            await repo.ForgetState(req.Id);
        }
        else
        {
            throw new ForbiddenException();
        }
    }
}
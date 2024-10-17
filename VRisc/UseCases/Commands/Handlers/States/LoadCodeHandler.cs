using MediatR;
using VRisc.UseCases.Interfaces;
using VRisc.UseCases.Interfaces;

namespace VRisc.UseCases.Commands.Handlers;

public class LoadCodeHandler(IEmulationStatesService states, ICodeCompiler compiler)
    : IRequestHandler<LoadCode>
{
    public async Task Handle(LoadCode req, CancellationToken token)
    {
        var (user, jwt, code) = (req.User, req.Jwt, req.Code);

        var bytes = await compiler.CompileAsync(jwt, code);

        states.UpdateState(user, state =>
        {
            state.Cpu.Bus.Dram = bytes;
            return state;
        });
    }
}
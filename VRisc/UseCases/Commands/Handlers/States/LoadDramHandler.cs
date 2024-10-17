namespace VRisc.UseCases.Commands.Handlers;

using MediatR;
using VRisc.UseCases.Interfaces;

public class LoadDramHandler(IEmulationStatesService states, ICompileCheckProducer check)
    : IRequestHandler<LoadDram>
{
    public async Task Handle(LoadDram req, CancellationToken token)
    {
        var (user, dram) = (req.User, req.Dram);

        check.SendPotentialAsm(user, dram);

        states.UpdateState(user, state =>
        {
            state.Cpu.Bus.Dram = dram;
            return state;
        });
    }
}
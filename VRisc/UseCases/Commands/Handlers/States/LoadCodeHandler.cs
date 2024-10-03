namespace VRisc.UseCases.Commands.Handlers;

using MediatR;
using VRisc.Core.Exceptions;
using VRisc.Infrastructure.Interfaces;
using VRisc.Infrastructure.Services;

public class LoadCodeHandler(IEmulationStatesService states, GrpcCompilerService compiler)
    : IRequestHandler<LoadCode>
{
    public async Task Handle(LoadCode req, CancellationToken token)
    {
        var (user, jwt, code) = (req.User, req.Jwt, req.Code);

        var compiled = await compiler.CompileAsync(jwt, new GrpcServices.CompileRequest
        {
            OptLevel = "0",
            Source = code,
        });

        if (!compiled.Success)
        {
            throw new BadRequestException(compiled.Message);
        }

        states.UpdateState(user, state =>
        {
            state.Cpu.Bus.Dram = compiled.Binary.ToByteArray();
            return state;
        });
    }
}
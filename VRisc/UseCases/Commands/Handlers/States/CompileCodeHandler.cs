using MediatR;
using VRisc.UseCases.Interfaces;

namespace VRisc.UseCases.Commands.Handlers;

public class CompileCodeHandler(ICodeCompiler compiler)
    : IRequestHandler<CompileCode, byte[]>
{
    public async Task<byte[]> Handle(CompileCode req, CancellationToken token)
    {
        var (jwt, code) = (req.Jwt, req.Code);

        return await compiler.CompileAsync(jwt, code);
    }
}
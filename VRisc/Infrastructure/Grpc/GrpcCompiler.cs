using VRisc.Core.Exceptions;
using VRisc.Infrastructure.Services;
using VRisc.UseCases.Interfaces;

namespace VRisc.Infrastructure.Grpc;

public class GrpcCompiler(GrpcCompilerService compiler) : ICodeCompiler
{
    public async Task<byte[]> CompileAsync(string jwt, string source)
    {
        var compiled = await compiler.CompileAsync(jwt, new GrpcServices.CompileRequest
        {
            OptLevel = "0",
            Source = source,
        });

        if (!compiled.Success)
        {
            throw new BadRequestException(compiled.Message);
        }

        return compiled.Binary.ToByteArray();
    }
}
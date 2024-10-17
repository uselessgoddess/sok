using Grpc.Core;

namespace VRisc.Infrastructure.Services;

using GrpcServices;
using VRisc.Infrastructure.Grpc;

public class GrpcCompilerService(Compiler.CompilerClient grpc)
{
    public async Task<CompileResponse> CompileAsync(string jwt, CompileRequest request)
    {

        return await grpc.CompileCodeAsync(request, new Metadata
        {
            { "Authorization", $"Bearer {jwt}" },
        });
    }
}
using Compiler.Core.Compilers;
using Grpc.Core;
using Compiler.GrpcServices;

namespace Compiler.Core.Services;

public class CompilerService(CacheCompiler compiler) : Compiler.GrpcServices.Compiler.CompilerBase
{
    public override async Task<CompileResponse> CompileCode(CompileRequest request, ServerCallContext _)
    {
        return await compiler.Compile(request.Source, request.OptLevel);
    }
}
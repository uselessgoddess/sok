using Compiler.Core.Compilers;

namespace Compiler.Core.Services;

using Grpc.Core;
using GrpcServices;

public class CompilerService(CacheCompiler compiler) : Compiler.CompilerBase
{
    public override async Task<CompileResponse> CompileCode(CompileRequest request, ServerCallContext _)
    {
        return await compiler.Compile(request.Source, request.OptLevel);
    }
}
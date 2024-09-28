using Compiler.Core.Interfaces;

namespace Compiler.Core.Services;

using Grpc.Core;
using GrpcServices;

public class CompilerService(ICompiler compiler) : Compiler.CompilerBase
{
    public override async Task<CompileResponse> CompileCode(CompileRequest request, ServerCallContext _)
    {
        return await compiler.Compile(request.Source, request.OptLevel);
    }
}
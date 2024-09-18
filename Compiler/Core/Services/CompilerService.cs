using Core.Interfaces;
using Grpc.Core;
using GrpcServices;

namespace Core.Services;

public class CompilerService(ICompiler compiler) : Compiler.CompilerBase
{
    public override async Task<CompileResponse> CompileCode(CompileRequest request, ServerCallContext _)
    {
        return await compiler.Compile(request.Source);
    }
}
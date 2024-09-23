namespace Compiler.Core.Services;

using Compiler.Core.Interfaces;
using Grpc.Core;
using GrpcServices;

public class CompilerService(ICompiler compiler) : GrpcServices.Compiler.CompilerBase
{
    public override async Task<CompileResponse> CompileCode(CompileRequest request, ServerCallContext _)
    {
        return await compiler.Compile(request.Source, request.OptLevel);
    }
}
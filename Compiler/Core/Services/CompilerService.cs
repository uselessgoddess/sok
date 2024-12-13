using Grpc.Core;
using Compiler.GrpcServices;
using Compiler.Core.Interfaces;

namespace Compiler.Core.Services;


public class CompilerService(ICompiler compiler) : Compiler.GrpcServices.Compiler.CompilerBase
{
    public override async Task<CompileResponse> CompileCode(CompileRequest request, ServerCallContext _)
    {
        return await compiler.Compile(request.Source, request.OptLevel);
    }
}
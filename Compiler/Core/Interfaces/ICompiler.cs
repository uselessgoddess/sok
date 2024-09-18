using GrpcServices;

namespace Core.Interfaces;

public interface ICompiler
{
    Task<CompileResponse> Compile(string src, CancellationToken token = default);
}
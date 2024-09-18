using Core.Interfaces;
using Google.Protobuf;
using GrpcServices;

namespace Core.Compilers;

public class ProcessCompiler : ICompiler
{
    public async Task<CompileResponse> Compile(string source, CancellationToken token = default)
    {
        var bin = Guid.NewGuid().ToString();
        var src = bin + ".src";

        await File.WriteAllTextAsync(src, source, token);

        var result = await new Data.Compilers.ProcessCompiler().Compile(src, token);

        var binary = result.Success
            ? await ByteString.FromStreamAsync(File.OpenRead(bin), token)
            : ByteString.Empty;

        File.Delete(src);
        File.Delete(bin);
        
        return new CompileResponse
        {
            Success = result.Success,
            Binary = binary,
            Message = result.Stderr,
        };
    }
}
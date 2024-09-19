namespace Compiler.Core.Compilers;

using Compiler.Core.Interfaces;
using Google.Protobuf;
using GrpcServices;

public class ProcessCompiler : ICompiler
{
    public async Task<CompileResponse> Compile(string source, string opt, CancellationToken token = default)
    {
        var elf = Guid.NewGuid().ToString();
        var src = elf + ".src";
        var bin = elf + ".bin";

        await File.WriteAllTextAsync(src, source, token);

        var result = await new Data.Compilers.ProcessCompiler().Compile(src, elf, opt, token);

        var binary = result.Success
            ? await ByteString.FromStreamAsync(File.OpenRead(bin), token)
            : ByteString.Empty;

        File.Delete(src);
        File.Delete(elf);
        File.Delete(bin);

        return new CompileResponse
        {
            Success = result.Success,
            Binary = binary,
            Message = result.Stderr,
        };
    }
}
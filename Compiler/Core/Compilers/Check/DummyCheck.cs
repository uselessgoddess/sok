using Compiler.Core.Interfaces;

namespace Compiler.Core.Compilers.Check;

public class DummyCheck : ICompileCheck
{
    public async Task<(bool ok, string message)> Check(byte[] src)
    {
        return (true, string.Empty);
    }
}
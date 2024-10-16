using Compiler.Core.Compilers.Check;
using Compiler.Core.Interfaces;

namespace Compiler.Tests;

public class CheckTests
{
    [Theory]
    [MemberData(nameof(GetEmulatorImplementations))]
    public async Task CompileInvariants(ICompileCheck check)
    {
        var codes = new List<byte[]>
        {
            new byte[] { }, // empty 
            new byte[] { 0x93, 0x01, 0x50, 0x00, }, // addi x3, x0, 5
            new byte[] { 0x13, 0x02, 0x60, 0x00, }, // addi x4, x0, 6
            new byte[] { 0x33, 0x81, 0x41, 0x00, }, // add x2, x3, x4
            new byte[] { 0x33, 0x81, 0x41, 0x00, 0x33, 0x81, 0x41, 0x00, },
        };

        foreach (var code in codes)
        {
            var (ok, _) = await check.Check(code);
            Assert.True(ok, $"check failed for: {check}");
        }
    }

    public static IEnumerable<object[]> GetEmulatorImplementations()
    {
        yield return [new DummyCheck()];
    }
}
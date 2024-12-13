namespace Compiler.Core.Interfaces;

public interface ICompileCheck
{
    Task<(bool ok, string message)> Check(byte[] src);
}
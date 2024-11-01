namespace VRisc.UseCases.Interfaces;

public interface ICodeCompiler
{
    Task<byte[]> CompileAsync(string jwt, string source);
}
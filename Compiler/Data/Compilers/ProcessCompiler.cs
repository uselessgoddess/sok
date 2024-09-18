namespace Data.Compilers;

using System.Diagnostics;

public class CompilationResult
{
    public bool Success { get; set; }

    public required string Stderr { get; set; }
}

public class ProcessCompiler(string compiler = "driver")
{
    public async Task<CompilationResult> Compile(string path, CancellationToken token = default)
    {
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = compiler,
            Arguments = $"{path} --target riscv32i-unknown-none-elf",
            RedirectStandardError = true,
        });

        _ = process.Start();
        
        await process.WaitForExitAsync(token);

        return new CompilationResult
        {
            Success = process.ExitCode == 0,
            Stderr = await process.StandardError.ReadToEndAsync(token),
        };
    }
}
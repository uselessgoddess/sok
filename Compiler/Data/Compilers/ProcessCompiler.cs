namespace Compiler.Data.Compilers;

using System.Diagnostics;

public class CompilationResult
{
    public bool Success { get; set; }

    public required string Stderr { get; set; }
}

public class ProcessCompiler(string compiler = "driver")
{
    public static async Task<Process> RunProcess(string name, string args, CancellationToken token = default)
    {
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = name,
            Arguments = args,
            RedirectStandardError = true,
        });

        _ = process.Start();

        await process.WaitForExitAsync(token);

        return process;
    }

    public async Task<CompilationResult> Compile(string srcPath, string elfPath, string opt,
        CancellationToken token = default)
    {
        var elf = await RunProcess(compiler,
            $"{srcPath} --target riscv32i-unknown-none-elf -Copt-level={opt} --no-main", token);

        _ = await RunProcess("llvm-objcopy", 
            $"-S -O binary --only-section=.text {elfPath} {elfPath}.bin", token);

        return new CompilationResult
        {
            Success = elf.ExitCode == 0,
            Stderr = await elf.StandardError.ReadToEndAsync(token),
        };
    }
}
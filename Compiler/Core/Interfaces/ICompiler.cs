﻿namespace Compiler.Core.Interfaces;

using GrpcServices;

public interface ICompiler
{
    Task<CompileResponse> Compile(string src, string opt, CancellationToken token = default);
}
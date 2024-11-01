namespace VRisc.UseCases.Commands;

using MediatR;

public class CompileCode : IRequest<byte[]>
{
    public required string Jwt { get; set; }

    public required string Code { get; set; }
}
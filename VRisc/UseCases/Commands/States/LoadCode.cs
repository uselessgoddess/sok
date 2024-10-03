namespace VRisc.UseCases.Commands;

using MediatR;

public class LoadCode : Base, IRequest
{
    public required string Jwt { get; set; }

    public required string Code { get; set; }
}
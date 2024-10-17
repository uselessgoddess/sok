namespace VRisc.UseCases.Commands;

using MediatR;

public class LoadState : Base, IRequest
{
    public required string Id { get; set; }
}
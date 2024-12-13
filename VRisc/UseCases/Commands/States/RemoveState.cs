namespace VRisc.UseCases.Commands;

using MediatR;

public class RemoveState : Base, IRequest
{
    public required string Id { get; set; }
}
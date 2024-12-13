using VRisc.Core.Entities;
using MediatR;

namespace VRisc.UseCases.Commands;

public class LoadState : Base, IRequest<EmulationState?>
{
    public required string Id { get; set; }
}
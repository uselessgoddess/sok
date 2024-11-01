using MediatR;
using VRisc.Core.Entities;

namespace VRisc.UseCases.Commands;

public class StoreState : IRequest
{
    public required EmulationState State { get; set; }
}
namespace VRisc.UseCases.Commands;

using MediatR;
using VRisc.Core.Entities;

public class UpdateState : Base, IRequest
{
    public Func<EmulationState, EmulationState> Update { get; set; }
}
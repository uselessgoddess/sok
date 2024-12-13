namespace VRisc.UseCases.Queries;

using MediatR;
using VRisc.Core.Entities;
using VRisc.UseCases.Commands;

public class StateSessions : Base, IRequest<IEnumerable<EmulationState>>
{
    public required uint Page { get; set; }

    public required uint Size { get; set; }
}
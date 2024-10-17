namespace VRisc.UseCases.Commands;

using MediatR;
using VRisc.Core.Entities;

public class CurrentState : Base, IRequest<EmulationState>;
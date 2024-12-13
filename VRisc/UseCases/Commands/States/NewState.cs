namespace VRisc.UseCases.Commands;

using MediatR;
using VRisc.Core.Entities;

public class NewState : Base, IRequest<EmulationState>;
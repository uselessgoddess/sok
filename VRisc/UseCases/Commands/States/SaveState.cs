namespace VRisc.UseCases.Commands;

using MediatR;
using VRisc.Core.Entities;

public class SaveState : Base, IRequest<EmulationState>;
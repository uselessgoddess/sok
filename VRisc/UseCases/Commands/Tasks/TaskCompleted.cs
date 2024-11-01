namespace VRisc.UseCases.Commands;

using MediatR;
using VRisc.Core.Entities;

public class TaskCompleted : Base, IRequest<bool>;
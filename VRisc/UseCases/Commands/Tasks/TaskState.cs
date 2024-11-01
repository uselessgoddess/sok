namespace VRisc.UseCases.Commands;

using MediatR;
using VRisc.Core.Entities;

public class TaskState : Base, IRequest<CpuState>;
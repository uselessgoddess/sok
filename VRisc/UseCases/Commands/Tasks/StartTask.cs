using VRisc.Core.Entities;
using MediatR;

namespace VRisc.UseCases.Commands;

public class StartTask : Base, IRequest
{
    public required CpuState State { get; set; }
}
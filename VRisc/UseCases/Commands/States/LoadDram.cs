namespace VRisc.UseCases.Commands;

using MediatR;

public class LoadDram : Base, IRequest
{
    public required byte[] Dram { get; set; }
}
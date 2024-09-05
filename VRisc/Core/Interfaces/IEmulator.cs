namespace VRisc.Core.Interfaces;

using System.Threading.Channels;
using VRisc.Core.Entities;
using TaskStatus = VRisc.Core.Entities.TaskStatus;

public interface IEmulator
{
    Trap NextCycle(ref CpuState state);

    async Task<EmulationResult> Run(
        CpuState state, Channel<CpuState> channel, TimeSpan span, CancellationToken token)
    {
        var timer = DateTime.Now;

        while (true)
        {
            if (NextCycle(ref state) == Trap.Fatal)
            {
                return new EmulationResult { Cpu = state, Fatal = true };
            }

            if (DateTime.Now.Subtract(timer) >= span)
            {
                await channel.Writer.WriteAsync(state, token);
            }
        }

        // TODO: Unreachable atm because there is no vision how to catch end of emulation
        return new EmulationResult { Cpu = state, Fatal = false };
    }
}
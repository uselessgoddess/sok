namespace VRisc.Core.Interfaces;

using System.Threading.Channels;
using VRisc.Core.Entities;

public interface IEmulator
{
    Trap NextCycle();

    CpuState GetState();

    async Task<CpuState> Run(Channel<CpuState> channel, TimeSpan span, CancellationToken token)
    {
        var timer = DateTime.Now;

        while (true)
        {
            if (NextCycle() == Trap.Fatal)
            {
                return GetState();
            }

            if (DateTime.Now.Subtract(timer) >= span)
            {
                await channel.Writer.WriteAsync(GetState(), token);
            }
        }
    }
}
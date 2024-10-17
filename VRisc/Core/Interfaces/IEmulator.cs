namespace VRisc.Core.Interfaces;

using System.Threading.Channels;
using VRisc.Core.Entities;

public interface IEmulator
{
    Trap NextCycle();

    CpuState GetState();

    async Task Run(
        Channels.Single<CpuState> state, Channel<Exception> error, TimeSpan span, CancellationToken token)
    {
        var timer = DateTime.UnixEpoch;

        while (true)
        {
            try
            {
                var trap = NextCycle();

                if (trap == Trap.Fatal || DateTime.Now.Subtract(timer) >= span)
                {
                    timer = DateTime.Now;

                    await state.Writer.WriteAsync(GetState(), token);
                }

                if (trap == Trap.Fatal)
                {
                    break;
                }
            }
            catch (Exception ex)
            {
                await error.Writer.WriteAsync(ex, token);
            }
        }
    }
}
using System.Threading.Channels;

namespace VRisc.Presentation.Hubs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize]
public class EmulationHub() : Hub
{
    public async Task StartStreamingCpuState()
    {
        var channel = Channel.CreateUnbounded<int>();
        {
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    await channel.Writer.WriteAsync(123);

                    await Task.Delay(1000); // Задержка в 1 секунду для обновления
                }
            });
        }

        var user = Context.User.Identity!.Name!;
        while (await channel.Reader.WaitToReadAsync())
        {
            while (channel.Reader.TryRead(out var reg))
            {
                await Clients.Caller.SendAsync("ReceiveRegistersUpdate", reg);
            }
        }
    }
}
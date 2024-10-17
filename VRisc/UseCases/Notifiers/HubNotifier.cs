using VRisc.Core.Interfaces;
using VRisc.UseCases.Interfaces;

namespace VRisc.UseCases.Notifiers;

using Microsoft.AspNetCore.SignalR;
using VRisc.UseCases.Hubs;

public class HubNotifier(IHubContext<EmulationHub> hub) : ICheckNotifier
{
    public async Task Notify(string user, bool ok, string message = "")
    {
        await hub.Clients.Client(user).SendAsync("NotifyCompileCheck", ok, message);
    }
}
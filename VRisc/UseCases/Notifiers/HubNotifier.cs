namespace VRisc.UseCases.Notifiers;

using Microsoft.AspNetCore.SignalR;
using VRisc.UseCases.Hubs;
using VRisc.UseCases.Interfaces;

public class HubNotifier(IHubContext<EmulationHub> hub) : ICheckNotifier
{
    public async Task Notify(string user, bool ok, string message = "")
    {
        await hub.Clients.Client(user).SendAsync("NotifyCompileCheck", ok, message);
    }
}
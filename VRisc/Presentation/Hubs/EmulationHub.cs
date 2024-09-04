namespace VRisc.Presentation.Hubs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

public class EmulationHub(EmulationHub emulator) : Hub
{
    [Authorize]
    public async Task StartEmulation(string sourceCode)
    {
        var user = Context.User.Identity!.Name;

        await Clients.All.SendAsync("ReceiveMessage", $"{user} started an emulation.");
    }

    public async Task StopEmulation(string id)
    {
        await Clients.All.SendAsync("ReceiveMessage", $"Emulation {id} stopped.");
    }
}
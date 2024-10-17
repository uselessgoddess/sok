namespace VRisc.UseCases.Hubs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize]
public class EmulationHub : Hub
{
}
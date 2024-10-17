using VRisc.Core.Interfaces;
using VRisc.UseCases.Interfaces;

namespace VRisc.UseCases.Notifiers;

public class DummyNotifier : ICheckNotifier
{
    public Task Notify(string user, bool ok, string message = "")
    {
        return Task.CompletedTask;
    }
}
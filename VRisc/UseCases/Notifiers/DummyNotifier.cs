namespace VRisc.UseCases.Notifiers;

using VRisc.UseCases.Interfaces;

public class DummyNotifier : ICheckNotifier
{
    public Task Notify(string user, bool ok, string message = "")
    {
        return Task.CompletedTask;
    }
}
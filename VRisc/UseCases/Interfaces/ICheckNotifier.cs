namespace VRisc.UseCases.Interfaces;

public interface ICheckNotifier
{
    Task Notify(string user, bool ok, string message = "");
}
namespace VRisc.UseCases.Interfaces;

public interface ICheckNotifier
{
    void Notify(bool success, string message = "");
}
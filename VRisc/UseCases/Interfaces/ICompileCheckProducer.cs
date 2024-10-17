namespace VRisc.UseCases.Interfaces;

public interface ICompileCheckProducer
{
    void SendPotentialAsm(string user, byte[] bytes);
}
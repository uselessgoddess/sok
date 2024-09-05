namespace VRisc.Core.Interfaces;

using VRisc.Core.Entities;

public interface IEmulationStateRepository
{
    Task<EmulationState> LoadState(string emulation);

    Task<IEnumerable<EmulationState>> LoadStates(string user);

    Task StoreState(EmulationState state);

    Task ForgetState(string emulation);
}
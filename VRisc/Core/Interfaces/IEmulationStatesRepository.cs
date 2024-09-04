using VRisc.Core.Entities;

namespace VRisc.Core.Interfaces;

public interface IEmulationStateRepository
{
    Task<EmulationState> LoadStateAsync(string emulation);

    Task<IEnumerable<EmulationState>> LoadStatesAsync(string user);

    Task StoreStateAsync(EmulationState state);

    Task ForgetStateAsync(string emulation);
}
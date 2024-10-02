namespace VRisc.Infrastructure.Interfaces;

using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

public interface IEmulationTaskManager
{
    EmulationTask? GetTask(string user);

    public void RunTask(string user, IEmulator emulator, Core.Channels.Single<CpuState> channel, TimeSpan span);

    void StopTask(string user);
}
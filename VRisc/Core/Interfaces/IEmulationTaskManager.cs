namespace VRisc.Core.Interfaces;

using VRisc.Core.Entities;

public interface IEmulationTaskManager
{
    EmulationTask? GetTask(string user);

    public void RunTask(string user, IEmulator emulator, Channels.Single<CpuState> channel, TimeSpan span);

    void StopTask(string user);
}
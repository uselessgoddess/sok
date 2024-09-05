namespace VRisc.Core.Interfaces;

using VRisc.Core.Entities;

public interface IEmulationTaskManager
{
    EmulationTask? GetTask(string user);

    void RunTask(string user, CpuState state, Channels.Single<CpuState> channel, TimeSpan span);

    void StopTask(string user);
}
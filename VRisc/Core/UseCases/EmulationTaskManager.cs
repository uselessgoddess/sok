using VRisc.Core.Exceptions;

namespace VRisc.Core.UseCases;

using System.Collections.Concurrent;
using VRisc.Core.Entities;
using VRisc.Core.Interfaces;

public class EmulationTaskManger : IEmulationTaskManager
{
    private readonly ConcurrentDictionary<string, EmulationTask> tasks = new();

    public EmulationTask? GetTask(string user)
    {
        return tasks.GetValueOrDefault(user);
    }

    public void RunTask(string user, IEmulator emulator, Channels.Single<CpuState> channel, TimeSpan span)
    {
        if (tasks.TryGetValue(user, out var old))
        {
            old.Cancellation.Cancel();
        }

        var cancel = new CancellationTokenSource();
        var task = new EmulationTask
        {
            Task = emulator.Run(channel, span, cancel.Token),
            Sync = channel,
            Cancellation = cancel,
        };
        tasks.AddOrUpdate(user, task, (_, old) =>
        {
            old.Cancellation.Cancel();
            return task;
        });
    }

    public void StopTask(string user)
    {
        var task = GetTask(user) ?? throw new NotFoundException();
        task.Cancellation.Cancel();

        tasks.Remove(user, out _);
    }
}
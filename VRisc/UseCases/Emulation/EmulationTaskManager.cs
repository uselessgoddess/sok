namespace VRisc.UseCases.Emulation;

using System.Collections.Concurrent;
using System.Threading.Channels;
using VRisc.Core.Entities;
using VRisc.Core.Exceptions;
using VRisc.Core.Interfaces;

public class EmulationTaskManger : IEmulationTaskManager
{
    private readonly ConcurrentDictionary<string, EmulationTask> tasks = new();

    public EmulationTask? GetTask(string user)
    {
        return tasks.GetValueOrDefault(user);
    }

    public void RunTask(string user, IEmulator emulator, Core.Channels.Single<CpuState> channel, TimeSpan span)
    {
        var cancel = new CancellationTokenSource();
        var error = Channel.CreateBounded<Exception>(32);
        var background = Task.Run(async () => await emulator.Run(channel, error, span, cancel.Token), cancel.Token);
        var task = new EmulationTask
        {
            Task = background,
            Sync = channel,
            Error = error,
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
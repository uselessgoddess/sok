namespace VRisc.Core.Entities;

using System.Threading.Channels;

public record struct EmulationTask
{
    public Task Task { get; init; }

    public Channel<Exception> Error { get; init; }

    public Channels.Single<CpuState> Sync { get; init; }

    public CancellationTokenSource Cancellation { get; init; }
}
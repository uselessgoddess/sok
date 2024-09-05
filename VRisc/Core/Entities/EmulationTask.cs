namespace VRisc.Core.Entities;

using VRisc.Core;

public record struct EmulationTask
{
    public Task<EmulationResult> Task { get; init; }

    public Channels.Single<CpuState> Sync { get; init; }

    public CancellationTokenSource Cancellation { get; init; }
}
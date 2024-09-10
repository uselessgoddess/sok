namespace VRisc.Core.Channels;

using System.Threading.Channels;

public class Single<T>
{
    private readonly Channel<T> channel;

    private Single(Channel<T> channel)
    {
        this.channel = channel;
    }

    public static Single<T> CreateChannel()
    {
        return new Single<T>(Channel.CreateBounded<T>(new BoundedChannelOptions(capacity: 1)
        {
            FullMode = BoundedChannelFullMode.DropOldest,
        }));
    }

    public ChannelWriter<T> Writer => channel.Writer;

    public ChannelReader<T> Reader => channel.Reader;
}

public static class SingleReaderExtension
{
    public static ValueTask<T> PeekAsync<T>(this ChannelReader<T> reader, CancellationToken token = default)
    {
        return token.IsCancellationRequested
            ? new ValueTask<T>(Task.FromCanceled<T>(token))
            : ReadAsyncCore(token);

        async ValueTask<T> ReadAsyncCore(CancellationToken ct)
        {
            while (true)
            {
                if (!await reader.WaitToReadAsync(ct).ConfigureAwait(false))
                {
                    throw new ChannelClosedException();
                }

                if (reader.TryPeek(out var item))
                {
                    return item;
                }
            }
        }
    }
}
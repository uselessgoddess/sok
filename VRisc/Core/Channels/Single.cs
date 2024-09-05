namespace VRisc.Core.Channels;

using System.Threading.Channels;

public class Single<T> : Channel<T>
{
    public static Single<T> CreateChannel()
    {
        return Channel.CreateBounded<T>(new BoundedChannelOptions(capacity: 1)
        {
            FullMode = BoundedChannelFullMode.DropOldest,
        }) as Single<T>;
    }
}
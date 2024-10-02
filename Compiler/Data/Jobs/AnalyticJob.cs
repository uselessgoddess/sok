using Compiler.Data.Cache;
using Compiler.Data.Services;
using Google.Protobuf;

namespace Compiler.Data.Jobs;

public class AnalyticsJob(AnalyticsService analytics, ICacheService cache)
{
    public void CollectMetrics()
    {
        var metrics = analytics.GetMetrics();

        cache.SetCacheAsync($"analytics:{DateTime.Now}", metrics.ToByteArray());

        analytics.ResetMetrics();
    }
}
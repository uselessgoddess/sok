using System.Text;
using Compiler.Data.Cache;
using Compiler.Data.Jobs;
using Compiler.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Hangfire;
using Hangfire.Redis.StackExchange;

namespace Compiler.Data;

public static class DependencyInjection
{
    private static IServiceCollection AddHangfireJobs(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<AnalyticsJob>();

        RecurringJob.AddOrUpdate<AnalyticsJob>("collect-metrics", analytics => analytics.CollectMetrics(), Cron.Hourly);
        RecurringJob.AddOrUpdate<ICacheService>("clean-cache", cache => cache.ClearCache(), Cron.Daily);
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddSingleton<AnalyticsService>();
        return services;
    }

    private static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration config)
    {
        var redis = config.GetConnectionString("Redis");
        var conn = ConnectionMultiplexer.Connect(redis);
        services
            .AddSingleton<IConnectionMultiplexer>(conn)
            .AddSingleton(conn.GetServer(redis))
            .AddSingleton<ICacheService, RedisCacheService>()
            .AddHangfire(config => { config.UseRedisStorage(conn); })
            .AddHangfireServer();
        JobStorage.Current = new RedisStorage(conn);
        return services;
    }

    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        var jwt = config.GetSection("Jwt");

        builder.Services
            .AddServices(config)
            .AddRedisCache(config)
            .AddHangfireJobs(config)
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.GetValue<string>("Issuer"),
                    ValidAudience = jwt.GetValue<string>("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.GetValue<string>("Key"))),
                };
            });
        return builder;
    }
}
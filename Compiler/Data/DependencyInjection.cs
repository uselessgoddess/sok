using System.Text;
using Compiler.Data.Cache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using GrpcServices;

namespace Compiler.Data;

public static class DependencyInjection
{
    private static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration config)
    {
        var redis = config.GetConnectionString("Redis");
        services
            .AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redis))
            .AddSingleton<ICacheService, RedisCacheService>();
        return services;
    }

    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        var jwt = config.GetSection("Jwt");

        builder.Services
            .AddRedisCache(config)
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
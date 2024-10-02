namespace VRisc.Infrastructure;

using Grpc.Net.Client;
using GrpcServices;
using VRisc.Infrastructure.Grpc;
using VRisc.Infrastructure.Interfaces;
using System.Text;
using GrpcServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using VRisc.Core.Entities;
using VRisc.Core.Interfaces;
using VRisc.Infrastructure.Broker;
using VRisc.Infrastructure.Data;
using VRisc.Infrastructure.Interfaces;
using VRisc.Infrastructure.Repositories;
using VRisc.Infrastructure.Services;
using VRisc.Infrastructure.Grpc;
using VRisc.Infrastructure.Interfaces;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddAuthentication(config)
            .AddResources(config)
            .AddServices(config);
        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var jwt = config.GetSection("Jwt");
        services.AddAuthentication(options =>
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
        return services;
    }

    private static IServiceCollection AddResources(this IServiceCollection services, IConfiguration config)
    {
        var conn = config.GetConnectionString("Mongo")!;
        var database = config.GetSection("Mongo")["Database"]!;
        var mongo = new MongoContext(new MongoClient(conn), database);

        services.AddSingleton(mongo)
            .AddScoped<IMongoCollection<EmulationState>>(_ => mongo.StatesCollection);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddGrpcClient<Compiler.CompilerClient>(o =>
        {
            o.Address = new Uri(config.GetSection("Grpc")["Address"]);
        });
        services
            .AddSingleton<GrpcCompilerService>()
            .AddSingleton(new RabbitMQConnection(config.GetConnectionString("RabbitMQ")!))
            .AddSingleton<IEmulationStatesService, EmulationStatesService>()
            .AddSingleton<IEmulationTaskManager, EmulationTaskManager>()
            .AddScoped<IEmulationStateRepository, EmulationStateRepository>();
        return services;
    }
}
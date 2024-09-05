namespace VRisc.Infrastructure;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using VRisc.Core.Entities;
using VRisc.Core.Interfaces;
using VRisc.Core.UseCases;
using VRisc.Infrastructure.Data;
using VRisc.Infrastructure.Repositories;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services.AddResources(config);

        var jwt = config.GetSection("Jwt");
        builder.Services.AddAuthentication(options =>
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

    private static IServiceCollection AddResources(this IServiceCollection services, ConfigurationManager config)
    {
        var conn = config.GetConnectionString("DefaultConnection")!;
        var database = config.GetSection("Mongo")["Database"]!;
        var mongo = new MongoContext(new MongoClient(conn), database);

        services.AddSingleton(mongo);
        services.AddScoped<IMongoCollection<EmulationState>>(_ => mongo.StatesCollection);

        {
            services.AddSingleton<IEmulationStatesService, EmulationStatesService>();
            services.AddSingleton<IEmulationTaskManager, EmulationTaskManger>();
            services.AddSingleton<IEmulator, DummyEmulator>();

            services.AddScoped<IEmulationStateRepository, EmulationStateRepository>();
        }

        return services;
    }
}
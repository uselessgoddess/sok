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

namespace VRisc.Infrastructure;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        var conn = builder.Configuration.GetConnectionString("DefaultConnection")!;
        var database = builder.Configuration.GetSection("Mongo")["Database"]!;
        var mongo = new MongoContext(new MongoClient(conn), database);
        
        builder.Services.AddSingleton(mongo);
        builder.Services.AddScoped<IMongoCollection<EmulationState>>(_ => mongo.StatesCollection);

        {
            builder.Services.AddSingleton<EmulationState>();
        }

        builder.Services.AddScoped<IEmulationStateRepository, EmulationStateRepository>();
        builder.Services.AddScoped<IEmulator, DummyEmulator>();

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
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.GetValue<string>("Issuer"),
                    ValidAudience = jwt.GetValue<string>("Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.GetValue<string>("Key"))),
                };
            });

        return builder;
    }
}
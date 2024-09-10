using Microsoft.OpenApi.Models;
using VRisc.Api;
using VRisc.Api.Hubs;
using VRisc.Api.Middlewares;
using VRisc.Infrastructure;
using VRisc.Presentation;
using VRisc.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructure();
builder.Services.AddUseCases();
builder.Services.AddApi();

builder.Services.AddControllers();
builder.Services.AddDataValidation();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme());
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            []
        },
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewares();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<EmulationHub>("/emu-hub");

app.Run();
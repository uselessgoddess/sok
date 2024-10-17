using VRisc.Api;
using VRisc.Api.Middlewares;
using VRisc.Infrastructure;
using VRisc.UseCases;
using VRisc.UseCases.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddUseCases()
    .AddApi();

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
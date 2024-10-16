using ApiGateway;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiGateway();

var app = builder.Build();

await app.UseOcelot();

app.Run();
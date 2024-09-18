using Api;
using Core;
using Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddCore();
builder.AddApi();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<CompilerService>();

app.Run();
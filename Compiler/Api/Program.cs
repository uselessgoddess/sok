using Compiler.Api;
using Compiler.Core;
using Compiler.Core.Services;
using Compiler.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddData().AddCore().AddApi();

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
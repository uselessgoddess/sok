using Identity.Api;
using Identity.Api.Middlewares;
using Identity.Infrastructure;
using Identity.Infrastructure.Data;
using Identity.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddInfrastructure(config);
builder.Services.AddUseCases();
builder.AddApi();

builder.Services.AddControllers();
builder.Services.AddDataValidation();

builder.Services.AddSwaggerGen(c => { c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()); });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.Migrate();
}

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

app.Run();
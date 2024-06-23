using CRMApi;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencyInjection();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddVersioning();
builder.Services.AddResponseCaching();

var app = builder.Build();

app.UseResponseCaching();
app.UseFastEndpoints(config =>
{
    config.Endpoints.RoutePrefix = "api";
    config.Versioning.Prefix = "v";
});
app.UseSwaggerGen();
app.Run();
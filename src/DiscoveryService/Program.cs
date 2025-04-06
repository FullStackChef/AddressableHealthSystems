using DiscoveryService.Endpoints;
using DiscoveryService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IDiscoveryProcessor, DiscoveryProcessor>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapDiscoveryEndpoints();

app.Run();

using PeerMessagingService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHttpClient();
builder.Services.AddScoped<PeerMessenger>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();

using MessagingService.Endpoints;
using MessagingService.Handlers;
using MessagingService.Services;
using DirectoryService.Services;
using PeerMessagingService.Services;
using Hl7.Fhir.Rest;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddDaprClient();


// If switching to Firely FhirClient SDK, use this instead:
builder.Services.AddSingleton(new FhirClient("https://your-fhir-server-url"));
builder.Services.AddSingleton<IFhirStorageService, FhirStorageService>();

// --- Core services ---
builder.Services.AddSingleton<IFhirClientAdapter, FhirClientAdapter>();
builder.Services.AddSingleton<IAuditService, AuditService>();
builder.Services.AddTransient<ICommunicationHandler, CommunicationHandler>();
builder.Services.AddSingleton<IPeerRegistryService, PeerRegistryService>();
builder.Services.AddScoped<PeerMessenger>();

// --- Auth ---
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanPostCommunication", policy =>
        policy.RequireAuthenticatedUser());

    options.AddPolicy("CanReadCommunication", policy =>
        policy.RequireAuthenticatedUser());
});


// --- OpenAPI ---
builder.Services.AddOpenApi();

// --- App ---
var app = builder.Build();

app.MapDefaultEndpoints();

app.UseAuthorization();
app.MapOpenApi();
app.MapCommunicationEndpoints();

app.Run();

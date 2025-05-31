using Client.Components;
using Client.Services;
using Hl7.Fhir.Rest;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

builder.Services.AddSingleton<IDocumentationService, FileDocumentationService>();
builder.Services.AddSingleton<ISettingsService, FileSettingsService>();

builder.Services.AddSingleton(new FhirClient("https://localhost:5172"));
builder.Services.AddSingleton<IFhirOrganizationClient, FhirOrganizationClient>();
builder.Services.AddSingleton<IOrganizationFhirService, FhirOrganizationService>();

builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

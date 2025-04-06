using ;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<FhirInMemoryStore>();
var app = builder.Build();

app.MapGet("/Communication", ([FromServices] FhirInMemoryStore store) =>
{
    return Results.Ok(store.GetAllCommunications());
});

app.MapGet("/Communication/{id}", ([FromServices] FhirInMemoryStore store, string id) =>
{
    var comm = store.GetCommunication(id);
    return comm is not null
        ? Results.Ok(comm)
        : Results.NotFound($"Communication/{id} not found.");
});

app.MapPost("/Communication", ([FromServices] FhirInMemoryStore store, [FromBody] Communication comm) =>
{
    var created = store.CreateCommunication(comm);
    return Results.Created($"/Communication/{created.Id}", created);
});

app.Run();

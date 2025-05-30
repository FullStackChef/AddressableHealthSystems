using Hl7.Fhir.Model;
using MessagingService.Handlers;
using Shared;

namespace MessagingService.Endpoints;

public static class CommunicationEndpoints
{
    public static IEndpointRouteBuilder MapCommunicationEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/communication/{id}", async (
            HttpContext context,
            string id,
            ICommunicationHandler handler
        ) =>
        {
            return await handler
                .GetCommunicationByIdAsync(context, id, context.RequestAborted)
                .ConfigureAwait(false);
        })
        .WithName("GetCommunicationById")
        .WithOpenApi(op =>
        {
            op.Summary = "Retrieve a Communication by ID";
            op.Description = "Returns a FHIR Communication resource if it exists and the caller is authorized.";
            return op;
        });


        endpoints.MapPost("/api/communication", async (
            HttpContext context,
            Communication communication,
            ICommunicationHandler handler
        ) =>
        {
            var cancellationToken = context.RequestAborted;
            return await handler
                .HandleIncomingCommunicationAsync(context, communication, DeliveryMode.StoreAndForward, cancellationToken)
                .ConfigureAwait(false);
        })
        .WithName("PostCommunication")
        .WithOpenApi(op =>
        {
            op.Summary = "Submit a FHIR Communication resource";
            op.Description = "Accepts a FHIR Communication JSON payload and stores it in the FHIR server.";
            return op;
        });

        return endpoints;
    }
}

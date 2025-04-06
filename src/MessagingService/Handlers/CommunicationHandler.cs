using System.Threading;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using MessagingService.Services;
using Dapr.Client;
using Shared;

namespace MessagingService.Handlers;

public interface ICommunicationHandler
{
    ValueTask<Results<Ok<string>, ProblemHttpResult, ForbidHttpResult>>
        HandleIncomingCommunicationAsync(HttpContext httpContext, Communication communication, CancellationToken cancellationToken);
    ValueTask<Results<Ok<Communication>, NotFound, ForbidHttpResult>>
        GetCommunicationByIdAsync(HttpContext httpContext, string id, CancellationToken cancellationToken);


}

public partial class CommunicationHandler(
    IFhirStorageService fhirStorageService,
    ILogger<CommunicationHandler> logger,
    IAuditService auditService,
    IAuthorizationService authorizationService,
    DaprClient daprClient
) : ICommunicationHandler
{
    public async ValueTask<
        Results<Ok<string>, ProblemHttpResult, ForbidHttpResult>
    > HandleIncomingCommunicationAsync(
        HttpContext httpContext,
        Communication communication,
        CancellationToken cancellationToken
    )
    {
        // Authorization check
        var authResult = await authorizationService
            .AuthorizeAsync(httpContext.User, "CanPostCommunication")
            .ConfigureAwait(false);

        if (!authResult.Succeeded)
        {
            Log.AuthorizationFailed(logger, httpContext.User?.Identity?.Name ?? "Unknown");
            return TypedResults.Forbid();
        }

        // Store the Communication in FHIR server
        var (Success, ResourceId) = await fhirStorageService
            .StoreResourceAsync(communication, cancellationToken)
            .ConfigureAwait(false);

        if (!Success || string.IsNullOrEmpty(ResourceId))
        {
            Log.CommunicationStorageFailed(logger, null);
            return TypedResults.Problem("Failed to store Communication resource.");
        }



        // Audit log
        await auditService
            .RecordAuditAsync(
                httpContext.User?.Identity?.Name ?? "Unknown",
                "CreateCommunication",
                ResourceId,
                "Stored FHIR Communication resource.")
            .ConfigureAwait(false);

        Log.CommunicationStored(logger, ResourceId);


        // Publish the Communication to Dapr pub/sub topic

        await daprClient.PublishEventAsync("pubsub", "messaging-delivery", new CommunicationDeliveryEvent(ResourceId), cancellationToken)
                        .ConfigureAwait(false);

        Log.DeliveryWorkflowTriggered(logger, ResourceId);

        await auditService
            .RecordAuditAsync(
                user: httpContext.User?.Identity?.Name ?? "Unknown",
                action: "TriggerDeliveryWorkflow",
                resourceId: ResourceId,
                details: "Published Communication delivery event to pubsub.")
            .ConfigureAwait(false);


        return TypedResults.Ok($"Communication stored successfully. ResourceId: {ResourceId}");

    }

    private static partial class Log
    {
        [LoggerMessage(EventId = 200, Level = LogLevel.Warning,
            Message = "Authorization failed for user '{User}' attempting to post Communication.")]
        public static partial void AuthorizationFailed(ILogger logger, string user);

        [LoggerMessage(EventId = 201, Level = LogLevel.Error,
            Message = "Failed to store Communication resource. ResourceId: {ResourceId}")]
        public static partial void CommunicationStorageFailed(ILogger logger, string? resourceId);


        [LoggerMessage(EventId = 202, Level = LogLevel.Information,
            Message = "Successfully stored Communication resource. ResourceId: {ResourceId}")]
        public static partial void CommunicationStored(ILogger logger, string? resourceId);

        [LoggerMessage(EventId = 205, Level = LogLevel.Information,
            Message = "Published delivery event for Communication resource {ResourceId} to Dapr pubsub.")]
        public static partial void DeliveryWorkflowTriggered(ILogger logger, string resourceId);

    }

    public async ValueTask<Results<Ok<Communication>, NotFound, ForbidHttpResult>> GetCommunicationByIdAsync(
    HttpContext httpContext,
    string id,
    CancellationToken cancellationToken)
    {
        var user = httpContext.User;

        // Authorization
        var authResult = await authorizationService
            .AuthorizeAsync(user, null, "CanReadCommunication")
            .ConfigureAwait(false);

        if (!authResult.Succeeded)
        {
            Log.AuthorizationFailed(logger, user?.Identity?.Name ?? "Unknown");
            return TypedResults.Forbid();
        }

        // Retrieve the communication
        var resource = await fhirStorageService
            .GetCommunicationByIdAsync(id, cancellationToken)
            .ConfigureAwait(false);

        if (resource is null)
        {
            Log.CommunicationNotFound(logger, id);
            return TypedResults.NotFound();
        }


        Log.CommunicationReadAccessed(logger, user?.Identity?.Name ?? "Unknown", id);

        await auditService.RecordAuditAsync(
            user?.Identity?.Name ?? "Unknown",
            "ReadCommunication",
            id,
            "Accessed Communication resource"
        ).ConfigureAwait(false);

        return TypedResults.Ok(resource);
    }

    private static partial class Log
    {
        [LoggerMessage(EventId = 203, Level = LogLevel.Warning,
            Message = "Communication resource with ID '{ResourceId}' was not found.")]
        public static partial void CommunicationNotFound(ILogger logger, string resourceId);

        [LoggerMessage(EventId = 206, Level = LogLevel.Information,
            Message = "User '{User}' accessed Communication resource with ID '{ResourceId}'")]
        public static partial void CommunicationReadAccessed(ILogger logger, string user, string resourceId);

    }
}
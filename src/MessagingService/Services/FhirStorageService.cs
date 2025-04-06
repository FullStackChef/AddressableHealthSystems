using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace MessagingService.Services;

public interface IFhirStorageService
{
    ValueTask<(bool Success, string? ResourceId)> StoreResourceAsync(DomainResource resource, CancellationToken cancellationToken = default);
    ValueTask<Communication?> GetCommunicationByIdAsync(string id, CancellationToken cancellationToken = default);
}


public partial class FhirStorageService(IFhirClientAdapter  client, ILogger<FhirStorageService> logger) : IFhirStorageService
{
    public async ValueTask<(bool Success, string? ResourceId)> StoreResourceAsync(DomainResource resource, CancellationToken cancellationToken = default)
    {
        try
        {
            var created = await client.CreateAsync(resource,  cancellationToken).ConfigureAwait(false);

            if (created is null)
            {
                Log.FhirCreateReturnedNull(logger);
                return (false, null);
            }

            return (true, created.Id);
        }
        catch (FhirOperationException ex)
        {
            Log.FhirCreateFailed(logger, ex.Message);
            return (false, null);
        }
    }


    private static partial class Log
    {
        [LoggerMessage(EventId = 300, Level = LogLevel.Error, Message = "FHIR create failed: {Message}")]
        public static partial void FhirCreateFailed(ILogger logger, string message);

        [LoggerMessage(EventId = 301, Level = LogLevel.Warning, Message = "FHIR client returned null on create.")]
        public static partial void FhirCreateReturnedNull(ILogger logger);
    }

    public async ValueTask<Communication?> GetCommunicationByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var resource = await client.ReadCommunicationAsync($"Communication/{id}",  cancellationToken).ConfigureAwait(false);
            return resource;
        }
        catch (FhirOperationException ex) when (ex.Status == System.Net.HttpStatusCode.NotFound)
        {
            Log.CommunicationNotFound(logger, id);
            return null;
        }
        catch (Exception ex)
        {
            Log.FhirReadFailed(logger, ex.Message);
            return null;
        }
    }

    private static partial class Log
    {
        [LoggerMessage(EventId = 302, Level = LogLevel.Warning, Message = "Communication/{Id} was not found.")]
        public static partial void CommunicationNotFound(ILogger logger, string id);

        [LoggerMessage(EventId = 303, Level = LogLevel.Error, Message = "Failed to read Communication: {Message}")]
        public static partial void FhirReadFailed(ILogger logger, string message);
    }

}

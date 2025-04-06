using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace MessagingService.Services;

public interface IFhirClientAdapter
{
    Task<DomainResource?> CreateAsync(DomainResource resource, CancellationToken cancellationToken = default);
    Task<Communication?> ReadCommunicationAsync(string id, CancellationToken cancellationToken = default);
}
public class FhirClientAdapter(FhirClient client) : IFhirClientAdapter
{
    public Task<DomainResource?> CreateAsync(DomainResource resource, CancellationToken cancellationToken = default)
    {
        return client.CreateAsync(resource, ct: cancellationToken);
    }

    public Task<Communication?> ReadCommunicationAsync(string id, CancellationToken cancellationToken = default)
    {
        return client.ReadAsync<Communication>($"Communication/{id}", ct: cancellationToken);
    }
}

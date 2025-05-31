using System.Net;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.Extensions.Logging;

namespace Client.Services;

public interface IFhirOrganizationClient
{
    Task<Organization?> ReadAsync(string id, CancellationToken cancellationToken = default);
    Task<Organization?> UpdateAsync(Organization organization, CancellationToken cancellationToken = default);
}

public class FhirOrganizationClient(FhirClient client) : IFhirOrganizationClient
{
    public Task<Organization?> ReadAsync(string id, CancellationToken cancellationToken = default)
        => client.ReadAsync<Organization>($"Organization/{id}", ct: cancellationToken);

    public Task<Organization?> UpdateAsync(Organization organization, CancellationToken cancellationToken = default)
        => client.UpdateAsync(organization, ct: cancellationToken);
}

public interface IOrganizationFhirService
{
    Task<Organization?> GetOrganizationAsync(string id, CancellationToken ct = default);
    Task<bool> SaveOrganizationAsync(Organization organization, CancellationToken ct = default);
}

public class FhirOrganizationService(IFhirOrganizationClient client, ILogger<FhirOrganizationService> logger) : IOrganizationFhirService
{
    public async Task<Organization?> GetOrganizationAsync(string id, CancellationToken ct = default)
    {
        try
        {
            return await client.ReadAsync(id, ct).ConfigureAwait(false);
        }
        catch (FhirOperationException ex) when (ex.Status == HttpStatusCode.NotFound)
        {
            logger.LogWarning("Organization {Id} not found", id);
            return null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to read Organization {Id}", id);
            return null;
        }
    }

    public async Task<bool> SaveOrganizationAsync(Organization organization, CancellationToken ct = default)
    {
        try
        {
            var result = await client.UpdateAsync(organization, ct).ConfigureAwait(false);
            return result is not null;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update Organization {Id}", organization.Id);
            return false;
        }
    }
}

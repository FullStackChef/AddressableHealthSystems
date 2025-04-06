using System.Net.Http.Json;
using DiscoveryService.Models;

namespace DiscoveryService.Services;

public class DiscoveryProcessor(HttpClient httpClient) : IDiscoveryProcessor
{
    public async Task<DiscoveryResult> DiscoverAsync(DiscoveryRequest request, CancellationToken cancellationToken)
    {
        var metadataUrl = $"{request.EndpointUrl.TrimEnd('/')}/metadata";

        var capability = await httpClient.GetFromJsonAsync<RemoteCapabilityStatement>(metadataUrl, cancellationToken);

        if (capability == null)
            throw new InvalidOperationException("Remote system did not return a capability statement.");

        return new DiscoveryResult
        {
            RemoteName = request.EndpointUrl,
            SoftwareName = capability.SoftwareName ?? "Unknown",
            Version = capability.SoftwareVersion ?? "Unknown",
            IsAhsCompatible = capability.IsAhsCompatible
        };
    }
}

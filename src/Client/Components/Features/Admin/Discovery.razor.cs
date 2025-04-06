using Microsoft.AspNetCore.Components;

namespace Client.Components.Features.Admin;

public partial class Discovery : ComponentBase
{
    private readonly HttpClient httpClient = new();

    protected DiscoveryRequest discoveryRequest = new();
    protected DiscoveryResult? discoveryResult;

    protected async Task RunDiscovery(DiscoveryRequest request)
    {
        discoveryResult = null;

        try
        {
            var response = await httpClient.PostAsJsonAsync("https://localhost:5001/discover", request);

            if (response.IsSuccessStatusCode)
            {
                discoveryResult = await response.Content.ReadFromJsonAsync<DiscoveryResult>();
            }
            else
            {
                discoveryResult = CreateErrorResult(request.EndpointUrl, "Failed response");
            }
        }
        catch (Exception ex)
        {
            discoveryResult = CreateErrorResult(request.EndpointUrl, ex.Message);
        }
    }

    protected Task SyncToDirectory()
    {
        // TODO: Call DirectoryService to sync Organization + Endpoint
        Console.WriteLine($"[Sync] Pushing {discoveryResult?.RemoteName} to local directory...");
        return Task.CompletedTask;
    }

    private DiscoveryResult CreateErrorResult(string url, string errorMessage) => new()
    {
        RemoteName = url,
        SoftwareName = $"Error: {errorMessage}",
        Version = "N/A",
        IsAhsCompatible = false,
        IsTrusted = false,
        Role = "unknown"
    };

    public class DiscoveryRequest
    {
        public string EndpointUrl { get; set; } = string.Empty;
    }

    public class DiscoveryResult
    {
        public string RemoteName { get; set; } = string.Empty;
        public string SoftwareName { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsAhsCompatible { get; set; }
        public bool IsTrusted { get; set; }
        public string? CertificateThumbprint { get; set; }
        public List<string> SupportedResources { get; set; } = new();
    }
}

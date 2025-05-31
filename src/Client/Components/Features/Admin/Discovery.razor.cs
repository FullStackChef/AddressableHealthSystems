using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Components.Features.Admin;

public partial class Discovery : ComponentBase
{
    [Inject] public HttpClient HttpClient { get; set; } = new();

    protected DiscoveryRequest discoveryRequest = new();
    protected DiscoveryResult? discoveryResult;

    protected async Task RunDiscovery(DiscoveryRequest request)
    {
        discoveryResult = null;

        try
        {
            var response = await HttpClient.PostAsJsonAsync("https://localhost:5001/discover", request);

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

    protected async Task SyncToDirectory()
    {
        if (discoveryResult is null)
            return;

        var peer = new
        {
            id = discoveryResult.RemoteName,
            baseUrl = discoveryRequest.EndpointUrl,
            messagingEndpoint = discoveryRequest.EndpointUrl,
            isTrusted = discoveryResult.IsTrusted
        };

        Console.WriteLine($"[Sync] Pushing {discoveryResult.RemoteName} to local directory...");

        try
        {
            var response = await HttpClient.PostAsJsonAsync("http://localhost:5131/peers", peer);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Sync] Failed: {ex.Message}");
        }
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

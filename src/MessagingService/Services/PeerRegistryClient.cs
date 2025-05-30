using System.Net.Http.Json;
using DirectoryService.Services;
using Shared;

namespace MessagingService.Services;

public class PeerRegistryClient(HttpClient httpClient) : IPeerRegistryService
{
    public async Task<IReadOnlyList<PeerInfo>> GetPeersAsync(CancellationToken ct = default)
    {
        var peers = await httpClient.GetFromJsonAsync<IReadOnlyList<PeerInfo>>("/peers", ct).ConfigureAwait(false);
        return peers ?? [];
    }

    public async Task AddOrUpdatePeerAsync(PeerInfo peer, CancellationToken ct = default)
    {
        await httpClient.PostAsJsonAsync("/peers", peer, ct).ConfigureAwait(false);
    }

    public async Task<PeerInfo?> GetPeerAsync(string id, CancellationToken ct = default)
    {
        var response = await httpClient.GetAsync($"/peers/{id}", ct).ConfigureAwait(false);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PeerInfo>(cancellationToken: ct).ConfigureAwait(false);
    }
}

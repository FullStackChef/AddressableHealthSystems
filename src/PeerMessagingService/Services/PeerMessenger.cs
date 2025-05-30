using System.Net.Http.Json;
using Hl7.Fhir.Model;
using Shared;

namespace PeerMessagingService.Services;

public class PeerMessenger(HttpClient httpClient)
{
    public async Task<bool> SendCommunicationAsync(PeerInfo peer, Communication comm, CancellationToken ct = default)
    {
        var response = await httpClient.PostAsJsonAsync(peer.MessagingEndpoint, comm, ct).ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }
}

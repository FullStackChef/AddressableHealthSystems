using Microsoft.AspNetCore.Components;

namespace Client.Components.Features.System;

public partial class Federation : ComponentBase
{
    protected List<FederationPeer> federatedPeers = new();

    protected override void OnInitialized()
    {
        federatedPeers = new()
        {
            new FederationPeer
            {
                Url = "https://ahs.partner-one.org/fhir",
                Role = "Mutual",
                LastDiscovery = DateTimeOffset.UtcNow.AddDays(-1),
                TrustStatus = "✅ Trusted",
                SyncStatus = "OK"
            },
            new FederationPeer
            {
                Url = "https://ahs.lab-network.org/fhir",
                Role = "Spoke",
                LastDiscovery = DateTimeOffset.UtcNow.AddHours(-3),
                TrustStatus = "❌ Unverified",
                SyncStatus = "Out of sync"
            }
        };
    }

    protected void OnSelect(FederationPeer peer)
    {
        Console.WriteLine($"Selected: {peer.Url}");
    }

    protected void Rediscover(FederationPeer peer)
    {
        Console.WriteLine($"[Discovery] Triggering rediscovery for {peer.Url}");
        // Later: Call DiscoveryService
    }

    protected void SyncPeer(FederationPeer peer)
    {
        Console.WriteLine($"[Sync] Initiating sync for {peer.Url}");
        // Later: Call FederationSyncService
    }

    public class FederationPeer
    {
        public string Url { get; set; } = default!;
        public string Role { get; set; } = default!;
        public DateTimeOffset LastDiscovery { get; set; }
        public string TrustStatus { get; set; } = default!;
        public string SyncStatus { get; set; } = default!;
    }
}

using System.Collections.Concurrent;
using Shared;

namespace DirectoryService.Services;

public interface IPeerRegistryService
{
    Task<IReadOnlyList<PeerInfo>> GetPeersAsync(CancellationToken ct = default);
    Task AddOrUpdatePeerAsync(PeerInfo peer, CancellationToken ct = default);
    Task<PeerInfo?> GetPeerAsync(string id, CancellationToken ct = default);
}

public class PeerRegistryService : IPeerRegistryService
{
    private readonly ConcurrentDictionary<string, PeerInfo> _peers = new();

    public Task<IReadOnlyList<PeerInfo>> GetPeersAsync(CancellationToken ct = default)
    {
        IReadOnlyList<PeerInfo> list = _peers.Values.ToList();
        return Task.FromResult(list);
    }

    public Task AddOrUpdatePeerAsync(PeerInfo peer, CancellationToken ct = default)
    {
        _peers[peer.Id] = peer;
        return Task.CompletedTask;
    }

    public Task<PeerInfo?> GetPeerAsync(string id, CancellationToken ct = default)
    {
        _peers.TryGetValue(id, out var peer);
        return Task.FromResult(peer);
    }
}

using DiscoveryService.Models;

namespace DiscoveryService.Services;

public interface IDiscoveryProcessor
{
    Task<DiscoveryResult> DiscoverAsync(DiscoveryRequest request, CancellationToken cancellationToken);
}

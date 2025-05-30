using DirectoryService.Services;
using Shared;

namespace DirectoryService.Endpoints;

public static class PeerRegistryEndpoints
{
    public static IEndpointRouteBuilder MapPeerRegistryEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/peers", async (IPeerRegistryService registry, CancellationToken ct) =>
        {
            var peers = await registry.GetPeersAsync(ct).ConfigureAwait(false);
            return Results.Ok(peers);
        });

        endpoints.MapGet("/peers/{id}", async (string id, IPeerRegistryService registry, CancellationToken ct) =>
        {
            var peer = await registry.GetPeerAsync(id, ct).ConfigureAwait(false);
            return peer is null ? Results.NotFound() : Results.Ok(peer);
        });

        endpoints.MapPost("/peers", async (PeerInfo peer, IPeerRegistryService registry, CancellationToken ct) =>
        {
            await registry.AddOrUpdatePeerAsync(peer, ct).ConfigureAwait(false);
            return Results.Ok();
        });

        return endpoints;
    }
}

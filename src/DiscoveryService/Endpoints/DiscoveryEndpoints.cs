using DiscoveryService.Models;
using DiscoveryService.Services;

namespace DiscoveryService.Endpoints;

public static class DiscoveryEndpoints
{
    public static IEndpointRouteBuilder MapDiscoveryEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/discover", async (
            DiscoveryRequest request,
            IDiscoveryProcessor processor,
            CancellationToken ct) =>
        {
            try
            {
                var result = await processor.DiscoverAsync(request, ct);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });

        return endpoints;
    }
}

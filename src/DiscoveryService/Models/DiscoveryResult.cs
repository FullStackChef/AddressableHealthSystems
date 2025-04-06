namespace DiscoveryService.Models;

public class DiscoveryResult
{
    public string RemoteName { get; set; } = default!;
    public string SoftwareName { get; set; } = default!;
    public string Version { get; set; } = default!;
    public bool IsAhsCompatible { get; set; }
}

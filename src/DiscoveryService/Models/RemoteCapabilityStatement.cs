namespace DiscoveryService.Models;

public class RemoteCapabilityStatement
{
    public string? SoftwareName { get; set; }
    public string? SoftwareVersion { get; set; }
    public bool IsAhsCompatible => SoftwareName?.Contains("AddressableHealthSystems") == true;
}

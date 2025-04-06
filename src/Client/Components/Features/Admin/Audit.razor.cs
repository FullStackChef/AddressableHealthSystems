using Microsoft.AspNetCore.Components;

namespace Client.Components.Features.Admin;

public partial class Audit : ComponentBase
{
    protected List<AuditEventModel> auditEvents = new();

    protected override void OnInitialized()
    {
        auditEvents = new()
        {
            new() {
                Timestamp = DateTimeOffset.UtcNow.AddMinutes(-2),
                EventType = "DiscoveryRequest",
                Actor = "admin@clinic.org",
                Target = "https://remote.ahs.org/fhir",
                Status = "Success",
                CorrelationId = "abc123"
            },
            new() {
                Timestamp = DateTimeOffset.UtcNow.AddMinutes(-5),
                EventType = "SyncComplete",
                Actor = "admin@clinic.org",
                Target = "https://remote.ahs.org/fhir",
                Status = "Success",
                CorrelationId = "abc123"
            },
            new() {
                Timestamp = DateTimeOffset.UtcNow.AddHours(-1),
                EventType = "EndpointUpdate",
                Actor = "admin@clinic.org",
                Target = "Central Clinic Endpoint",
                Status = "Error",
                CorrelationId = "xyz789"
            }
        };
    }

    public class AuditEventModel
    {
        public DateTimeOffset Timestamp { get; set; }
        public string EventType { get; set; } = default!;
        public string Actor { get; set; } = default!;
        public string Target { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string CorrelationId { get; set; } = default!;
    }
}

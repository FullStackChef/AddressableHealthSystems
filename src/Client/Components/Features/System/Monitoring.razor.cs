using Microsoft.AspNetCore.Components;

namespace Client.Components.Features.System;

public partial class Monitoring : ComponentBase
{
    protected MonitoringStats stats = new();

    protected override void OnInitialized()
    {
        // Placeholder values
        stats = new MonitoringStats
        {
            TotalMessages = 4233,
            ActiveTenants = 22,
            FailedDeliveries = 3,
            FederationHealthy = true,
            AuditQueueStatus = "Idle"
        };
    }

    public class MonitoringStats
    {
        public int TotalMessages { get; set; }
        public int ActiveTenants { get; set; }
        public int FailedDeliveries { get; set; }
        public bool FederationHealthy { get; set; }
        public string AuditQueueStatus { get; set; } = default!;
    }
}

using Microsoft.AspNetCore.Components;

namespace Client.Components.Features.System;

public partial class TenantDetail : ComponentBase
{
    [Parameter] public string OrgId { get; set; } = default!;

    protected TenantModel? tenant;
    protected List<string> statuses = new() { "Active", "Suspended" };

    protected override void OnInitialized()
    {
        // Simulated fetch - in real app, get from TenantService
        var allTenants = new List<TenantModel>
        {
            new() { Name = "Greenfield Medical", OrgId = "org-1001", Status = "Active", MessageVolume = 145, AgentUrl = "https://greenfield-agent.local" },
            new() { Name = "Lakeside Lab Group", OrgId = "org-2003", Status = "Active", MessageVolume = 82, AgentUrl = "https://lakeside.local/agent" },
            new() { Name = "Northside Hospital", OrgId = "org-3110", Status = "Suspended", MessageVolume = 0, AgentUrl = null }
        };

        tenant = allTenants.FirstOrDefault(t => t.OrgId == OrgId);
    }

    protected Task Save(TenantModel model)
    {
        tenant = model;
        Console.WriteLine($"Saved tenant: {tenant?.Name} / {tenant?.Status}");
        return Task.CompletedTask;
    }


    public class TenantModel
    {
        public string Name { get; set; } = default!;
        public string OrgId { get; set; } = default!;
        public string Status { get; set; } = "Active";
        public int MessageVolume { get; set; }
        public string? AgentUrl { get; set; }
    }
}

using Microsoft.AspNetCore.Components;

namespace Client.Components.Features.System;

public partial class Tenants : ComponentBase
{
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    protected List<TenantModel> tenants = new();

    protected override void OnInitialized()
    {
        tenants = new()
        {
            new() { Name = "Greenfield Medical", OrgId = "org-1001", Status = "Active", MessageVolume = 145, AgentUrl = "https://greenfield-agent.local" },
            new() { Name = "Lakeside Lab Group", OrgId = "org-2003", Status = "Active", MessageVolume = 82, AgentUrl = "https://lakeside.local/agent" },
            new() { Name = "Northside Hospital", OrgId = "org-3110", Status = "Suspended", MessageVolume = 0, AgentUrl = null }
        };
    }

    protected void OnSelect(TenantModel tenant)
    {
        Console.WriteLine($"Selected tenant: {tenant.Name}");
    }


    protected void NavigateToTenant(string orgId)
    {
        NavigationManager.NavigateTo($"/system/tenants/{orgId}");
    }


    protected void SuspendTenant(TenantModel tenant)
    {
        tenant.Status = "Suspended";
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

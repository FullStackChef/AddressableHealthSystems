using Microsoft.AspNetCore.Components;

namespace Client.Components.Features.Admin;

public partial class Directory : ComponentBase
{
    protected List<OrganizationModel> organizations = new();
    protected List<PractitionerModel> practitioners = new();

    protected override void OnInitialized()
    {
        // Mock orgs
        organizations = new()
        {
            new() { Name = "Central Clinic", Type = "clinic", Address = "123 Main St" },
            new() { Name = "Regional Hospital", Type = "hospital", Address = "456 Health Rd" }
        };

        // Mock practitioners
        practitioners = new()
        {
            new() { FullName = "Dr. Alice Moore", Role = "General Practitioner", OrgName = "Central Clinic" },
            new() { FullName = "Dr. Ben Wright", Role = "Cardiologist", OrgName = "Regional Hospital" }
        };
    }

    public class OrganizationModel
    {
        public string Name { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Address { get; set; } = default!;
    }

    public class PractitionerModel
    {
        public string FullName { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string OrgName { get; set; } = default!;
    }
}

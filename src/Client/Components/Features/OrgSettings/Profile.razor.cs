using Microsoft.AspNetCore.Components;

namespace Client.Components.Features.OrgSettings;

public partial class Profile : ComponentBase
{
    protected OrgProfile org = new();

    protected override void OnInitialized()
    {
        // TODO: Load from FHIR Organization resource
        org = new OrgProfile
        {
            Name = "Greenfield Medical Group",
            Identifier = "urn:oid:2.16.840.1.113883.19.5",
            Email = "info@greenfield.org",
            Phone = "(555) 123-4567",
            Address = "123 Main Street, Springfield, ST 99999"
        };
    }

    protected Task Save(OrgProfile updated)
    {
        org = updated;
        Console.WriteLine($"Saved org: {org.Name} / {org.Identifier}");
        // TODO: PUT to FHIR /Organization/{id}
        return Task.CompletedTask;
    }

    public class OrgProfile
    {
        public string Name { get; set; } = string.Empty;
        public string Identifier { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}

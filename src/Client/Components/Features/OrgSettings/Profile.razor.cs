using Microsoft.AspNetCore.Components;
using Hl7.Fhir.Model;
using Client.Services;

namespace Client.Components.Features.OrgSettings;

public partial class Profile : ComponentBase
{
    private const string OrgId = "local-org";

    [Inject] public IOrganizationFhirService FhirService { get; set; } = default!;

    protected OrgProfile org = new();

    protected override async Task OnInitializedAsync()
    {
        var resource = await FhirService.GetOrganizationAsync(OrgId);
        if (resource is not null)
        {
            org = new OrgProfile
            {
                Name = resource.Name ?? string.Empty,
                Identifier = resource.Identifier?.FirstOrDefault()?.Value ?? string.Empty,
                Email = resource.Telecom?.FirstOrDefault(t => t.System == ContactPoint.ContactPointSystem.Email)?.Value ?? string.Empty,
                Phone = resource.Telecom?.FirstOrDefault(t => t.System == ContactPoint.ContactPointSystem.Phone)?.Value ?? string.Empty,
                Address = resource.Address?.FirstOrDefault()?.Text ?? string.Empty
            };
        }
    }

    protected async Task Save(OrgProfile updated)
    {
        org = updated;

        var resource = new Organization
        {
            Id = OrgId,
            Name = org.Name,
            Identifier = [ new Identifier { Value = org.Identifier } ],
            Telecom =
            [
                new ContactPoint(ContactPoint.ContactPointSystem.Email, null, org.Email),
                new ContactPoint(ContactPoint.ContactPointSystem.Phone, null, org.Phone)
            ],
            Address = [ new Address { Text = org.Address } ]
        };

        await FhirService.SaveOrganizationAsync(resource);
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

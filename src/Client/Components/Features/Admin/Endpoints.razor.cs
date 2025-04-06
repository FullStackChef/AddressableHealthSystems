using Microsoft.AspNetCore.Components;

namespace Client.Components.Features.Admin;

public partial class Endpoints : ComponentBase
{
    protected List<EndpointModel> endpoints = new();

    protected override void OnInitialized()
    {
        // Mock endpoint data (normally fetched from DirectoryService or FHIR API)
        endpoints = new()
        {
            new EndpointModel
            {
                Id = "ep1",
                Name = "Dr. Smith's PMS",
                Address = "https://drsmith-pms.org/fhir",
                Status = "active",
                ConnectionType = "hl7-fhir-rest"
            },
            new EndpointModel
            {
                Id = "ep2",
                Name = "LabCorp FHIR",
                Address = "https://labcorp.io/fhir",
                Status = "active",
                ConnectionType = "hl7-fhir-rest"
            },
            new EndpointModel
            {
                Id = "ep3",
                Name = "Legacy HL7 Listener",
                Address = "tcp://10.1.1.5:2575",
                Status = "suspended",
                ConnectionType = "hl7-v2-mllp"
            }
        };
    }

    protected void OnRowSelect(EndpointModel selected)
    {
        // Optionally show more detail or navigate to edit
        Console.WriteLine($"Selected: {selected.Name}");
    }

    protected void ValidateEndpoint(EndpointModel endpoint)
    {
        // Placeholder: Later you might ping certs, test connectivity, etc.
        Console.WriteLine($"Validating: {endpoint.Address}");
    }

    public class EndpointModel
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string ConnectionType { get; set; } = default!;
    }
}

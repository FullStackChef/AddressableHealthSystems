using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Components.Features.OrgSettings;

public partial class Certs : ComponentBase
{
    protected List<CertModel> certs = new();

    protected override void OnInitialized()
    {
        certs = new()
        {
            new() {
                CommonName = "greenfield.org",
                Thumbprint = "ABCD1234EF567890...",
                Expires = DateTimeOffset.UtcNow.AddMonths(6),
                TrustType = "Inbound",
                Status = "Active"
            },
            new() {
                CommonName = "test.partner.local",
                Thumbprint = "9988AABBCCDD...",
                Expires = DateTimeOffset.UtcNow.AddDays(-10),
                TrustType = "Outbound",
                Status = "Expired"
            }
        };
    }

    protected Task Revoke(CertModel cert)
    {
        Console.WriteLine($"Revoking cert: {cert.Thumbprint}");
        cert.Status = "Revoked";
        return Task.CompletedTask;
    }

    protected async Task OnFileUpload(InputFileChangeEventArgs args)
    {
        var file = args.File;

        // NOTE: Simulated file handling. Real app would parse X.509 cert and extract CN, thumbprint, etc.
        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();

        certs.Add(new CertModel
        {
            CommonName = $"Uploaded {DateTimeOffset.UtcNow:u}",
            Thumbprint = Guid.NewGuid().ToString("N").ToUpper(),
            Expires = DateTimeOffset.UtcNow.AddYears(1),
            TrustType = "Inbound",
            Status = "Active"
        });
    }

    public class CertModel
    {
        public string CommonName { get; set; } = string.Empty;
        public string Thumbprint { get; set; } = string.Empty;
        public DateTimeOffset Expires { get; set; }
        public string TrustType { get; set; } = "Inbound";
        public string Status { get; set; } = "Active";
    }
}

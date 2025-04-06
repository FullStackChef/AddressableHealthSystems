using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace Client.Components.Features.Inbox;

public partial class MessageDetail : ComponentBase
{
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    [Parameter] public string Id { get; set; } = default!;
    protected MessageDetailModel? Message { get; set; }

    protected void GoBack()
    {
        NavigationManager.NavigateTo("/inbox");
    }
    protected override void OnInitialized()
    {
        // Simulated message fetch
        List<MessageDetailModel> all =
        [
            new() { Id = "msg-001", Subject = "Referral for John Smith", From = "Dr.Jones", Received = DateTimeOffset.UtcNow.AddHours(-2), Body = "Please review this patient's case." },
            new() { Id = "msg-002", Subject = "Lab results for Jane Doe", From = "PathLab Central", Received = DateTimeOffset.UtcNow.AddDays(-1), Body = "Lab results are attached in FHIR bundle." }
        ];

        Message = all.FirstOrDefault(m => m.Id == Id);
    }

    public class MessageDetailModel
    {
        public string Id { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public string From { get; set; } = default!;
        public string Body { get; set; } = default!;
        public DateTimeOffset Received { get; set; }
    }
}
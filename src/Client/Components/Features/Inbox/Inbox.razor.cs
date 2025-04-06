using Microsoft.AspNetCore.Components;

namespace Client.Components.Features.Inbox;

public partial class Inbox : ComponentBase
{
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

    protected List<MessageSummary> inbox =
    [
        new MessageSummary { Id = "msg-001", Subject = "Referral for John Smith", From = "Dr. Jones", Received = DateTimeOffset.UtcNow.AddHours(-2) },
        new MessageSummary { Id = "msg-002", Subject = "Lab results for Jane Doe", From = "PathLab Central", Received = DateTimeOffset.UtcNow.AddDays(-1) }
    ];

    protected void OnRowSelect(MessageSummary msg)
    {
        ViewMessage(msg.Id);
    }
    protected void ViewMessage(string id)
    {
        NavigationManager.NavigateTo($"/messages/{id}");
    }

}

public class MessageSummary
{
    public string Id { get; set; } = default!;
    public string Subject { get; set; } = default!;
    public string From { get; set; } = default!;
    public DateTimeOffset Received { get; set; }
}
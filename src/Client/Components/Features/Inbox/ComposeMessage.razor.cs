using Microsoft.AspNetCore.Components;

namespace Client.Components.Features.Inbox;

public partial class ComposeMessage : ComponentBase
{
    protected OutgoingMessage message = new();
    protected List<RecipientOption> recipientOptions = new();

    protected override void OnInitialized()
    {
        // Simulated recipient list
        recipientOptions = new()
        {
            new() { Id = "org-1001", Display = "Greenfield Medical Group" },
            new() { Id = "org-2003", Display = "Lakeside Lab Group" }
        };

    }

    protected Task SendMessage(OutgoingMessage outgoingMessage)
    {
        Console.WriteLine($"[SEND] To: {message.To}, Subject: {message.Subject}");
        // TODO: POST to message dispatch API (e.g., FHIR Communication or message router)
        return Task.CompletedTask;
    }

    public class OutgoingMessage
    {
        public string To { get; set; } = default!;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }

    public class RecipientOption
    {
        public string Id { get; set; } = default!;
        public string Display { get; set; } = default!;
    }
}

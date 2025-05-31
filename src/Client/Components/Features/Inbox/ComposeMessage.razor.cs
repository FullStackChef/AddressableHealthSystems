using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Components.Features.Inbox;

public partial class ComposeMessage : ComponentBase
{
    [Inject] public HttpClient HttpClient { get; set; } = default!;

    protected OutgoingMessage message = new();
    protected List<RecipientOption> recipientOptions = new();
    protected string? statusMessage;

    protected override void OnInitialized()
    {
        // Simulated recipient list
        recipientOptions = new()
        {
            new() { Id = "org-1001", Display = "Greenfield Medical Group" },
            new() { Id = "org-2003", Display = "Lakeside Lab Group" }
        };

    }

    protected async Task SendMessage(OutgoingMessage outgoingMessage)
    {
        Console.WriteLine($"[SEND] To: {message.To}, Subject: {message.Subject}");

        var communication = new
        {
            resourceType = "Communication",
            status = "completed",
            recipient = new[] { new { identifier = new { value = outgoingMessage.To } } },
            subject = new { text = outgoingMessage.Subject },
            payload = new[] { new { contentString = outgoingMessage.Body } },
            sent = DateTimeOffset.UtcNow
        };

        try
        {
            var response = await HttpClient.PostAsJsonAsync("/api/communication", communication);
            statusMessage = response.IsSuccessStatusCode
                ? "Message sent successfully."
                : $"Failed to send message ({(int)response.StatusCode}).";
        }
        catch (Exception ex)
        {
            statusMessage = $"Error sending message: {ex.Message}";
        }
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

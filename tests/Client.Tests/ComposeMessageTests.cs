using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Client.Components.Features.Inbox;
using Moq;
using Moq.Protected;

namespace Client.Tests;

public class ComposeMessageTests
{
    private class TestComposeMessage : ComposeMessage
    {
        public void SetClient(HttpClient client) => HttpClient = client;
        public string? Status => statusMessage;
        public Task InvokeSendMessage(OutgoingMessage msg) => SendMessage(msg);
    }

    [Fact]
    public async Task SendMessage_PostsCommunication()
    {
        var msg = new ComposeMessage.OutgoingMessage
        {
            To = "org-1",
            Subject = "hi",
            Body = "body"
        };

        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Post && r.RequestUri!.PathAndQuery == "/api/communication"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

        var client = new HttpClient(handler.Object) { BaseAddress = new Uri("http://localhost") };
        var component = new TestComposeMessage();
        component.SetClient(client);

        await component.InvokeSendMessage(msg);

        Assert.Equal("Message sent successfully.", component.Status);
        handler.Protected().Verify("SendAsync", Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
            ItExpr.IsAny<CancellationToken>());
    }
}

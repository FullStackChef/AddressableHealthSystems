using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Client.Components.Features.Admin;
using Moq;
using Moq.Protected;

namespace Client.Tests;

public class DiscoveryTests
{
    private class TestDiscovery : Discovery
    {
        public void SetClient(HttpClient client) => HttpClient = client;
        public DiscoveryRequest Request => discoveryRequest;
        public void SetResult(DiscoveryResult result) => discoveryResult = result;
        public Task InvokeSync() => SyncToDirectory();
    }

    [Fact]
    public async Task SyncToDirectory_PostsPeerInfo()
    {
        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Post && r.RequestUri!.ToString() == "http://localhost:5131/peers"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

        var client = new HttpClient(handler.Object);
        var comp = new TestDiscovery();
        comp.SetClient(client);
        comp.Request.EndpointUrl = "https://remote";
        comp.SetResult(new Discovery.DiscoveryResult
        {
            RemoteName = "peer1",
            IsTrusted = true,
            Role = "hub",
            SoftwareName = "AHS",
            Version = "1.0",
            IsAhsCompatible = true
        });

        await comp.InvokeSync();

        handler.Protected().Verify("SendAsync", Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri!.ToString() == "http://localhost:5131/peers"),
            ItExpr.IsAny<CancellationToken>());
    }
}

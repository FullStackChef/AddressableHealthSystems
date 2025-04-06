using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using DiscoveryService.Models;
using DiscoveryService.Services;
using Moq;
using Moq.Protected;
using Xunit;

namespace DiscoveryService.Tests.Services;

public class DiscoveryProcessorTests
{
    [Fact]
    public async Task DiscoverAsync_Returns_Valid_Result_For_Ahs_Compatible()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(new RemoteCapabilityStatement
                {
                    SoftwareName = "AddressableHealthSystems",
                    SoftwareVersion = "1.0.0"
                })
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var processor = new DiscoveryProcessor(httpClient);

        var request = new DiscoveryRequest("https://remote.ahs.org");

        // Act
        var result = await processor.DiscoverAsync(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsAhsCompatible);
        Assert.Equal("AddressableHealthSystems", result.SoftwareName);
        Assert.Equal("1.0.0", result.Version);
        Assert.Equal("https://remote.ahs.org", result.RemoteName);
    }

    [Fact]
    public async Task DiscoverAsync_Throws_When_Response_Is_Null()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("null", System.Text.Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var processor = new DiscoveryProcessor(httpClient);
        var request = new DiscoveryRequest("https://invalid.ahs.org");

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            processor.DiscoverAsync(request, CancellationToken.None));
    }
}

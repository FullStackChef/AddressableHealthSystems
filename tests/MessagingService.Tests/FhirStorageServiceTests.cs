using System.Net;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using MessagingService.Services;
using Microsoft.Extensions.Logging;
using Task = System.Threading.Tasks.Task;
using Moq;

public class FhirStorageServiceTests
{
    private readonly Mock<IFhirClientAdapter> _fhirClient = new(MockBehavior.Strict);
    private readonly FhirStorageService _service;

    public FhirStorageServiceTests()
    {
        var logger = Mock.Of<ILogger<FhirStorageService>>();
        _service = new FhirStorageService(_fhirClient.Object, logger);
    }

    [Fact]
    public async Task StoreResourceAsync_ReturnsSuccess_WhenCreateSucceeds()
    {
        var communication = new Communication { Id = "test-123", Status = EventStatus.Completed };

        _fhirClient.Setup(c => c.CreateAsync(
                communication,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(communication);

        var (success, resourceId) = await _service.StoreResourceAsync(communication);

        Assert.True(success);
        Assert.Equal("test-123", resourceId);
    }

    [Fact]
    public async Task StoreResourceAsync_ReturnsFalse_WhenCreateReturnsNull()
    {
        var communication = new Communication();

        _fhirClient.Setup(c => c.CreateAsync(
                communication,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Communication?)null);

        var (success, resourceId) = await _service.StoreResourceAsync(communication);

        Assert.False(success);
        Assert.Null(resourceId);
    }

    [Fact]
    public async Task StoreResourceAsync_ReturnsFalse_WhenCreateThrows()
    {
        var communication = new Communication();

        _fhirClient.Setup(c => c.CreateAsync(
                communication,
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new FhirOperationException("Create failed", HttpStatusCode.InternalServerError));

        var (success, resourceId) = await _service.StoreResourceAsync(communication);

        Assert.False(success);
        Assert.Null(resourceId);
    }

    [Fact]
    public async Task GetCommunicationByIdAsync_ReturnsResource_WhenFound()
    {
        var expected = new Communication { Id = "comm-999" };

        _fhirClient.Setup(c => c.ReadCommunicationAsync(
                "Communication/comm-999",
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _service.GetCommunicationByIdAsync("comm-999");

        Assert.NotNull(result);
        Assert.Equal("comm-999", result.Id);
    }

    [Fact]
    public async Task GetCommunicationByIdAsync_ReturnsNull_WhenNotFound()
    {
        _fhirClient.Setup(c => c.ReadCommunicationAsync(
                "Communication/missing",
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new FhirOperationException("Not found", HttpStatusCode.NotFound));

        var result = await _service.GetCommunicationByIdAsync("missing");

        Assert.Null(result);
    }
}

using System.Net;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Client.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Client.Tests;

public class FhirOrganizationServiceTests
{
    private readonly Mock<IFhirOrganizationClient> _client = new(MockBehavior.Strict);
    private readonly FhirOrganizationService _service;

    public FhirOrganizationServiceTests()
    {
        var logger = Mock.Of<ILogger<FhirOrganizationService>>();
        _service = new FhirOrganizationService(_client.Object, logger);
    }

    [Fact]
    public async Task GetOrganizationAsync_ReturnsResource_WhenFound()
    {
        var org = new Organization { Id = "org-1" };
        _client.Setup(c => c.ReadAsync("org-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(org);

        var result = await _service.GetOrganizationAsync("org-1");

        Assert.NotNull(result);
        Assert.Equal("org-1", result.Id);
    }

    [Fact]
    public async Task GetOrganizationAsync_ReturnsNull_WhenNotFound()
    {
        _client.Setup(c => c.ReadAsync("missing", It.IsAny<CancellationToken>()))
            .ThrowsAsync(new FhirOperationException("nf", HttpStatusCode.NotFound));

        var result = await _service.GetOrganizationAsync("missing");

        Assert.Null(result);
    }

    [Fact]
    public async Task SaveOrganizationAsync_ReturnsTrue_WhenUpdateSucceeds()
    {
        var org = new Organization { Id = "org-1" };
        _client.Setup(c => c.UpdateAsync(org, It.IsAny<CancellationToken>()))
            .ReturnsAsync(org);

        var success = await _service.SaveOrganizationAsync(org);

        Assert.True(success);
    }

    [Fact]
    public async Task SaveOrganizationAsync_ReturnsFalse_WhenUpdateThrows()
    {
        var org = new Organization { Id = "org-1" };
        _client.Setup(c => c.UpdateAsync(org, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new FhirOperationException("fail", HttpStatusCode.InternalServerError));

        var success = await _service.SaveOrganizationAsync(org);

        Assert.False(success);
    }
}

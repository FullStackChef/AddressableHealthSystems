using System.Security.Claims;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Shared;
using Claim = System.Security.Claims.Claim;
using Task = System.Threading.Tasks.Task;

namespace MessagingService.Tests;

public class CommunicationHandlerTests
{
    private readonly TestHandlerFactory factory = new();


    private static HttpContext CreateHttpContext(string? username = "test-user")
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim(ClaimTypes.Name, username ?? "anonymous")],
            "mock"));

        return new DefaultHttpContext { User = user };
    }

    [Fact]
    public async Task HandleIncomingCommunicationAsync_ReturnsOk_WhenAuthorizedAndStored()
    {
        // Arrange
        var handler = factory.Create();
        var context = CreateHttpContext();
        var communication = new Communication { Id = "comm-1", Status = EventStatus.Completed };

        factory.AuthorizationServiceMock.Setup(x => x.AuthorizeAsync(
                It.IsAny<ClaimsPrincipal>(),
                It.IsAny<object?>(),
                "CanPostCommunication"))
            .ReturnsAsync(AuthorizationResult.Success());

        factory.FhirStorageMock.Setup(x => x.StoreResourceAsync(communication, It.IsAny<CancellationToken>()))
            .ReturnsAsync((true, "comm-1"));

        // NEW: Mock Dapr client and inject into handler
        factory.DaprClientMock.Setup(d => d.PublishEventAsync(
                "pubsub",
                "messaging-delivery",
                It.IsAny<CommunicationDeliveryEvent>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);


        // Act
        var result = await handler.HandleIncomingCommunicationAsync(context, communication, DeliveryMode.StoreAndForward, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<Ok<string>>(result.Result);
        Assert.Contains("comm-1", okResult.Value);

        // Verify audit entries (1 = create, 2 = pubsub)
        factory.AuditServiceMock.Verify(x => x.RecordAuditAsync("test-user", "CreateCommunication", "comm-1", It.IsAny<string>()), Times.Once);
        factory.AuditServiceMock.Verify(x => x.RecordAuditAsync("test-user", "TriggerDeliveryWorkflow", "comm-1", It.IsAny<string>()), Times.Once);

        // Verify pubsub publish
        factory.DaprClientMock.Verify(d => d.PublishEventAsync(
            "pubsub",
            "messaging-delivery",
            It.Is<CommunicationDeliveryEvent>(e => e.Id.Equals("comm-1")),
            It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact]
    public async Task HandleIncomingCommunicationAsync_ReturnsForbid_WhenUnauthorized()
    {
        // Arrange
        var handler = factory.Create();
        var context = CreateHttpContext();

        factory.AuthorizationServiceMock.Setup(x => x.AuthorizeAsync(context.User, It.IsAny<object>(), "CanPostCommunication"))
                 .ReturnsAsync(AuthorizationResult.Failed());

        // Act
        var result = await handler.HandleIncomingCommunicationAsync(context, new Communication(), DeliveryMode.StoreAndForward, CancellationToken.None);

        // Assert
        Assert.IsType<ForbidHttpResult>(result.Result);
    }

    [Fact]
    public async Task HandleIncomingCommunicationAsync_ReturnsProblem_WhenStorageFails()
    {
        // Arrange
        var handler = factory.Create();
        var context = CreateHttpContext();
        var communication = new Communication();

        factory.AuthorizationServiceMock.Setup(x => x.AuthorizeAsync(context.User, It.IsAny<object>(), "CanPostCommunication"))
                 .ReturnsAsync(AuthorizationResult.Success());

        factory.FhirStorageMock.Setup(x => x.StoreResourceAsync(communication, It.IsAny<CancellationToken>()))
                        .ReturnsAsync((false, null));

        // Act
        var result = await handler.HandleIncomingCommunicationAsync(context, communication, DeliveryMode.StoreAndForward, CancellationToken.None);

        // Assert
        Assert.IsType<ProblemHttpResult>(result.Result);
    }

    [Fact]
    public async Task GetCommunicationByIdAsync_ReturnsOk_WhenResourceFound()
    {
        // Arrange
        var handler = factory.Create();

        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.Name, "test-user") }, "mock"))
        };

        var comm = new Communication { Id = "comm-42" };

        factory.AuthorizationServiceMock
            .Setup(x => x.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<object?>(), "CanReadCommunication"))
            .ReturnsAsync(AuthorizationResult.Success());

        factory.FhirStorageMock
            .Setup(x => x.GetCommunicationByIdAsync("comm-42", It.IsAny<CancellationToken>()))
            .ReturnsAsync(comm);

        // Act
        var result = await handler.GetCommunicationByIdAsync(context, "comm-42", CancellationToken.None);

        // Assert
        var ok = Assert.IsType<Ok<Communication>>(result.Result);
        Assert.Equal("comm-42", ok.Value?.Id);

        factory.AuditServiceMock.Verify(a =>
            a.RecordAuditAsync("test-user", "ReadCommunication", "comm-42", It.IsAny<string>()),
            Times.Once);
    }


    [Fact]
    public async Task GetCommunicationByIdAsync_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        var factory = new TestHandlerFactory();
        var handler = factory.Create();

        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.Name, "test-user") }, "mock"))
        };

        factory.AuthorizationServiceMock
            .Setup(x => x.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<object?>(), "CanReadCommunication"))
            .ReturnsAsync(AuthorizationResult.Success());

        factory.FhirStorageMock
            .Setup(x => x.GetCommunicationByIdAsync("not-found", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Communication?)null);

        // Act
        var result = await handler.GetCommunicationByIdAsync(context, "not-found", CancellationToken.None);

        // Assert
        Assert.IsType<NotFound>(result.Result);

        factory.AuditServiceMock.Verify(a =>
            a.RecordAuditAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
            Times.Never); // No audit if not found
    }

    [Fact]
    public async Task HandleIncomingCommunicationAsync_DirectDelivery_SkipsStorage()
    {
        var handlerFactory = new TestHandlerFactory();
        handlerFactory.Options = Options.Create(new MessagingOptions { DeliveryMode = MessagingDeliveryMode.Direct });
        var handler = handlerFactory.Create();
        var context = CreateHttpContext();
        var comm = new Communication
        {
            Id = "comm-9",
            Recipient = [ new ResourceReference { Identifier = new Identifier { Value = "peer-1" } } ]
        };

        handlerFactory.AuthorizationServiceMock.Setup(x => x.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<object?>(), "CanPostCommunication"))
            .ReturnsAsync(AuthorizationResult.Success());

        handlerFactory.PeerRegistryMock.Setup(r => r.GetPeerAsync("peer-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PeerInfo("peer-1", "http://base", "endpoint", true));

        handlerFactory.PeerMessengerMock.Setup(m => m.SendCommunicationAsync(It.IsAny<PeerInfo>(), comm, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await handler.HandleIncomingCommunicationAsync(context, comm, CancellationToken.None);

        Assert.IsType<Ok<string>>(result.Result);
        handlerFactory.FhirStorageMock.Verify(s => s.StoreResourceAsync(It.IsAny<Communication>(), It.IsAny<CancellationToken>()), Times.Never);
}
  public async Task HandleIncomingCommunicationAsync_SendsDirect_WhenModeDirect()
    {
        var handler = factory.Create();
        var context = CreateHttpContext();
        var communication = new Communication
        {
            Id = "comm-direct",
            Recipient = [ new ResourceReference("peer-1") ],
            Status = EventStatus.Completed
        };

        factory.AuthorizationServiceMock.Setup(x => x.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<object?>(), "CanPostCommunication"))
            .ReturnsAsync(AuthorizationResult.Success());

        var peer = new PeerInfo("peer-1", "http://p", "http://p/msg", true);
        factory.PeerRegistryMock.Setup(x => x.GetPeerAsync("peer-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(peer);
        factory.PeerMessengerMock.Setup(x => x.SendCommunicationAsync(peer, communication, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await handler.HandleIncomingCommunicationAsync(context, communication, DeliveryMode.Direct, CancellationToken.None);

        Assert.IsType<Ok<string>>(result.Result);
        factory.PeerMessengerMock.Verify(x => x.SendCommunicationAsync(peer, communication, It.IsAny<CancellationToken>()), Times.Once);
        factory.FhirStorageMock.Verify(x => x.StoreResourceAsync(It.IsAny<Communication>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task HandleIncomingCommunicationAsync_FallsBack_WhenDirectFails()
    {
        var handler = factory.Create();
        var context = CreateHttpContext();
        var communication = new Communication
        {
            Id = "comm-fallback",
            Recipient = [ new ResourceReference("peer-1") ],
            Status = EventStatus.Completed
        };

        factory.AuthorizationServiceMock.Setup(x => x.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<object?>(), "CanPostCommunication"))
            .ReturnsAsync(AuthorizationResult.Success());

        var peer = new PeerInfo("peer-1", "http://p", "http://p/msg", true);
        factory.PeerRegistryMock.Setup(x => x.GetPeerAsync("peer-1", It.IsAny<CancellationToken>()))
            .ReturnsAsync(peer);
        factory.PeerMessengerMock.Setup(x => x.SendCommunicationAsync(peer, communication, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        factory.FhirStorageMock.Setup(x => x.StoreResourceAsync(communication, It.IsAny<CancellationToken>()))
            .ReturnsAsync((true, "fhir-1"));
        factory.DaprClientMock.Setup(d => d.PublishEventAsync(
                "pubsub",
                "messaging-delivery",
                It.IsAny<CommunicationDeliveryEvent>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await handler.HandleIncomingCommunicationAsync(context, communication, DeliveryMode.Direct, CancellationToken.None);

        Assert.IsType<Ok<string>>(result.Result);
        factory.PeerMessengerMock.Verify(x => x.SendCommunicationAsync(peer, communication, It.IsAny<CancellationToken>()), Times.Once);
        factory.FhirStorageMock.Verify(x => x.StoreResourceAsync(communication, It.IsAny<CancellationToken>()), Times.Once);
        factory.DaprClientMock.Verify(d => d.PublishEventAsync(
                "pubsub",
                "messaging-delivery",
                It.IsAny<CommunicationDeliveryEvent>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

}
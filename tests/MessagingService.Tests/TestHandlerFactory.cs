using Dapr.Client;
using MessagingService.Handlers;
using MessagingService.Services;
using DirectoryService.Services;
using PeerMessagingService.Services;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PeerMessagingService.Services;
using DirectoryService.Services;
using Moq;

namespace MessagingService.Tests;

public class TestHandlerFactory
{
    public Mock<IFhirStorageService> FhirStorageMock { get; } = new();
    public Mock<IAuditService> AuditServiceMock { get; } = new();
    public Mock<IAuthorizationService> AuthorizationServiceMock { get; } = new();
    public Mock<DaprClient> DaprClientMock { get; } = new();
    public Mock<PeerMessenger> PeerMessengerMock { get; } = new(new HttpClient());
    public Mock<IPeerRegistryService> PeerRegistryMock { get; } = new();
    public IOptions<MessagingOptions> Options { get; set; } = Options.Create(new MessagingOptions());
    public ILogger<CommunicationHandler> Logger { get; } = Mock.Of<ILogger<CommunicationHandler>>();

    public CommunicationHandler Create()
    {
        return new CommunicationHandler(
            FhirStorageMock.Object,
            Logger,
            AuditServiceMock.Object,
            AuthorizationServiceMock.Object,
            DaprClientMock.Object,
            Options,
            PeerMessengerMock.Object,
            PeerRegistryMock.Object
        );
    }
}
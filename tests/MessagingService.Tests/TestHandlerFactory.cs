using Dapr.Client;
using MessagingService.Handlers;
using MessagingService.Services;
using DirectoryService.Services;
using PeerMessagingService.Services;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;

namespace MessagingService.Tests;

public class TestHandlerFactory
{
    public Mock<IFhirStorageService> FhirStorageMock { get; } = new();
    public Mock<IAuditService> AuditServiceMock { get; } = new();
    public Mock<IAuthorizationService> AuthorizationServiceMock { get; } = new();
    public Mock<DaprClient> DaprClientMock { get; } = new();
    public Mock<IPeerRegistryService> PeerRegistryMock { get; } = new();
    public Mock<PeerMessenger> PeerMessengerMock { get; } = new(new HttpClient());
    public ILogger<CommunicationHandler> Logger { get; } = Mock.Of<ILogger<CommunicationHandler>>();

    public CommunicationHandler Create()
    {
        return new CommunicationHandler(
            FhirStorageMock.Object,
            Logger,
            AuditServiceMock.Object,
            AuthorizationServiceMock.Object,
            DaprClientMock.Object,
            PeerRegistryMock.Object,
            PeerMessengerMock.Object
        );
    }
}
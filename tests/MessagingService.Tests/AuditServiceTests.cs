using MessagingService.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;

namespace MessagingService.Tests;

public class AuditServiceTests
{
    [Fact]
    public async Task RecordAuditAsync_LogsExpectedMessage()
    {
        // Arrange
        var logger = new FakeLogger<AuditService>();
        var service = new AuditService(logger);

        // Act
        await service.RecordAuditAsync(
            user: "alice",
            action: "SendMessage",
            resourceId: "comm-123",
            details: "Sent message to Bob."
        );

        // Assert
        var entry = logger.LatestRecord;
        Assert.Contains("alice", entry.Message);
        Assert.Contains("SendMessage", entry.Message);
        Assert.Contains("comm-123", entry.Message);
        Assert.Contains("Sent message", entry.Message);
        Assert.Equal(LogLevel.Information, entry.Level);
    }

    [Fact]
    public async Task RecordAuditAsync_LogsDefaults_WhenOptionalFieldsMissing()
    {
        var logger = new FakeLogger<AuditService>();
        var service = new AuditService(logger);

        await service.RecordAuditAsync("system", "Startup");

        var entry = logger.LatestRecord;
        Assert.Contains("system", entry.Message);
        Assert.Contains("Startup", entry.Message);
        Assert.Contains("(none)", entry.Message); // for null resourceId/details
    }
}
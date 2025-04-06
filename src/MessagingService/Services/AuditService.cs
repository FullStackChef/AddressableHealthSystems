using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MessagingService.Services
{
    public interface IAuditService
    {
        ValueTask RecordAuditAsync(string user, string action, string? resourceId = null, string? details = null);
    }

    public partial class AuditService(ILogger<AuditService> logger) : IAuditService
    {
        public ValueTask RecordAuditAsync(string user, string action, string? resourceId = null, string? details = null)
        {
            Log.Audit(logger, user, action, resourceId ?? "(none)", details ?? "(none)");
            return ValueTask.CompletedTask;
        }

        private static partial class Log
        {
            [LoggerMessage(EventId = 100, Level = LogLevel.Information,
                Message = "AUDIT: {User} performed {Action} on {ResourceId}. Details: {Details}")]
            public static partial void Audit(ILogger logger, string user, string action, string resourceId, string details);
        }
    }
}

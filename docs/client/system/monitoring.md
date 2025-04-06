# System Monitoring (`/system/monitoring`)

The Monitoring page provides a high-level overview of the AHS instance's operational status. It is intended for use by system administrators to quickly assess performance, delivery health, tenant activity, and infrastructure signals.

## 📍 Location

`/system/monitoring`

## 👥 Who Can Access

- System administrators  
- Role: `SystemAdmin`

## ✨ Features

- **Platform-wide metrics** displayed in a card-style summary:
  - Total messages sent or received
  - Active tenant count
  - Failed deliveries in last 24 hours
  - Federation link health
  - Audit queue status
- Intended as a future launch point for dashboards, alerts, and live telemetry

## 🧱 UI Components

- `RadzenCard` for grouped metrics
- Static values rendered for now; expandable to:
  - `RadzenChart`
  - `RadzenGauge`
  - Realtime SignalR integration

## 🔌 Data Model (UI-bound)

```csharp
class MonitoringStats {
    int TotalMessages;
    int ActiveTenants;
    int FailedDeliveries;
    bool FederationHealthy;
    string AuditQueueStatus;
}
```

## 🔄 Data Source

- Static/mock for MVP
- In production:
  - Pulled from backend `MonitoringService`
  - Sourced from message logs, delivery queue, audit pipeline, tenant registry

## 🔐 Notes

- Page is read-only and non-interactive for now
- Designed for operator visibility, not message-level debugging
- Should be secured against unauthorized access (system-level scope only)

## 🔜 Future Enhancements

- Live updates via SignalR
- Detailed error breakdowns (e.g., by endpoint or tenant)
- Delivery queue depth, agent heartbeat status
- Visual indicators (charts, gauges, sparklines)

## 🧪 Example Code References

- `Features/System/Monitoring.razor`
- `Monitoring.razor.cs`
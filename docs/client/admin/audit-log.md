# Audit Log Viewer (`/admin/audit`)

The Audit Log provides a chronological view of administrative and system-level actions, including discovery events, sync operations, and endpoint or tenant changes.

## 📍 Location

`/admin/audit`

## 👥 Who Can Access

- System and organization administrators  
- Role: `OrgAdmin`, `SystemAdmin`

## ✨ Features

- View recent audit events with:
  - Timestamp
  - Event type (e.g. `DiscoveryRequest`, `SyncComplete`)
  - Actor (e.g. `admin@greenfield.org`)
  - Target (endpoint, tenant, etc.)
  - Status (`Success`, `Error`)
  - Correlation ID (for grouped workflows)
- Filter and sort by any column
- Paginated view for larger logs

## 🧱 UI Components

- `RadzenDataGrid` with columns:
  - `Timestamp` (UTC)
  - `EventType`
  - `Actor`
  - `Target`
  - `Status`
  - `CorrelationId`

## 🔌 Data Model

```csharp
class AuditEventModel {
    DateTimeOffset Timestamp;
    string EventType;
    string Actor;
    string Target;
    string Status;
    string CorrelationId;
}
```

## 🔄 Data Source

- Simulated in-memory for MVP
- In production: pulled from:
  - AuditService
  - FHIR `Provenance` resources (optional)
  - Azure Event Grid or system logs

## 🔐 Notes

- Intended to provide traceability for:
  - Security-relevant events
  - Manual changes
  - Discovery/sync activities
- Designed to support compliance requirements (HIPAA, etc.)

## 🔜 Future Enhancements

- Export as CSV or JSON
- Filter by time window or event category
- Drill-down to see full request/response or payload metadata
- Timeline or grouped event view by correlation ID

## 🧪 Example Code References

- `Features/Admin/Audit.razor`
- `Audit.razor.cs`
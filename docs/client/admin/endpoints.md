# Delivery Endpoints (`/admin/endpoints`)

The Endpoints page allows administrators to view and manage delivery endpoints configured for outbound messaging within AHS.

## 📍 Location

`/admin/endpoints`

## 👥 Who Can Access

- Organization or system administrators
- Role: `OrgAdmin`, `SystemAdmin`

## ✨ Features

- **View all known delivery endpoints** from the local FHIR `Endpoint` resources
- **Filter by name, status, or type**
- **Trigger validation** (e.g., test connectivity, check mTLS certificate)
- **Prepare for message routing** (delivery URLs, connection types)

## 🧱 UI Components

- `RadzenDataGrid` showing:
  - Name
  - Endpoint URL
  - Status (e.g. active, suspended)
  - Connection Type (e.g. `hl7-fhir-rest`, `hl7-v2-mllp`)
- `Validate` button (placeholder)

## 🔌 Data Model

```csharp
class EndpointModel {
    string Id;
    string Name;
    string Address;
    string Status;
    string ConnectionType;
}
```

Currently mocked in code-behind; in production, sourced from:
- Local FHIR store (`/Endpoint`)
- Optionally: synced via Discovery process

## 🔐 Notes

- This page displays endpoints only — **organization** and **practitioner** info lives under the Directory section
- Future enhancements may include:
  - Trust validation results
  - Sync status and discovery source
  - Editing or suspending endpoints directly

## 🧪 Example Code References

- `Features/Admin/Endpoints.razor`
- `Endpoints.razor.cs`


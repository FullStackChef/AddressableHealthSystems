# Tenant Detail (`/system/tenants/{OrgId}`)

The Tenant Detail page provides a focused view of a single tenant (organization) in a multi-tenant AHS deployment. It allows system administrators to view and edit basic configuration for that tenant.

## 📍 Location

`/system/tenants/{OrgId}`

## 👥 Who Can Access

- System administrators  
- Role: `SystemAdmin`

## ✨ Features

- View and update core tenant metadata:
  - Display name
  - Org ID / OID (read-only)
  - Agent URL (if present)
  - Current status (Active / Suspended)
  - Message volume (last 30 days)
- Editable fields:
  - Name
  - Agent URL
  - Status (`RadzenDropDown`)

## 🧱 UI Components

- `RadzenTemplateForm` with:
  - `RadzenTextBox` for name and agent
  - `RadzenDropDown` for status
  - `RadzenNumeric` for message volume (read-only)
- Save button to commit changes (mocked for now)

## 🔌 Data Model

```csharp
class TenantModel {
    string Name;
    string OrgId;
    string Status;
    int MessageVolume;
    string? AgentUrl;
}
```

## 🔄 Data Source

- Tenant record is matched via `OrgId` route parameter
- In production:
  - Backed by `TenantService` or FHIR `Organization`
  - Supports `GET /tenants/{id}` and `PUT /tenants/{id}`

## 🔐 Notes

- Intended for **system-level** tenant config
- Org-scoped settings like certs/profile live under `/orgsettings/*`
- `OrgId` is a persistent identifier and not editable

## 🔜 Future Enhancements

- View audit trail of tenant events
- Add sync status and agent health indicators
- Link to tenant-specific endpoints, inbox, org settings
- Suspend tenant (affects message delivery, discovery)

## 🧪 Example Code References

- `Features/System/TenantDetail.razor`
- `TenantDetail.razor.cs`
- Linked from: `Tenants.razor` via `NavigateToTenant()`
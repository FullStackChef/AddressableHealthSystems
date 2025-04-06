## 📄 `docs/client/tenants.md`

```markdown
# Tenants (`/system/tenants`)

The Tenants page provides a centralized view of all organizations (tenants) hosted in a multi-tenant Addressable Health System (AHS) deployment. Each tenant represents a distinct organization with its own messaging, directory, and agent configuration.

## 📍 Location

`/system/tenants`

## 👥 Who Can Access

- System administrators  
- Role: `SystemAdmin`

## ✨ Features

- List of all registered tenants
- Key info per tenant:
  - Name
  - Org ID (from FHIR `Organization.identifier`)
  - Status (Active / Suspended)
  - Message volume (last 30 days)
  - Agent URL (if connected)
- Actions per tenant:
  - View tenant detail
  - Suspend tenant

## 🧱 UI Components

- `RadzenDataGrid` with:
  - Filtering, paging, sorting
  - Action buttons (`View`, `Suspend`)

## 🔌 Data Model (UI-bound)

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

- Currently simulated in-memory
- In production:
  - Backed by `TenantService` or equivalent
  - Possibly driven by FHIR `Organization` + AHS-specific metadata

## 🔐 Notes

- Tenants may map directly to local AHS hubs or integration spokes
- Suspended tenants may have delivery disabled or discovery blocked
- In future, tenants could be linked to:
  - OrgSettings (certs, profile)
  - Audit log views
  - Agent health

## 🔜 Future Enhancements

- Search by region, org type, agent status
- Create new tenant workflow
- Tenant impersonation / scoped admin
- Health check integration (agent ping, cert validity)

## 🧪 Example Code References

- `Features/System/Tenants.razor`
- `Tenants.razor.cs`
- Linked route: `/system/tenants/{OrgId}` → `TenantDetail.razor`
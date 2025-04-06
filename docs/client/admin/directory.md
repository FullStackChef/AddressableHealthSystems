# Directory Viewer (`/admin/directory`)

The Directory page provides a searchable view into the local AHS directory, listing known organizations and practitioners. It reflects the FHIR-based `Organization` and `Practitioner` resources stored in the system.

## 📍 Location

`/admin/directory`

## 👥 Who Can Access

- Organization or system administrators  
- Role: `OrgAdmin`, `SystemAdmin`

## ✨ Features

- **Tab-based view** for:
  - Organizations
  - Practitioners
- **Searchable, filterable grids**
- Display key FHIR resource fields:
  - Organization: Name, Type, Address
  - Practitioner: Name, Role, Organization

## 🧱 UI Components

- `RadzenTabs` with:
  - `RadzenDataGrid` for each resource type
  - Columns tailored to the respective FHIR resource

## 🔌 Data Models

### OrganizationModel (UI view)

```csharp
class OrganizationModel {
    string Name;
    string Type;
    string Address;
}
```

### PractitionerModel (UI view)

```csharp
class PractitionerModel {
    string FullName;
    string Role;
    string OrgName;
}
```

These models are currently mocked but map to FHIR `Organization` and `Practitioner` resources.

## 🔄 Data Source

- FHIR resources stored locally
- Populated via:
  - Manual sync (Discovery)
  - Future imports / onboarding flows

## 🔐 Notes

- This is a **read-only view** for now
- Endpoint details are managed separately (`/admin/endpoints`)
- Practitioner role handling may eventually use `PractitionerRole` resources

## 🔜 Future Enhancements

- Add filters by status, region, or organization type
- Link organizations to their endpoints
- Allow edit or merge of discovered duplicates
- Add detail panels with full FHIR resource previews

## 🧪 Example Code References

- `Features/Admin/Directory.razor`
- `Directory.razor.cs`
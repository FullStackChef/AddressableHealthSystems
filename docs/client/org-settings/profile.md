# Organization Profile (`/orgsettings/profile`)

The Org Profile page allows an administrator to manage the core metadata about their own organization, which is represented in the system as a FHIR `Organization` resource.

## 📍 Location

`/orgsettings/profile`

## 👥 Who Can Access

- Organization administrators  
- Role: `OrgAdmin`

## ✨ Features

- **Editable fields**:
  - Organization name
  - Organization ID (e.g., OID, UUID)
  - Email and phone contact
  - Address
- Stores data in the local directory (`Organization` resource)
- Prepares metadata used in:
  - Discovery responses
  - Directory sync
  - Certificate trust chains

## 🧱 UI Components

- `RadzenTemplateForm` with:
  - `RadzenTextBox` (Name, Org ID, Email, Phone)
  - `RadzenTextArea` (Address)
  - `RadzenButton` (Save)

## 🔌 Data Model (UI-bound)

```csharp
class OrgProfile {
    string Name;
    string Identifier;
    string Email;
    string Phone;
    string Address;
}
```

## 🔄 Data Source

- Currently loaded in-memory
- In production: loads from and updates the local FHIR store (`GET/PUT Organization/{id}`)

## 🔐 Notes

- Intended for the **hosting organization only** — not for managing external orgs (those live in the directory)
- Identifier field may be constrained to an OID format or system-specific URI
- Ensures that other instances can resolve your identity via Discovery

## 🔜 Future Enhancements

- Add logo or branding
- Add multiple contact points or departments (FHIR `Organization.contact`)
- Validation for known identifier systems (e.g., `urn:oid:...`)
- Read/write with FHIR `Organization` resource + server-side enforcement

## 🧪 Example Code References

- `Features/OrgSettings/Profile.razor`
- `Profile.razor.cs`
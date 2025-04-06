# Discovery Interface (`/admin/discovery`)

The Discovery page allows administrators to initiate and manage AHS-to-AHS discovery. This enables trusted connections between AHS instances and sets the foundation for endpoint synchronization and certificate validation.

## 📍 Location

`/admin/discovery`

## 👥 Who Can Access

- Organization or system administrators  
- Role: `OrgAdmin`, `SystemAdmin`

## ✨ Features

- **Submit remote FHIR base URL** (e.g. `https://remote.ahs.org/fhir`)
- Perform a **FHIR `CapabilityStatement` request**
- Parse response to determine:
  - Remote software name and version
  - AHS compatibility (via declared extensions or supported resources)
  - Federation role (Hub / Spoke / Mutual)
  - Trust state (cert validation, JWKS presence)
- Show result summary:
  - Name, Version, Role, Trust Status
- **Sync to Directory** button (if compatible)
  - Imports `Organization`, `Endpoint`, and other supported resources

## 🧱 UI Components

- `RadzenTemplateForm` to enter remote FHIR endpoint
- `RadzenCard` to display discovery result
- Conditional "Sync to Directory" button

## 🔌 Data Models

### Discovery Request

```csharp
class DiscoveryRequest {
    string EndpointUrl;
}
```

### Discovery Result

```csharp
class DiscoveryResult {
    string RemoteName;
    string SoftwareName;
    string Version;
    string Role;
    bool IsAhsCompatible;
    bool IsTrusted;
    string? CertificateThumbprint;
    List<string> SupportedResources;
}
```

## 🔐 Notes

- Discovery leverages the public `/metadata` endpoint on remote FHIR servers
- Trust validation is local (mTLS cert check, JWK resolution, etc.)
- No authentication is assumed on first discovery pass

## 🔜 Future Enhancements

- Display supported interactions and security extensions
- Allow federation role selection (e.g. confirm “mutual spoke”)
- Support multi-step onboarding workflows

## 🧪 Example Code References

- `Features/Admin/Discovery.razor`
- `Discovery.razor.cs`
- Backend endpoint: `POST /discover` (stub or actual DiscoveryService)


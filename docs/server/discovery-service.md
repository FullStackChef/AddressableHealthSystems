# DiscoveryService

The `DiscoveryService` is responsible for initiating and processing the discovery of remote AHS instances via their FHIR R5 `CapabilityStatement`.

It supports trust establishment, compatibility checks, and prepares for endpoint synchronization between federated hubs or spokes.

---

## 🎯 Purpose

- Initiate capability exchange with a remote FHIR endpoint
- Validate whether the remote system is AHS-compatible
- Extract metadata (software name, version, supported resources)
- Optionally: validate TLS certificates and trust anchors
- Store or return parsed results for UI and directory sync

---

## 📮 Endpoint

```http
POST /discover
Content-Type: application/json
```

### Request Payload

```json
{
  "endpointUrl": "https://remote.ahs.org/fhir"
}
```

### Response Payload

```json
{
  "remoteName": "Partner AHS Node",
  "softwareName": "AddressableHealthHub",
  "version": "1.0.2",
  "role": "Mutual",
  "isAhsCompatible": true,
  "isTrusted": true,
  "certificateThumbprint": "ABCD1234...",
  "supportedResources": ["Organization", "Endpoint", "MessageHeader"]
}
```

---

## ⚙️ Key Logic

- Performs a `GET {endpointUrl}/metadata`
- Parses:
  - `software.name` and `software.version`
  - Supported `resource.type`
  - Custom AHS capability extensions (if present)
- Validates TLS layer:
  - Peer certificate thumbprint
  - (Optional) JWKS discovery or trust anchor

---

## 🧱 Service Interface

```csharp
Task<DiscoveryResult> DiscoverAsync(string endpointUrl);
```

### `DiscoveryResult` DTO

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

---

## 🔐 Trust Validation (Pluggable)

Trust status is derived by one or more of:

- Valid public cert with known thumbprint
- mTLS handshake (when supported)
- Presence in known-trusted federation map
- Future: signed JWKS documents

---

## 🔁 Usage

Used by:

- `Discovery.razor` via `POST /discover`
- Future CLI tools or federation automations
- Federation sync process to bootstrap new remote peers

---

## 🔜 Roadmap

- Cache recent discovery attempts
- Support retry with enhanced diagnostics
- Plug-in model for validating proprietary trust chains
- Extract declared roles or endpoints directly from `CapabilityStatement.rest`

---

## 📂 Related Docs

- [`/admin/discovery`](../client/discovery.md)
- [`FederationService`](./federation-service.md)
- [`DirectoryService`](./directory-service.md)
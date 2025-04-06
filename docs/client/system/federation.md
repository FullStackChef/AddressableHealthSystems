# Federated Hubs (`/system/federation`)

The Federation page allows a system administrator to manage trust relationships between Addressable Health System (AHS) instances. This is where federated communication is configured and monitored.

## 📍 Location

`/system/federation`

## 👥 Who Can Access

- System administrators  
- Role: `SystemAdmin`

## ✨ Features

- View list of federated AHS instances
- Display for each peer:
  - Remote FHIR base URL
  - Federation role (`Hub`, `Spoke`, `Mutual`)
  - Trust status (valid certs / trusted thumbprints)
  - Sync status (e.g. last sync time, out-of-sync)
  - Last discovery time
- Actions per peer:
  - **Discover**: Re-run remote metadata discovery
  - **Sync**: Pull/update remote organization and endpoints

## 🧱 UI Components

- `RadzenDataGrid`
  - Columns: URL, Role, Last Discovery, Trust, Sync Status
- Action buttons per row:
  - `Discover`
  - `Sync`

## 🔌 Data Model (UI-bound)

```csharp
class FederationPeer {
    string Url;
    string Role;
    DateTimeOffset LastDiscovery;
    string TrustStatus;
    string SyncStatus;
}
```

## 🔄 Data Source

- In-memory mock for MVP
- Future implementation:
  - DiscoveryService handles `/metadata` calls
  - FederationSyncService manages relationships
  - TrustService validates certs / JWKS

## 🔐 Notes

- Federation peers must be trusted before sync is enabled
- Supports mutual AHS relationships (bi-directional delivery)
- Can evolve to use FHIR `CapabilityStatement` extensions for AHS compatibility detection

## 🔜 Future Enhancements

- View remote org metadata inline
- Auto-discovery based on broadcast or out-of-band registration
- Trust negotiation and policy engine
- Timeline/history of interactions with each peer

## 🧪 Example Code References

- `Features/System/Federation.razor`
- `Federation.razor.cs`
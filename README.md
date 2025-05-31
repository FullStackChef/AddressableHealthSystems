# AddressableHealthSystems

Addressable Health Systems (AHS) aims to facilitate secure, federated exchange of FHIR resources between trusted healthcare organizations. It provides services for discovering peer nodes, synchronizing directory information and delivering FHIR `Communication` resources.

## Service Architecture

The solution is composed of several .NET microservices hosted by the `AddressableHealthSystems.AppHost` application:

- **Gateway** – API gateway and entry point for clients.
- **Client** – Blazor UI for administration and inbox management.
- **DirectoryService** – stores organization and endpoint data.
- **DiscoveryService** – performs capability discovery of remote AHS nodes.
- **MessagingService** – stores and dispatches FHIR `Communication` messages.
- **PeerMessagingService** – handles peer registry and direct message delivery.

The services communicate over HTTP and use [Dapr](https://dapr.io/) for pub/sub and service invocation. A sample Hyperledger Fabric environment is provided under `blockchain/` for experimentation with ledger-backed workflows.

## Building and Running

1. **Prerequisites**
   - .NET 9 SDK preview.
   - [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr/).
   - Docker (required for Dapr components and optional Fabric dev environment).
   - Optional: Hyperledger Fabric binaries if exploring the `blockchain` samples.

2. **Restore and build the solution**

```bash
 dotnet restore AddressableHealthSystems.sln
 dotnet build AddressableHealthSystems.sln
```

3. **Run the distributed application**

```bash
 dotnet run --project src/AddressableHealthSystems.AppHost
```

This launches all services with the default configuration. Individual services can also be run separately by executing `dotnet run` inside each project directory.

## Running Tests

Unit tests are located under the `tests/` folder. Execute the following command from the repository root:

```bash
 dotnet test AddressableHealthSystems.sln
```

Dapr sidecars do not need to be running for the unit tests.

## Documentation

Additional documentation can be found in the [docs](docs/) folder, including:

- [Discovery Service](docs/server/discovery-service.md)
- [Peer Messaging Services](docs/server/peer-messaging.md)
- [Federation Administration UI](docs/client/system/federation.md)
- [Discovery Administration UI](docs/client/admin/discovery.md)

---

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.

# Peer Messaging Services

The `PeerRegistryService` and `PeerMessagingService` provide the minimal pieces needed to exchange FHIR `Communication` resources between federated AHS nodes. The registry keeps track of peers and their messaging endpoints, while the messaging service posts communications to those endpoints when direct delivery is enabled.

---

## 🎯 Purpose

- `PeerRegistryService` stores information about remote peers (`PeerInfo`) such as base URL, messaging endpoint, and trust status.
- `PeerMessagingService` (via `PeerMessenger`) sends a FHIR `Communication` to a peer using HTTP `POST`.

---

## ⚙️ Configuration

`MessagingService` reads the `Messaging:DeliveryMode` option.

- **Direct** &mdash; `CommunicationHandler` calls `PeerMessenger` to deliver the message immediately.
- **StoreAndForward** &mdash; messages are persisted locally and queued for later dispatch.

---

## 🔁 Fallback Behavior

If a direct delivery attempt fails or the peer is unknown, the handler records an audit entry and falls back to store-and-forward processing so the message is still stored in the local FHIR server.

---

## 📮 Example API Calls

Register or update a peer:

```http
POST /peers
Content-Type: application/json

{
  "id": "peer1",
  "baseUrl": "https://remote.ahs.org",
  "messagingEndpoint": "https://remote.ahs.org/api/communication",
  "isTrusted": true
}
```

Send a communication:

```http
POST /api/communication
Content-Type: application/fhir+json

{ ...FHIR Communication JSON... }
```

---

## 📂 Related Docs

- [`Federated Hubs`](../client/system/federation.md)
- [`Compose Message`](../client/inbox/compose-message.md)


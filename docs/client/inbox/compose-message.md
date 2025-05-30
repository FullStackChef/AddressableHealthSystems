# Compose Message (`/inbox/compose`)

The Compose Message page allows authenticated users to send secure messages to other organizations or endpoints. This supports basic messaging workflows within federated AHS networks.

## 📍 Location

`/inbox/compose`

## 👥 Who Can Access

- All authenticated users  
- Role: `Practitioner`, `OrgAdmin`

## ✨ Features

- Recipient picker from known organizations
- Subject + Body form
- Sends via message dispatch pipeline (TBD: HL7, FHIR `Communication`, etc.)

## 🧱 UI Components

- `RadzenTemplateForm`
- `RadzenDropDown` for recipient
- `RadzenTextBox` for subject
- `RadzenTextArea` for body
- `RadzenButton` to submit

## 🔌 Data Model

```csharp
class OutgoingMessage {
    string To;
    string Subject;
    string Body;
}
```

### Recipient Picker

```csharp
class RecipientOption {
    string Id;
    string Display;
}
```

## 🔄 Data Source

- Recipients loaded from local `DirectoryService` or hardcoded list
- In production:
  - FHIR `Organization` directory
  - Local message router or HL7 integration spoke

## 🔐 Notes

- No attachments or structured payload yet
- Messages are plaintext for MVP
- Backend dispatch may route via FHIR `Communication`, HL7 v2, or internal message broker

## 🔜 Future Enhancements

- Support attachments (PDF, CDA, etc.)
- Smart auto-complete from FHIR directory
- Message templates
- Role-based routing (e.g., “send to radiologist”)

## 🧪 Example Code References

- `Features/Inbox/ComposeMessage.razor`
- `ComposeMessage.razor.cs`
## 📂 Related Docs

- [`Peer Messaging Services`](../../server/peer-messaging.md)


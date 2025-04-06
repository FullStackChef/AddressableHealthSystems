# Inbox (`/inbox`)

The Inbox page provides a user-friendly interface for viewing and managing messages exchanged through the Addressable Health System. This is where clinicians or administrative users can read, compose, and follow up on secure communications.

## 📍 Location

`/inbox`

## 👥 Who Can Access

- All authenticated users (clinicians, admins)  
- Role: `Practitioner`, `OrgAdmin`, `Support`

## ✨ Features

- **Message List View**
  - Display sender, subject, and timestamp
  - Filter and search support (planned)
- **Message Detail Panel**
  - Full content view
  - Headers, sender info
- **Compose Message**
  - Select recipient (from Directory)
  - Compose subject + body
  - Send via FHIR `Communication` or `MessageHeader` resource (future)

## 🧱 UI Components

- `RadzenDataGrid` for message listing
- `RadzenCard` or panel for viewing selected message
- `RadzenButton` for “Compose” (navigates to `/inbox/compose`)
- Placeholder or future integration for attachments

## 🔌 Data Model (UI-bound)

```csharp
class Message {
    string Id;
    string Sender;
    string Subject;
    string Body;
    DateTimeOffset SentAt;
}
```

For MVP, messages are mocked. In production:
- Sourced from `Communication`, `MessageHeader`, or message queue
- Filtered by authenticated user/org

## 🔄 Routes

| Path                 | Purpose         |
|----------------------|------------------|
| `/inbox`             | View inbox and selected message |
| `/inbox/compose`     | Compose a new message |
| `/inbox/{messageId}` | View message details (optional enhancement)

## 🔐 Notes

- UI is intentionally lightweight to avoid duplicating functionality already present in connected PMS or EMR systems
- Emphasis is on **viewing inbound messages**, not full-case management

## 🔜 Future Enhancements

- Role-based inbox filtering
- Message threading
- Send as `CommunicationRequest` or `Communication` FHIR resource
- Smart recipient auto-complete via Directory

## 🧪 Example Code References

- `Features/Inbox/Inbox.razor`
- `Features/Inbox/MessageDetail.razor`
- `Features/Inbox/ComposeMessage.razor`
# Message Detail (`/inbox/{id}`)

The Message Detail page displays the full contents of a selected message. This route is accessed directly when a user selects a message from the inbox or links to a message notification.

## 📍 Location

`/inbox/{id}`

## 👥 Who Can Access

- All authenticated users  
- Role: `Practitioner`, `OrgAdmin`

## ✨ Features

- View a single message in detail
- Includes:
  - Sender name or organization
  - Subject
  - Message body (plain or rich text)
  - Sent timestamp
- Context-aware navigation (e.g., return to `/inbox`)

## 🧱 UI Components

- `RadzenCard` or panel layout
- `RadzenLabel`, `RadzenTextBox` (read-only), or HTML rendering of message body
- Back button to inbox (`/inbox`)

## 🔌 Data Model

```csharp
class Message {
    string Id;
    string Sender;
    string Subject;
    string Body;
    DateTimeOffset SentAt;
}
```

### Route Parameter

- `@page "/inbox/{id}"`
- ID is used to fetch or filter from message store

## 🔄 Data Source

- Mocked for now (local list or static data)
- In production:
  - Pulled from `MessageService`
  - Backed by FHIR `Communication` or internal store

## 🔐 Notes

- Messages are read-only
- User must be authorized to view the message
- May be scoped by org ID or sender

## 🔜 Future Enhancements

- Reply or forward actions
- Timeline view for related messages (via `CorrelationId`)
- Render message attachments or referenced resources
- Message read receipt or audit trace

## 🧪 Example Code References

- `Features/Inbox/MessageDetail.razor`
- Uses route parameter: `@page "/inbox/{id}"`
- Bound to a selected or fetched `Message` object
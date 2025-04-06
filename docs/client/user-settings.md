# User Settings Page (`/settings`)

The User Settings page allows authenticated users to manage personal preferences related to their account and system behavior.

## 📍 Location

`/settings`

## 👥 Who Can Access

- All authenticated users
- Role: Any (`Practitioner`, `OrgAdmin`, `SystemAdmin`)

## ✨ Features

- **Display Name**: Editable name shown throughout the UI
- **Email**: Read-only email address (from auth claims)
- **Role**: Read-only user role
- **Notifications**:
  - Enable/disable real-time toast alerts
  - Enable/disable email notifications

## 🧱 UI Components

- `RadzenTemplateForm`
- `RadzenTextBox`, `RadzenCheckBox`, `RadzenButton`
- Form auto-binds to a local `UserProfile` and `UserPreferences` model

## 🔐 Notes

- This page is scoped to the current user session
- Changes are currently stored in-memory (for MVP); can later persist to:
  - Local storage
  - Secure backend service
  - Claims extension (if using IdentityServer or similar)

## 🔜 Future Enhancements

- Password / MFA configuration (if not using external auth)
- Theme / language preferences
- Login history or session manager

## 🧪 Example Code References

- `Pages/Settings.razor`
- `UserProfile`, `UserPreferences` in `Settings.razor.cs`


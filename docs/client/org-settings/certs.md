# Organization Certificates (`/orgsettings/certs`)

The Certificates page allows an organization administrator to upload, view, and manage X.509 certificates used for secure communication, such as mTLS or JWKS-based validation.

## 📍 Location

`/orgsettings/certs`

## 👥 Who Can Access

- Organization administrators  
- Role: `OrgAdmin`

## ✨ Features

- List of trusted and active certificates
  - Common Name (CN)
  - Thumbprint
  - Expiration date
  - Trust type (Inbound / Outbound)
  - Status (Active, Expired, Revoked)
- Upload PEM / CRT / CER files (manually or via drag & drop)
- Revoke expired or invalid certificates

## 🧱 UI Components

- `RadzenDataGrid` for displaying certs
- `RadzenFileInput` for uploads
- `RadzenButton` for "Revoke" and future actions

## 🔌 Data Model (UI-bound)

```csharp
class CertModel {
    string CommonName;
    string Thumbprint;
    DateTimeOffset Expires;
    string TrustType;
    string Status;
}
```

## 🔄 Data Source

- In-memory for MVP
- Future implementation:
  - Stored in a local trust store or FHIR extension
  - Integrated with Discovery and sync logic
  - Supports `GET/POST` to `CertificateService` or equivalent

## 🔐 Notes

- Certificates are critical for:
  - Validating remote AHS instances during Discovery
  - Ensuring secure delivery via mTLS
- Thumbprints may be cross-validated with those declared in remote metadata
- Uploaded certs should be parsed to extract:
  - CN
  - Expiry
  - Thumbprint
  - Key usage (optional)

## 🔜 Future Enhancements

- Full certificate chain validation
- Show parsed cert details (Issuer, SANs, etc.)
- Auto-detect and trust JWKs
- Role-based cert scope (Org-level vs. System-wide)

## 🧪 Example Code References

- `Features/OrgSettings/Certs.razor`
- `Certs.razor.cs`
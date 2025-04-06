# 🔐 Certificates

This page allows your organization to manage its public certificates. These are used for secure communication (like mTLS) and for building trust with other AHS systems.

---

## 👥 Who Should Use This Page

- **Organization administrators**
- IT/security staff managing certificates

---

## ✅ What You Can Do

- View a list of certificates associated with your organization
- See:
  - Common name (CN)
  - Thumbprint
  - Expiry date
  - Trust type (Inbound/Outbound)
  - Status (Active, Expired, Revoked)
- Upload new certificates (PEM, CRT, CER)
- Revoke old or expired certificates

---

## 📝 How to Use It

1. Go to **Org Settings → Certificates**
2. Review the list of active certificates
3. To upload a new certificate:
   - Select a `.pem`, `.cer`, or `.crt` file
   - It will be parsed and added to the list
4. To revoke a certificate, click the **Revoke** button next to it

---

## ⚠️ Things to Know

- Uploaded certificates must be in X.509 format
- Revoking a certificate immediately disables trust for that key
- Certificates are used in Discovery and secure delivery

---

## 🔗 Related Pages

- [🏢 Organization Profile](./profile.md)
- [🌐 Federation](../system/federation.md)

# 🔍 Discovery

The Discovery page allows you to connect with other AHS instances by entering their FHIR server address. Once discovered, their directory and endpoints can be synced into your system.

---

## 👥 Who Should Use This Page

- **Organization or System administrators**
- Anyone onboarding a new connection or partner org

---

## ✅ What You Can Do

- Enter a remote FHIR URL (e.g. from another clinic)
- Validate that the other system is AHS-compatible
- View:
  - Name
  - Role (Hub, Spoke)
  - Trust status
- Sync their endpoints and org metadata into your Directory

---

## 📝 How to Use It

1. Go to **Admin → Discovery**
2. Enter the **remote FHIR base URL**
3. Click **Discover**
4. Review the remote instance's metadata
5. Click **Sync to Directory** if it's a trusted system. This posts the peer details to the local **DirectoryService**.

---

## ⚠️ Things to Know

- Discovery only works if the remote server is public and AHS-compatible
- Make sure certificates are valid before syncing
- You can re-run discovery at any time to update

---

## 🔗 Related Pages

- [🌐 Endpoints](./endpoints.md)
- [📖 Directory](./directory.md)

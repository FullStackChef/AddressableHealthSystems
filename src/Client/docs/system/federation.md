# 🌐 Federation

The Federation page allows you to manage trusted relationships between different AHS hubs and spokes. These relationships define which remote systems you can communicate and exchange messages with.

---

## 👥 Who Should Use This Page

- **System administrators**
- Anyone responsible for onboarding external AHS instances

---

## ✅ What You Can Do

- See all currently connected federated AHS systems
- Check:
  - Role (Hub, Spoke, Mutual)
  - Trust status
  - Discovery & sync status
- Trigger:
  - Remote discovery
  - Sync of directory data

---

## 📝 How to Use It

1. Go to **System → Federation**
2. Review the list of connected AHS peers
3. Click **Discover** to re-check remote status
4. Click **Sync** to pull remote endpoints/orgs

---

## ⚠️ Things to Know

- Trust must be established (e.g., via certs) for sync to work
- Role affects directionality of message exchange
- This page is for **inter-instance trust**, not internal orgs

---

## 🔗 Related Pages

- [🔍 Discovery](./discovery.md)
- [📖 Directory](./directory.md)

import { Routes, Route } from 'react-router-dom'
import { MainLayout } from '@/components/layout/main-layout'
import { HomePage } from '@/components/pages/home-page'
import { InboxPage } from '@/components/features/inbox/inbox-page'
import { ComposeMessagePage } from '@/components/features/inbox/compose-message-page'
import { MessageDetailPage } from '@/components/features/inbox/message-detail-page'
import { EndpointsPage } from '@/components/features/admin/endpoints-page'
import { DiscoveryPage } from '@/components/features/admin/discovery-page'
import { DirectoryPage } from '@/components/features/admin/directory-page'
import { AuditPage } from '@/components/features/admin/audit-page'
import { FederationPage } from '@/components/features/system/federation-page'
import { TenantsPage } from '@/components/features/system/tenants-page'
import { TenantDetailPage } from '@/components/features/system/tenant-detail-page'
import { MonitoringPage } from '@/components/features/system/monitoring-page'
import { OrgProfilePage } from '@/components/features/org-settings/org-profile-page'
import { CertificatesPage } from '@/components/features/org-settings/certificates-page'
import { SettingsPage } from '@/components/features/settings/settings-page'

export function App() {
  return (
    <MainLayout>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/inbox" element={<InboxPage />} />
        <Route path="/inbox/compose" element={<ComposeMessagePage />} />
        <Route path="/messages/:id" element={<MessageDetailPage />} />
        <Route path="/admin/endpoints" element={<EndpointsPage />} />
        <Route path="/admin/discovery" element={<DiscoveryPage />} />
        <Route path="/admin/directory" element={<DirectoryPage />} />
        <Route path="/admin/audit" element={<AuditPage />} />
        <Route path="/system/federation" element={<FederationPage />} />
        <Route path="/system/tenants" element={<TenantsPage />} />
        <Route path="/system/tenants/:id" element={<TenantDetailPage />} />
        <Route path="/system/monitoring" element={<MonitoringPage />} />
        <Route path="/org-settings/profile" element={<OrgProfilePage />} />
        <Route path="/org-settings/certs" element={<CertificatesPage />} />
        <Route path="/settings" element={<SettingsPage />} />
      </Routes>
    </MainLayout>
  )
}
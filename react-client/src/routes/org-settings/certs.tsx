import { createFileRoute } from '@tanstack/react-router'
import { CertificatesPage } from '@/components/features/org-settings/certificates-page'

export const Route = createFileRoute('/org-settings/certs')({
  component: CertificatesPage,
})
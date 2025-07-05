import { createFileRoute } from '@tanstack/react-router'
import { OrgProfilePage } from '@/components/features/org-settings/org-profile-page'

export const Route = createFileRoute('/org-settings/profile')({
  component: OrgProfilePage,
})
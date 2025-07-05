import { createFileRoute } from '@tanstack/react-router'
import { TenantsPage } from '@/components/features/system/tenants-page'

export const Route = createFileRoute('/system/tenants')({
  component: TenantsPage,
})
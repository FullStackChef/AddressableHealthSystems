import { createFileRoute } from '@tanstack/react-router'
import { TenantDetailPage } from '@/components/features/system/tenant-detail-page'

export const Route = createFileRoute('/system/tenants/$id')({
  component: TenantDetailPage,
})
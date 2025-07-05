import { createFileRoute } from '@tanstack/react-router'
import { DiscoveryPage } from '@/components/features/admin/discovery-page'

export const Route = createFileRoute('/admin/discovery')({
  component: DiscoveryPage,
})
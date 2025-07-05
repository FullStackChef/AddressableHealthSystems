import { createFileRoute } from '@tanstack/react-router'
import { EndpointsPage } from '@/components/features/admin/endpoints-page'

export const Route = createFileRoute('/admin/endpoints')({
  component: EndpointsPage,
})
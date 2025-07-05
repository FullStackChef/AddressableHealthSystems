import { createFileRoute } from '@tanstack/react-router'
import { FederationPage } from '@/components/features/system/federation-page'

export const Route = createFileRoute('/system/federation')({
  component: FederationPage,
})
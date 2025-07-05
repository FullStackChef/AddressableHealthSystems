import { createFileRoute } from '@tanstack/react-router'
import { MonitoringPage } from '@/components/features/system/monitoring-page'

export const Route = createFileRoute('/system/monitoring')({
  component: MonitoringPage,
})
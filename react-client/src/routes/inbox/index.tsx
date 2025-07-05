import { createFileRoute } from '@tanstack/react-router'
import { InboxPage } from '@/components/features/inbox/inbox-page'

export const Route = createFileRoute('/inbox/')({
  component: InboxPage,
})
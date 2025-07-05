import { createFileRoute } from '@tanstack/react-router'
import { ComposeMessagePage } from '@/components/features/inbox/compose-message-page'

export const Route = createFileRoute('/inbox/compose')({
  component: ComposeMessagePage,
})
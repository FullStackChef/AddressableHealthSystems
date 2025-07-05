import { createFileRoute } from '@tanstack/react-router'
import { MessageDetailPage } from '@/components/features/inbox/message-detail-page'

export const Route = createFileRoute('/messages/$id')({
  component: MessageDetailPage,
})
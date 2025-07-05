import { createFileRoute } from '@tanstack/react-router'
import { DirectoryPage } from '@/components/features/admin/directory-page'

export const Route = createFileRoute('/admin/directory')({
  component: DirectoryPage,
})
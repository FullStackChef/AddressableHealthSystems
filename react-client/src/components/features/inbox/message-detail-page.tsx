import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { ArrowLeft } from 'lucide-react'
import { Link, useParams } from '@tanstack/react-router'
import { formatDate } from '@/lib/utils'

interface MessageDetail {
  id: string
  subject: string
  from: string
  body: string
  received: string
}

const mockMessages: MessageDetail[] = [
  {
    id: 'msg-001',
    subject: 'Referral for John Smith',
    from: 'Dr. Jones',
    received: new Date(Date.now() - 2 * 60 * 60 * 1000).toISOString(),
    body: 'Please review this patient\'s case.',
  },
  {
    id: 'msg-002',
    subject: 'Lab results for Jane Doe',
    from: 'PathLab Central',
    received: new Date(Date.now() - 24 * 60 * 60 * 1000).toISOString(),
    body: 'Lab results are attached in FHIR bundle.',
  },
]

export function MessageDetailPage() {
  const { id } = useParams({ from: '/messages/$id' })
  const message = mockMessages.find((m) => m.id === id)

  if (!message) {
    return (
      <div className="container mx-auto py-8">
        <Card>
          <CardContent className="p-6">
            <p>Message not found.</p>
          </CardContent>
        </Card>
      </div>
    )
  }

  return (
    <div className="container mx-auto py-8">
      <div className="mb-4">
        <Button variant="outline" asChild>
          <Link to="/inbox" className="flex items-center gap-2">
            <ArrowLeft className="h-4 w-4" />
            Back to Inbox
          </Link>
        </Button>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>{message.subject}</CardTitle>
          <div className="text-sm text-muted-foreground">
            <p><strong>From:</strong> {message.from}</p>
            <p><strong>Received:</strong> {formatDate(message.received)}</p>
          </div>
        </CardHeader>
        <CardContent>
          <div className="border-t pt-4">
            <p className="whitespace-pre-wrap">{message.body}</p>
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
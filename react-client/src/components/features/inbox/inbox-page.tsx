import { useState } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { DataTable } from '@/components/ui/data-table'
import { ColumnDef } from '@tanstack/react-table'
import { formatDate } from '@/lib/utils'
import { Eye } from 'lucide-react'
import { Link } from 'react-router-dom'

interface Message {
  id: string
  subject: string
  from: string
  received: string
}

const mockMessages: Message[] = [
  {
    id: 'msg-001',
    subject: 'Referral for John Smith',
    from: 'Dr. Jones',
    received: new Date(Date.now() - 2 * 60 * 60 * 1000).toISOString(),
  },
  {
    id: 'msg-002',
    subject: 'Lab results for Jane Doe',
    from: 'PathLab Central',
    received: new Date(Date.now() - 24 * 60 * 60 * 1000).toISOString(),
  },
]

const columns: ColumnDef<Message>[] = [
  {
    accessorKey: 'subject',
    header: 'Subject',
  },
  {
    accessorKey: 'from',
    header: 'From',
  },
  {
    accessorKey: 'received',
    header: 'Received',
    cell: ({ row }) => formatDate(row.getValue('received')),
  },
  {
    id: 'actions',
    header: 'Actions',
    cell: ({ row }) => (
      <Button variant="ghost" size="sm" asChild>
        <Link to={`/messages/${row.original.id}`}>
          <Eye className="h-4 w-4" />
        </Link>
      </Button>
    ),
  },
]

export function InboxPage() {
  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>Inbox</CardTitle>
        </CardHeader>
        <CardContent>
          <DataTable
            columns={columns}
            data={mockMessages}
            searchKey="subject"
            searchPlaceholder="Search messages..."
          />
        </CardContent>
      </Card>
    </div>
  )
}
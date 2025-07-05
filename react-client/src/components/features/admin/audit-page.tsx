import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { DataTable } from '@/components/ui/data-table'
import { ColumnDef } from '@tanstack/react-table'
import { formatDate } from '@/lib/utils'

interface AuditEvent {
  timestamp: string
  eventType: string
  actor: string
  target: string
  status: string
  correlationId: string
}

const mockAuditEvents: AuditEvent[] = [
  {
    timestamp: new Date(Date.now() - 2 * 60 * 1000).toISOString(),
    eventType: 'DiscoveryRequest',
    actor: 'admin@clinic.org',
    target: 'https://remote.ahs.org/fhir',
    status: 'Success',
    correlationId: 'abc123',
  },
  {
    timestamp: new Date(Date.now() - 5 * 60 * 1000).toISOString(),
    eventType: 'SyncComplete',
    actor: 'admin@clinic.org',
    target: 'https://remote.ahs.org/fhir',
    status: 'Success',
    correlationId: 'abc123',
  },
  {
    timestamp: new Date(Date.now() - 60 * 60 * 1000).toISOString(),
    eventType: 'EndpointUpdate',
    actor: 'admin@clinic.org',
    target: 'Central Clinic Endpoint',
    status: 'Error',
    correlationId: 'xyz789',
  },
]

const columns: ColumnDef<AuditEvent>[] = [
  {
    accessorKey: 'timestamp',
    header: 'Timestamp',
    cell: ({ row }) => formatDate(row.getValue('timestamp')),
  },
  {
    accessorKey: 'eventType',
    header: 'Event Type',
  },
  {
    accessorKey: 'actor',
    header: 'Actor',
  },
  {
    accessorKey: 'target',
    header: 'Target',
  },
  {
    accessorKey: 'status',
    header: 'Status',
    cell: ({ row }) => {
      const status = row.getValue('status') as string
      return (
        <span
          className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${
            status === 'Success'
              ? 'bg-green-100 text-green-800'
              : 'bg-red-100 text-red-800'
          }`}
        >
          {status}
        </span>
      )
    },
  },
  {
    accessorKey: 'correlationId',
    header: 'Correlation ID',
  },
]

export function AuditPage() {
  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>Audit Log</CardTitle>
        </CardHeader>
        <CardContent>
          <DataTable
            columns={columns}
            data={mockAuditEvents}
            searchKey="eventType"
            searchPlaceholder="Search audit events..."
          />
        </CardContent>
      </Card>
    </div>
  )
}
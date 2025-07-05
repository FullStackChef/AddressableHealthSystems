import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { DataTable } from '@/components/ui/data-table'
import { ColumnDef } from '@tanstack/react-table'
import { Eye, Pause } from 'lucide-react'
import { Link } from 'react-router-dom'

interface Tenant {
  name: string
  orgId: string
  status: string
  messageVolume: number
  agentUrl?: string
}

const mockTenants: Tenant[] = [
  {
    name: 'Greenfield Medical',
    orgId: 'org-1001',
    status: 'Active',
    messageVolume: 145,
    agentUrl: 'https://greenfield-agent.local',
  },
  {
    name: 'Lakeside Lab Group',
    orgId: 'org-2003',
    status: 'Active',
    messageVolume: 82,
    agentUrl: 'https://lakeside.local/agent',
  },
  {
    name: 'Northside Hospital',
    orgId: 'org-3110',
    status: 'Suspended',
    messageVolume: 0,
  },
]

const columns: ColumnDef<Tenant>[] = [
  {
    accessorKey: 'name',
    header: 'Name',
  },
  {
    accessorKey: 'orgId',
    header: 'Org ID',
  },
  {
    accessorKey: 'status',
    header: 'Status',
    cell: ({ row }) => {
      const status = row.getValue('status') as string
      return (
        <span
          className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${
            status === 'Active'
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
    accessorKey: 'messageVolume',
    header: 'Msg/30d',
  },
  {
    accessorKey: 'agentUrl',
    header: 'Agent URL',
    cell: ({ row }) => {
      const url = row.getValue('agentUrl') as string
      return url ? (
        <span className="text-sm text-muted-foreground">{url}</span>
      ) : (
        <span className="text-sm text-muted-foreground">-</span>
      )
    },
  },
  {
    id: 'actions',
    header: 'Actions',
    cell: ({ row }) => (
      <div className="flex gap-2">
        <Button variant="outline" size="sm" asChild>
          <Link to={`/system/tenants/${row.original.orgId}`}>
            <Eye className="h-4 w-4 mr-2" />
            View
          </Link>
        </Button>
        <Button
          variant="outline"
          size="sm"
          onClick={() => console.log('Suspending tenant:', row.original.name)}
        >
          <Pause className="h-4 w-4 mr-2" />
          Suspend
        </Button>
      </div>
    ),
  },
]

export function TenantsPage() {
  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>Registered Tenants</CardTitle>
        </CardHeader>
        <CardContent>
          <DataTable
            columns={columns}
            data={mockTenants}
            searchKey="name"
            searchPlaceholder="Search tenants..."
          />
        </CardContent>
      </Card>
    </div>
  )
}
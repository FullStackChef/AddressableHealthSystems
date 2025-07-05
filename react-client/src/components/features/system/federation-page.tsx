import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { DataTable } from '@/components/ui/data-table'
import { ColumnDef } from '@tanstack/react-table'
import { formatDate } from '@/lib/utils'
import { Search, RefreshCw } from 'lucide-react'

interface FederationPeer {
  url: string
  role: string
  lastDiscovery: string
  trustStatus: string
  syncStatus: string
}

const mockPeers: FederationPeer[] = [
  {
    url: 'https://ahs.partner-one.org/fhir',
    role: 'Mutual',
    lastDiscovery: new Date(Date.now() - 24 * 60 * 60 * 1000).toISOString(),
    trustStatus: '✅ Trusted',
    syncStatus: 'OK',
  },
  {
    url: 'https://ahs.lab-network.org/fhir',
    role: 'Spoke',
    lastDiscovery: new Date(Date.now() - 3 * 60 * 60 * 1000).toISOString(),
    trustStatus: '❌ Unverified',
    syncStatus: 'Out of sync',
  },
]

const columns: ColumnDef<FederationPeer>[] = [
  {
    accessorKey: 'url',
    header: 'Remote AHS URL',
  },
  {
    accessorKey: 'role',
    header: 'Role',
  },
  {
    accessorKey: 'lastDiscovery',
    header: 'Last Discovery',
    cell: ({ row }) => formatDate(row.getValue('lastDiscovery')),
  },
  {
    accessorKey: 'trustStatus',
    header: 'Trust',
  },
  {
    accessorKey: 'syncStatus',
    header: 'Sync',
    cell: ({ row }) => {
      const status = row.getValue('syncStatus') as string
      return (
        <span
          className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${
            status === 'OK'
              ? 'bg-green-100 text-green-800'
              : 'bg-yellow-100 text-yellow-800'
          }`}
        >
          {status}
        </span>
      )
    },
  },
  {
    id: 'actions',
    header: 'Actions',
    cell: ({ row }) => (
      <div className="flex gap-2">
        <Button
          variant="outline"
          size="sm"
          onClick={() => console.log('[Discovery] Triggering rediscovery for', row.original.url)}
        >
          <Search className="h-4 w-4 mr-2" />
          Discover
        </Button>
        <Button
          variant="outline"
          size="sm"
          onClick={() => console.log('[Sync] Initiating sync for', row.original.url)}
        >
          <RefreshCw className="h-4 w-4 mr-2" />
          Sync
        </Button>
      </div>
    ),
  },
]

export function FederationPage() {
  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>Federated Hubs</CardTitle>
        </CardHeader>
        <CardContent>
          <DataTable
            columns={columns}
            data={mockPeers}
            searchKey="url"
            searchPlaceholder="Search federation peers..."
          />
        </CardContent>
      </Card>
    </div>
  )
}
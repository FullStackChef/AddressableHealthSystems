import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { DataTable } from '@/components/ui/data-table'
import { ColumnDef } from '@tanstack/react-table'
import { Shield } from 'lucide-react'

interface Endpoint {
  id: string
  name: string
  address: string
  status: string
  connectionType: string
}

const mockEndpoints: Endpoint[] = [
  {
    id: 'ep1',
    name: "Dr. Smith's PMS",
    address: 'https://drsmith-pms.org/fhir',
    status: 'active',
    connectionType: 'hl7-fhir-rest',
  },
  {
    id: 'ep2',
    name: 'LabCorp FHIR',
    address: 'https://labcorp.io/fhir',
    status: 'active',
    connectionType: 'hl7-fhir-rest',
  },
  {
    id: 'ep3',
    name: 'Legacy HL7 Listener',
    address: 'tcp://10.1.1.5:2575',
    status: 'suspended',
    connectionType: 'hl7-v2-mllp',
  },
]

const columns: ColumnDef<Endpoint>[] = [
  {
    accessorKey: 'name',
    header: 'Name',
  },
  {
    accessorKey: 'address',
    header: 'URL',
  },
  {
    accessorKey: 'status',
    header: 'Status',
    cell: ({ row }) => {
      const status = row.getValue('status') as string
      return (
        <span
          className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${
            status === 'active'
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
    accessorKey: 'connectionType',
    header: 'Connection Type',
  },
  {
    id: 'actions',
    header: 'Actions',
    cell: ({ row }) => (
      <Button
        variant="outline"
        size="sm"
        onClick={() => console.log('Validating:', row.original.address)}
      >
        <Shield className="h-4 w-4 mr-2" />
        Validate
      </Button>
    ),
  },
]

export function EndpointsPage() {
  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>Delivery Endpoints</CardTitle>
        </CardHeader>
        <CardContent>
          <DataTable
            columns={columns}
            data={mockEndpoints}
            searchKey="name"
            searchPlaceholder="Search endpoints..."
          />
        </CardContent>
      </Card>
    </div>
  )
}
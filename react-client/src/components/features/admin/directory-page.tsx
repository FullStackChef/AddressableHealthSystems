import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Tabs, TabsContent, TabsList, TabsTrigger } from '@/components/ui/tabs'
import { DataTable } from '@/components/ui/data-table'
import { ColumnDef } from '@tanstack/react-table'

interface Organization {
  name: string
  type: string
  address: string
}

interface Practitioner {
  fullName: string
  role: string
  orgName: string
}

const mockOrganizations: Organization[] = [
  { name: 'Central Clinic', type: 'clinic', address: '123 Main St' },
  { name: 'Regional Hospital', type: 'hospital', address: '456 Health Rd' },
]

const mockPractitioners: Practitioner[] = [
  { fullName: 'Dr. Alice Moore', role: 'General Practitioner', orgName: 'Central Clinic' },
  { fullName: 'Dr. Ben Wright', role: 'Cardiologist', orgName: 'Regional Hospital' },
]

const organizationColumns: ColumnDef<Organization>[] = [
  {
    accessorKey: 'name',
    header: 'Name',
  },
  {
    accessorKey: 'type',
    header: 'Type',
  },
  {
    accessorKey: 'address',
    header: 'Address',
  },
]

const practitionerColumns: ColumnDef<Practitioner>[] = [
  {
    accessorKey: 'fullName',
    header: 'Name',
  },
  {
    accessorKey: 'role',
    header: 'Role',
  },
  {
    accessorKey: 'orgName',
    header: 'Organization',
  },
]

export function DirectoryPage() {
  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>Directory</CardTitle>
        </CardHeader>
        <CardContent>
          <Tabs defaultValue="organizations" className="w-full">
            <TabsList className="grid w-full grid-cols-2">
              <TabsTrigger value="organizations">Organizations</TabsTrigger>
              <TabsTrigger value="practitioners">Practitioners</TabsTrigger>
            </TabsList>
            
            <TabsContent value="organizations" className="mt-6">
              <DataTable
                columns={organizationColumns}
                data={mockOrganizations}
                searchKey="name"
                searchPlaceholder="Search organizations..."
              />
            </TabsContent>
            
            <TabsContent value="practitioners" className="mt-6">
              <DataTable
                columns={practitionerColumns}
                data={mockPractitioners}
                searchKey="fullName"
                searchPlaceholder="Search practitioners..."
              />
            </TabsContent>
          </Tabs>
        </CardContent>
      </Card>
    </div>
  )
}
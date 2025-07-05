import { useState, useEffect } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { useParams } from 'react-router-dom'
import { Save } from 'lucide-react'

interface TenantModel {
  name: string
  orgId: string
  status: string
  messageVolume: number
  agentUrl?: string
}

const mockTenants: TenantModel[] = [
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

const statuses = ['Active', 'Suspended']

export function TenantDetailPage() {
  const { id } = useParams<{ id: string }>()
  const [tenant, setTenant] = useState<TenantModel | null>(null)

  useEffect(() => {
    const foundTenant = mockTenants.find((t) => t.orgId === id)
    setTenant(foundTenant || null)
  }, [id])

  const handleSave = () => {
    if (tenant) {
      console.log('Saved tenant:', tenant.name, '/', tenant.status)
    }
  }

  if (!tenant) {
    return (
      <div className="container mx-auto py-8">
        <Card>
          <CardContent className="p-6">
            <p>Tenant not found.</p>
          </CardContent>
        </Card>
      </div>
    )
  }

  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>Tenant Details</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="name">Name</Label>
            <Input
              id="name"
              value={tenant.name}
              onChange={(e) => setTenant({ ...tenant, name: e.target.value })}
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="orgId">Org ID</Label>
            <Input id="orgId" value={tenant.orgId} disabled />
          </div>

          <div className="space-y-2">
            <Label htmlFor="agentUrl">Agent URL</Label>
            <Input
              id="agentUrl"
              value={tenant.agentUrl || ''}
              onChange={(e) => setTenant({ ...tenant, agentUrl: e.target.value })}
              placeholder="https://agent.example.com"
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="status">Status</Label>
            <Select
              value={tenant.status}
              onValueChange={(value) => setTenant({ ...tenant, status: value })}
            >
              <SelectTrigger>
                <SelectValue />
              </SelectTrigger>
              <SelectContent>
                {statuses.map((status) => (
                  <SelectItem key={status} value={status}>
                    {status}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>

          <div className="space-y-2">
            <Label htmlFor="messageVolume">Message Volume (30d)</Label>
            <Input
              id="messageVolume"
              type="number"
              value={tenant.messageVolume}
              disabled
            />
          </div>

          <Button onClick={handleSave} className="flex items-center gap-2">
            <Save className="h-4 w-4" />
            Save Changes
          </Button>
        </CardContent>
      </Card>
    </div>
  )
}
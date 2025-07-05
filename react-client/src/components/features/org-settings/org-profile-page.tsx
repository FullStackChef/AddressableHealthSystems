import { useState } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import { Label } from '@/components/ui/label'
import { Save } from 'lucide-react'

interface OrgProfile {
  name: string
  identifier: string
  email: string
  phone: string
  address: string
}

export function OrgProfilePage() {
  const [profile, setProfile] = useState<OrgProfile>({
    name: 'Greenfield Medical Group',
    identifier: 'urn:oid:2.16.840.1.113883.3.1234',
    email: 'admin@greenfield.org',
    phone: '+1-555-0123',
    address: '123 Healthcare Drive\nGreenfield, CA 90210',
  })

  const handleSave = () => {
    console.log('Saving organization profile:', profile)
    // In a real app, this would make an API call to save the profile
  }

  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>Organization Profile</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="name">Name</Label>
            <Input
              id="name"
              value={profile.name}
              onChange={(e) => setProfile({ ...profile, name: e.target.value })}
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="identifier">Org ID / OID</Label>
            <Input
              id="identifier"
              value={profile.identifier}
              onChange={(e) => setProfile({ ...profile, identifier: e.target.value })}
              placeholder="urn:oid:..."
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="email">Email</Label>
            <Input
              id="email"
              type="email"
              value={profile.email}
              onChange={(e) => setProfile({ ...profile, email: e.target.value })}
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="phone">Phone</Label>
            <Input
              id="phone"
              type="tel"
              value={profile.phone}
              onChange={(e) => setProfile({ ...profile, phone: e.target.value })}
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="address">Address</Label>
            <Textarea
              id="address"
              value={profile.address}
              onChange={(e) => setProfile({ ...profile, address: e.target.value })}
              rows={3}
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
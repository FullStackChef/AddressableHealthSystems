import { useState } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Checkbox } from '@/components/ui/checkbox'
import { Save } from 'lucide-react'

interface UserProfile {
  displayName: string
  email: string
  role: string
}

interface UserPreferences {
  receiveEmailAlerts: boolean
  showRealTimeToasts: boolean
}

export function SettingsPage() {
  const [profile, setProfile] = useState<UserProfile>({
    displayName: 'Dr. Taylor Jenkins',
    email: 'tjenkins@greenfield.org',
    role: 'Practitioner',
  })

  const [preferences, setPreferences] = useState<UserPreferences>({
    receiveEmailAlerts: true,
    showRealTimeToasts: true,
  })

  const handleSave = () => {
    console.log('Saving user settings:', { profile, preferences })
    // In a real app, this would make an API call to save the settings
  }

  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>My Settings</CardTitle>
        </CardHeader>
        <CardContent className="space-y-6">
          <div className="space-y-4">
            <h3 className="text-lg font-medium">Profile</h3>
            
            <div className="space-y-2">
              <Label htmlFor="displayName">Display Name</Label>
              <Input
                id="displayName"
                value={profile.displayName}
                onChange={(e) => setProfile({ ...profile, displayName: e.target.value })}
              />
            </div>

            <div className="space-y-2">
              <Label htmlFor="email">Email</Label>
              <Input
                id="email"
                value={profile.email}
                disabled
                className="bg-muted"
              />
            </div>

            <div className="space-y-2">
              <Label htmlFor="role">Role</Label>
              <Input
                id="role"
                value={profile.role}
                disabled
                className="bg-muted"
              />
            </div>
          </div>

          <div className="space-y-4">
            <h3 className="text-lg font-medium">Notifications</h3>
            
            <div className="flex items-center space-x-2">
              <Checkbox
                id="emailAlerts"
                checked={preferences.receiveEmailAlerts}
                onCheckedChange={(checked) =>
                  setPreferences({ ...preferences, receiveEmailAlerts: checked as boolean })
                }
              />
              <Label htmlFor="emailAlerts">Receive email alerts for new messages</Label>
            </div>

            <div className="flex items-center space-x-2">
              <Checkbox
                id="realtimeToasts"
                checked={preferences.showRealTimeToasts}
                onCheckedChange={(checked) =>
                  setPreferences({ ...preferences, showRealTimeToasts: checked as boolean })
                }
              />
              <Label htmlFor="realtimeToasts">Show real-time toast notifications</Label>
            </div>
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
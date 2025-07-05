import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'

interface MonitoringStats {
  totalMessages: number
  activeTenants: number
  failedDeliveries: number
  federationHealthy: boolean
  auditQueueStatus: string
}

const mockStats: MonitoringStats = {
  totalMessages: 4233,
  activeTenants: 22,
  failedDeliveries: 3,
  federationHealthy: true,
  auditQueueStatus: 'Idle',
}

export function MonitoringPage() {
  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>System Monitoring</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            <div className="space-y-2">
              <h4 className="text-sm font-medium text-muted-foreground">Total Messages</h4>
              <p className="text-2xl font-bold">{mockStats.totalMessages.toLocaleString()}</p>
            </div>
            
            <div className="space-y-2">
              <h4 className="text-sm font-medium text-muted-foreground">Active Tenants</h4>
              <p className="text-2xl font-bold">{mockStats.activeTenants}</p>
            </div>
            
            <div className="space-y-2">
              <h4 className="text-sm font-medium text-muted-foreground">Failed Deliveries (24h)</h4>
              <p className="text-2xl font-bold text-red-600">{mockStats.failedDeliveries}</p>
            </div>
            
            <div className="space-y-2">
              <h4 className="text-sm font-medium text-muted-foreground">Federation Health</h4>
              <p className="text-2xl font-bold">
                {mockStats.federationHealthy ? '✅ OK' : '❌ Issue'}
              </p>
            </div>
            
            <div className="space-y-2">
              <h4 className="text-sm font-medium text-muted-foreground">Audit Queue</h4>
              <p className="text-2xl font-bold">{mockStats.auditQueueStatus}</p>
            </div>
          </div>
        </CardContent>
      </Card>
    </div>
  )
}
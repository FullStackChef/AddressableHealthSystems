import { useState } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Search, RefreshCw } from 'lucide-react'

interface DiscoveryRequest {
  endpointUrl: string
}

interface DiscoveryResult {
  remoteName: string
  softwareName: string
  version: string
  role: string
  isAhsCompatible: boolean
  isTrusted: boolean
  certificateThumbprint?: string
  supportedResources: string[]
}

export function DiscoveryPage() {
  const [request, setRequest] = useState<DiscoveryRequest>({ endpointUrl: '' })
  const [result, setResult] = useState<DiscoveryResult | null>(null)

  const runDiscovery = async () => {
    setResult(null)

    try {
      const response = await fetch('/discover', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(request),
      })

      if (response.ok) {
        const discoveryResult = await response.json()
        setResult(discoveryResult)
      } else {
        setResult({
          remoteName: request.endpointUrl,
          softwareName: 'Error: Failed response',
          version: 'N/A',
          isAhsCompatible: false,
          isTrusted: false,
          role: 'unknown',
          supportedResources: [],
        })
      }
    } catch (error) {
      setResult({
        remoteName: request.endpointUrl,
        softwareName: `Error: ${error}`,
        version: 'N/A',
        isAhsCompatible: false,
        isTrusted: false,
        role: 'unknown',
        supportedResources: [],
      })
    }
  }

  const syncToDirectory = async () => {
    if (!result) return

    const peer = {
      id: result.remoteName,
      baseUrl: request.endpointUrl,
      messagingEndpoint: request.endpointUrl,
      isTrusted: result.isTrusted,
    }

    console.log(`[Sync] Pushing ${result.remoteName} to local directory...`)

    try {
      const response = await fetch('/peers', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(peer),
      })
      response.ok && console.log('Sync successful')
    } catch (error) {
      console.log(`[Sync] Failed: ${error}`)
    }
  }

  return (
    <div className="container mx-auto py-8 space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Discovery</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="endpointUrl">Discover Remote AHS Instance</Label>
            <div className="flex gap-2">
              <Input
                id="endpointUrl"
                value={request.endpointUrl}
                onChange={(e) => setRequest({ endpointUrl: e.target.value })}
                placeholder="https://remote.ahs.org/fhir"
                className="flex-1"
              />
              <Button onClick={runDiscovery} className="flex items-center gap-2">
                <Search className="h-4 w-4" />
                Run Discovery
              </Button>
            </div>
          </div>
        </CardContent>
      </Card>

      {result && (
        <Card>
          <CardHeader>
            <CardTitle>Discovery Result</CardTitle>
          </CardHeader>
          <CardContent className="space-y-4">
            <div className="grid grid-cols-2 gap-4">
              <div>
                <p><strong>Remote URL:</strong> {result.remoteName}</p>
                <p><strong>Software:</strong> {result.softwareName}</p>
                <p><strong>Version:</strong> {result.version}</p>
                <p><strong>Role:</strong> {result.role}</p>
              </div>
              <div>
                <p><strong>Compatible:</strong> {result.isAhsCompatible ? '✅ Yes' : '❌ No'}</p>
                <p><strong>Trusted:</strong> {result.isTrusted ? '✅ Trusted' : '❌ Untrusted'}</p>
                {result.certificateThumbprint && (
                  <p><strong>Certificate Thumbprint:</strong> {result.certificateThumbprint}</p>
                )}
              </div>
            </div>

            {result.supportedResources?.length > 0 && (
              <div>
                <h4 className="font-semibold mb-2">Supported FHIR Resources</h4>
                <ul className="list-disc list-inside space-y-1">
                  {result.supportedResources.map((resource) => (
                    <li key={resource}>{resource}</li>
                  ))}
                </ul>
              </div>
            )}

            {result.isAhsCompatible && (
              <Button onClick={syncToDirectory} className="flex items-center gap-2">
                <RefreshCw className="h-4 w-4" />
                Sync to Directory
              </Button>
            )}
          </CardContent>
        </Card>
      )}
    </div>
  )
}
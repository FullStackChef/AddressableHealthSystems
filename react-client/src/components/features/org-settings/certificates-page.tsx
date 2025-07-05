import { useState } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { DataTable } from '@/components/ui/data-table'
import { ColumnDef } from '@tanstack/react-table'
import { formatDate } from '@/lib/utils'
import { Trash2, Upload } from 'lucide-react'

interface Certificate {
  commonName: string
  thumbprint: string
  expires: string
  trustType: string
  status: string
}

const mockCertificates: Certificate[] = [
  {
    commonName: 'greenfield.org',
    thumbprint: 'ABCD1234EF567890...',
    expires: new Date(Date.now() + 6 * 30 * 24 * 60 * 60 * 1000).toISOString(),
    trustType: 'Inbound',
    status: 'Active',
  },
  {
    commonName: 'test.partner.local',
    thumbprint: '9988AABBCCDD...',
    expires: new Date(Date.now() - 10 * 24 * 60 * 60 * 1000).toISOString(),
    trustType: 'Outbound',
    status: 'Expired',
  },
]

const columns: ColumnDef<Certificate>[] = [
  {
    accessorKey: 'commonName',
    header: 'Common Name',
  },
  {
    accessorKey: 'thumbprint',
    header: 'Thumbprint',
    cell: ({ row }) => {
      const thumbprint = row.getValue('thumbprint') as string
      return <span className="font-mono text-sm">{thumbprint}</span>
    },
  },
  {
    accessorKey: 'expires',
    header: 'Expires',
    cell: ({ row }) => formatDate(row.getValue('expires')),
  },
  {
    accessorKey: 'trustType',
    header: 'Trust Type',
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
              : status === 'Expired'
              ? 'bg-red-100 text-red-800'
              : 'bg-gray-100 text-gray-800'
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
      <Button
        variant="outline"
        size="sm"
        onClick={() => console.log('Revoking cert:', row.original.thumbprint)}
      >
        <Trash2 className="h-4 w-4 mr-2" />
        Revoke
      </Button>
    ),
  },
]

export function CertificatesPage() {
  const [certificates, setCertificates] = useState(mockCertificates)

  const handleFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0]
    if (file) {
      // Simulate certificate upload
      const newCert: Certificate = {
        commonName: `Uploaded ${new Date().toISOString()}`,
        thumbprint: Math.random().toString(36).substring(2, 15).toUpperCase(),
        expires: new Date(Date.now() + 365 * 24 * 60 * 60 * 1000).toISOString(),
        trustType: 'Inbound',
        status: 'Active',
      }
      setCertificates([...certificates, newCert])
    }
  }

  return (
    <div className="container mx-auto py-8 space-y-6">
      <Card>
        <CardHeader>
          <CardTitle>Organization Certificates</CardTitle>
        </CardHeader>
        <CardContent>
          <DataTable
            columns={columns}
            data={certificates}
            searchKey="commonName"
            searchPlaceholder="Search certificates..."
          />
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle>Upload New Certificate</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="certificate">Select PEM / X.509 certificate</Label>
            <Input
              id="certificate"
              type="file"
              accept=".crt,.pem,.cer"
              onChange={handleFileUpload}
            />
          </div>
          <p className="text-sm text-muted-foreground">
            Supported formats: .pem, .crt, .cer
          </p>
        </CardContent>
      </Card>
    </div>
  )
}
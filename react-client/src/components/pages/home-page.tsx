import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card'

export function HomePage() {
  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>Welcome to Addressable Health Systems</CardTitle>
          <CardDescription>
            Secure, federated exchange of FHIR resources between trusted healthcare organizations.
          </CardDescription>
        </CardHeader>
        <CardContent>
          <p className="text-muted-foreground">
            Use the navigation menu to access different features of the system including messaging,
            administration, and system management.
          </p>
        </CardContent>
      </Card>
    </div>
  )
}
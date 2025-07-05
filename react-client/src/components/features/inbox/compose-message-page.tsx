import { useState } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import { Label } from '@/components/ui/label'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { Send } from 'lucide-react'

interface OutgoingMessage {
  to: string
  subject: string
  body: string
}

const recipientOptions = [
  { id: 'org-1001', display: 'Greenfield Medical Group' },
  { id: 'org-2003', display: 'Lakeside Lab Group' },
]

export function ComposeMessagePage() {
  const [message, setMessage] = useState<OutgoingMessage>({
    to: '',
    subject: '',
    body: '',
  })
  const [statusMessage, setStatusMessage] = useState<string>('')

  const handleSend = async () => {
    console.log('[SEND] To:', message.to, 'Subject:', message.subject)

    const communication = {
      resourceType: 'Communication',
      status: 'completed',
      recipient: [{ identifier: { value: message.to } }],
      subject: { text: message.subject },
      payload: [{ contentString: message.body }],
      sent: new Date().toISOString(),
    }

    try {
      const response = await fetch('/api/communication', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(communication),
      })

      if (response.ok) {
        setStatusMessage('Message sent successfully.')
        setMessage({ to: '', subject: '', body: '' })
      } else {
        setStatusMessage(`Failed to send message (${response.status}).`)
      }
    } catch (error) {
      setStatusMessage(`Error sending message: ${error}`)
    }
  }

  return (
    <div className="container mx-auto py-8">
      <Card>
        <CardHeader>
          <CardTitle>Compose Message</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="to">To</Label>
            <Select value={message.to} onValueChange={(value) => setMessage({ ...message, to: value })}>
              <SelectTrigger>
                <SelectValue placeholder="Select recipient" />
              </SelectTrigger>
              <SelectContent>
                {recipientOptions.map((option) => (
                  <SelectItem key={option.id} value={option.id}>
                    {option.display}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>

          <div className="space-y-2">
            <Label htmlFor="subject">Subject</Label>
            <Input
              id="subject"
              value={message.subject}
              onChange={(e) => setMessage({ ...message, subject: e.target.value })}
              placeholder="Enter subject"
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="body">Body</Label>
            <Textarea
              id="body"
              value={message.body}
              onChange={(e) => setMessage({ ...message, body: e.target.value })}
              placeholder="Enter message body"
              rows={6}
            />
          </div>

          <Button onClick={handleSend} className="flex items-center gap-2">
            <Send className="h-4 w-4" />
            Send
          </Button>

          {statusMessage && (
            <p className="text-sm text-muted-foreground">{statusMessage}</p>
          )}
        </CardContent>
      </Card>
    </div>
  )
}
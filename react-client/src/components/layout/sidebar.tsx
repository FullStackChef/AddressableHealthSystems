import { cn } from '@/lib/utils'
import { Button } from '@/components/ui/button'
import { Link } from '@tanstack/react-router'
import {
  Mail,
  Edit,
  Settings,
  Users,
  Search,
  Contacts,
  FileText,
  Network,
  Building,
  BarChart3,
  Shield,
  Key,
  X,
} from 'lucide-react'

interface SidebarProps {
  open: boolean
  onClose: () => void
}

const navigation = [
  {
    name: 'Inbox',
    icon: Mail,
    children: [
      { name: 'Messages', href: '/inbox', icon: Mail },
      { name: 'Compose', href: '/inbox/compose', icon: Edit },
    ],
  },
  {
    name: 'Admin',
    icon: Settings,
    children: [
      { name: 'Endpoints', href: '/admin/endpoints', icon: Network },
      { name: 'Discovery', href: '/admin/discovery', icon: Search },
      { name: 'Directory', href: '/admin/directory', icon: Contacts },
      { name: 'Audit Log', href: '/admin/audit', icon: FileText },
    ],
  },
  {
    name: 'System',
    icon: BarChart3,
    children: [
      { name: 'Federation', href: '/system/federation', icon: Network },
      { name: 'Tenants', href: '/system/tenants', icon: Building },
      { name: 'Monitoring', href: '/system/monitoring', icon: BarChart3 },
    ],
  },
  {
    name: 'Org Settings',
    icon: Building,
    children: [
      { name: 'Profile', href: '/org-settings/profile', icon: Shield },
      { name: 'Certificates', href: '/org-settings/certs', icon: Key },
    ],
  },
]

export function Sidebar({ open, onClose }: SidebarProps) {
  return (
    <>
      {/* Mobile overlay */}
      {open && (
        <div
          className="fixed inset-0 z-40 bg-black bg-opacity-50 lg:hidden"
          onClick={onClose}
        />
      )}
      
      {/* Sidebar */}
      <div
        className={cn(
          'fixed inset-y-0 left-0 z-50 w-64 transform bg-card border-r transition-transform duration-200 ease-in-out lg:static lg:translate-x-0',
          open ? 'translate-x-0' : '-translate-x-full'
        )}
      >
        <div className="flex h-full flex-col">
          <div className="flex items-center justify-between p-4 lg:hidden">
            <span className="text-lg font-semibold">Menu</span>
            <Button variant="ghost" size="icon" onClick={onClose}>
              <X className="h-5 w-5" />
            </Button>
          </div>
          
          <nav className="flex-1 space-y-2 p-4">
            {navigation.map((section) => (
              <div key={section.name} className="space-y-1">
                <div className="flex items-center gap-2 px-2 py-1 text-sm font-medium text-muted-foreground">
                  <section.icon className="h-4 w-4" />
                  {section.name}
                </div>
                <div className="ml-6 space-y-1">
                  {section.children.map((item) => (
                    <Button
                      key={item.href}
                      variant="ghost"
                      className="w-full justify-start"
                      asChild
                    >
                      <Link to={item.href} className="flex items-center gap-2">
                        <item.icon className="h-4 w-4" />
                        {item.name}
                      </Link>
                    </Button>
                  ))}
                </div>
              </div>
            ))}
          </nav>
        </div>
      </div>
    </>
  )
}
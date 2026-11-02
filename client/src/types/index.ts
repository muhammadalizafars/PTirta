export interface ServiceItem {
  id: number
  title: string
  description: string
  category: 'perencanaan' | 'kajian' | 'op' | string
  categoryLabel: string
  iconClass: string
  imageUrl: string
}

export interface PortfolioItem {
  id: number
  title: string
  description: string
  category: 'banjir' | 'sda' | 'kajian' | 'irigasi' | string
  categoryLabel: string
  location: string
  year: string
  status: string
  imageUrl: string
}

export interface User {
  id: string
  fullName: string
  email: string
  userName: string
}

export interface AuthResponse {
  success: boolean
  message: string
  user?: User
  errors?: string[]
}


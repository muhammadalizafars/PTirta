import axios from 'axios'

export const apiClient = axios.create({
  baseURL: '/api/v1',
  headers: {
    'Content-Type': 'application/json'
  },
  withCredentials: true
})

// Response interceptor for centralized error logging or handling
apiClient.interceptors.response.use(
  response => response,
  error => {
    if (error.response?.status === 401) {
      console.warn('Session expired or unauthorized.')
    }
    return Promise.reject(error)
  }
)


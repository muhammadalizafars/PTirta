import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { apiClient } from '@/api/client'
import type { User, AuthResponse } from '@/types'

export const useAuthStore = defineStore('auth', () => {
  const user = ref<User | null>(null)
  const isLoading = ref<boolean>(false)
  const error = ref<string | null>(null)

  const isAuthenticated = computed(() => !!user.value)

  async function checkAuth(): Promise<void> {
    try {
      isLoading.value = true
      const response = await apiClient.get<AuthResponse>('/auth/me')
      if (response.data.success && response.data.user) {
        user.value = response.data.user
      } else {
        user.value = null
      }
    } catch {
      user.value = null
    } finally {
      isLoading.value = false
    }
  }

  async function login(credentials: { email: string; password: string; rememberMe: boolean }): Promise<AuthResponse> {
    try {
      isLoading.value = true
      error.value = null
      const response = await apiClient.post<AuthResponse>('/auth/login', credentials)
      if (response.data.success && response.data.user) {
        user.value = response.data.user
      }
      return response.data
    } catch (err: any) {
      const msg = err.response?.data?.message || 'Gagal masuk. Periksa kembali email dan kata sandi Anda.'
      error.value = msg
      throw new Error(msg)
    } finally {
      isLoading.value = false
    }
  }

  async function register(payload: { name: string; email: string; password: string; confirmPassword: string }): Promise<AuthResponse> {
    try {
      isLoading.value = true
      error.value = null
      const response = await apiClient.post<AuthResponse>('/auth/register', payload)
      return response.data
    } catch (err: any) {
      const msg = err.response?.data?.message || 'Gagal mendaftarkan akun.'
      error.value = msg
      throw new Error(msg)
    } finally {
      isLoading.value = false
    }
  }

  async function logout(): Promise<void> {
    try {
      await apiClient.post('/auth/logout')
    } finally {
      user.value = null
    }
  }

  return {
    user,
    isLoading,
    error,
    isAuthenticated,
    checkAuth,
    login,
    register,
    logout
  }
})


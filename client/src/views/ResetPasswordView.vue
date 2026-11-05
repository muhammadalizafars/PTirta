<script setup lang="ts">
import { reactive, ref } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import AuthLayout from '@/layouts/AuthLayout.vue'

const authStore = useAuthStore()
const router = useRouter()
const route = useRoute()

const token = route.params.token as string
const form = reactive({
  password: '',
  confirm: ''
})

const localError = ref<string | null>(null)
const isSuccess = ref(false)

const handleSubmit = async () => {
  localError.value = null

  if (!form.password || form.password.length < 6) {
    localError.value = 'Kata sandi minimal 6 karakter.'
    return
  }

  if (form.password !== form.confirm) {
    localError.value = 'Konfirmasi kata sandi tidak cocok.'
    return
  }

  try {
    await authStore.resetPassword(token, form.password)
    isSuccess.value = true
    setTimeout(() => router.push('/login'), 2000)
  } catch (err: any) {
    localError.value = err.message || 'Gagal mengatur ulang kata sandi.'
  }
}
</script>

<template>
  <AuthLayout>
    <div class="account-container">
      <div class="account-box">
        <div class="auth-header">
          <router-link to="/">
            <img src="/assets/img/Logo/Logo.png" alt="Logo PT Tirta" class="auth-logo" />
          </router-link>
          <h2 class="auth-title">Atur Ulang Kata Sandi</h2>
          <p class="auth-subtitle">Masukkan kata sandi baru Anda</p>
        </div>

        <div v-if="isSuccess" class="text-center py-4">
          <div class="mb-4">
            <i class="bi bi-check-circle-fill text-success" style="font-size: 3.5rem;"></i>
          </div>
          <h4 class="mb-3">Berhasil!</h4>
          <p class="text-muted mb-4">Kata sandi Anda telah diperbarui. Mengalihkan ke halaman login...</p>
        </div>

        <form v-else @submit.prevent="handleSubmit">
          <div v-if="localError" class="alert alert-danger py-2 small mb-3">
            <i class="bi bi-exclamation-circle-fill me-1"></i> {{ localError }}
          </div>

          <div class="mb-3">
            <label class="form-label">Kata Sandi Baru</label>
            <div class="input-group-custom">
              <span class="input-icon-wrapper"><i class="bi bi-lock"></i></span>
              <input v-model="form.password" type="password" class="form-control-custom" placeholder="••••••••" required />
            </div>
          </div>

          <div class="mb-4">
            <label class="form-label">Konfirmasi Kata Sandi Baru</label>
            <div class="input-group-custom">
              <span class="input-icon-wrapper"><i class="bi bi-shield-lock"></i></span>
              <input v-model="form.confirm" type="password" class="form-control-custom" placeholder="••••••••" required />
            </div>
          </div>

          <button type="submit" class="btn-auth-primary" :disabled="authStore.isLoading">
            <span v-if="authStore.isLoading" class="spinner-border spinner-border-sm me-2"></span>
            {{ authStore.isLoading ? 'Memproses...' : 'Perbarui Kata Sandi' }}
          </button>
        </form>
      </div>
    </div>
  </AuthLayout>
</template>

<style scoped>
.account-container {
  background: linear-gradient(135deg, rgba(15, 23, 42, 0.85) 0%, rgba(2, 132, 199, 0.8) 100%),
              url('/assets/img/hero-carousel/hero-carousel-1.jpg') center/cover no-repeat fixed;
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  padding: 40px 20px;
}

.account-box {
  background-color: #ffffff;
  padding: 40px 36px;
  border-radius: 20px;
  box-shadow: 0 20px 45px rgba(0, 0, 0, 0.22);
  max-width: 460px;
  width: 100%;
}

.auth-header {
  text-align: center;
  margin-bottom: 28px;
}

.auth-logo {
  max-height: 52px;
  margin-bottom: 16px;
  object-fit: contain;
}

.auth-title {
  font-size: 1.6rem;
  font-weight: 700;
  color: #0f172a;
  margin-bottom: 6px;
}

.auth-subtitle {
  font-size: 0.88rem;
  color: #64748b;
}

.form-label {
  font-size: 0.88rem;
  font-weight: 600;
  color: #334155;
  margin-bottom: 6px;
}

.input-group-custom {
  position: relative;
}

.input-icon-wrapper {
  position: absolute;
  top: 50%;
  left: 14px;
  transform: translateY(-50%);
  color: #94a3b8;
  font-size: 1.1rem;
  pointer-events: none;
}

.form-control-custom {
  width: 100%;
  padding: 11px 16px 11px 42px;
  font-size: 0.92rem;
  border-radius: 10px;
  border: 1.5px solid #e2e8f0;
  background-color: #f8fafc;
  outline: none;
}

.form-control-custom:focus {
  background-color: #ffffff;
  border-color: #0284c7;
  box-shadow: 0 0 0 4px rgba(2, 132, 199, 0.15);
}

.btn-auth-primary {
  background: #0284c7;
  color: #ffffff;
  font-size: 0.98rem;
  font-weight: 600;
  padding: 12px 20px;
  border-radius: 10px;
  border: none;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: all 0.25s ease;
}

@media (max-width: 767.98px) {
  .account-box {
    padding: 28px 24px;
    border-radius: 16px;
  }
}
</style>

<script setup lang="ts">
import { reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import AuthLayout from '@/layouts/AuthLayout.vue'

const authStore = useAuthStore()
const router = useRouter()

const form = reactive({
  name: '',
  email: '',
  password: '',
  confirmPassword: ''
})

const localError = ref<string | null>(null)
const successMessage = ref<string | null>(null)

const handleSubmit = async () => {
  localError.value = null
  successMessage.value = null

  if (form.password !== form.confirmPassword) {
    localError.value = 'Konfirmasi kata sandi tidak cocok.'
    return
  }

  try {
    const res = await authStore.register({
      name: form.name,
      email: form.email,
      password: form.password,
      confirmPassword: form.confirmPassword
    })
    successMessage.value = res.message || 'Registrasi berhasil! Mengalihkan ke login...'
    setTimeout(() => {
      router.push('/login')
    }, 1500)
  } catch (err: any) {
    localError.value = err.message || 'Pendaftaran gagal.'
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
          <h2 class="auth-title">Buat Akun Baru</h2>
          <p class="auth-subtitle">Daftarkan akun untuk akses layanan PT. Tirta</p>
        </div>

        <form @submit.prevent="handleSubmit">
          <div v-if="localError" class="alert alert-danger py-2 small mb-3">
            <i class="bi bi-exclamation-circle-fill me-1"></i> {{ localError }}
          </div>
          <div v-if="successMessage" class="alert alert-success py-2 small mb-3">
            <i class="bi bi-check-circle-fill me-1"></i> {{ successMessage }}
          </div>

          <div class="mb-3">
            <label class="form-label">Nama Lengkap</label>
            <div class="input-group-custom">
              <span class="input-icon-wrapper"><i class="bi bi-person"></i></span>
              <input v-model="form.name" type="text" class="form-control-custom" placeholder="Nama Anda" required />
            </div>
          </div>

          <div class="mb-3">
            <label class="form-label">Email</label>
            <div class="input-group-custom">
              <span class="input-icon-wrapper"><i class="bi bi-envelope"></i></span>
              <input v-model="form.email" type="email" class="form-control-custom" placeholder="nama@email.com" required />
            </div>
          </div>

          <div class="mb-3">
            <label class="form-label">Kata Sandi</label>
            <div class="input-group-custom">
              <span class="input-icon-wrapper"><i class="bi bi-lock"></i></span>
              <input v-model="form.password" type="password" class="form-control-custom" placeholder="Minimal 6 karakter" required minlength="6" />
            </div>
          </div>

          <div class="mb-4">
            <label class="form-label">Konfirmasi Kata Sandi</label>
            <div class="input-group-custom">
              <span class="input-icon-wrapper"><i class="bi bi-shield-lock"></i></span>
              <input v-model="form.confirmPassword" type="password" class="form-control-custom" placeholder="Ulangi kata sandi" required />
            </div>
          </div>

          <button type="submit" class="btn-auth-primary" :disabled="authStore.isLoading">
            <span v-if="authStore.isLoading" class="spinner-border spinner-border-sm me-2"></span>
            <i v-else class="bi bi-person-plus-fill me-1"></i> 
            {{ authStore.isLoading ? 'Mendaftarkan...' : 'Daftarkan Akun' }}
          </button>

          <div class="auth-divider">
            <span>sudah punya akun?</span>
          </div>

          <p class="text-center mb-3" style="font-size: 0.9rem; color: #64748b;">
            Sudah memiliki akun?
            <router-link to="/login" class="auth-link">Masuk sekarang</router-link>
          </p>

          <div class="text-center">
            <router-link to="/" class="auth-back-link">
              <i class="bi bi-arrow-left"></i> Kembali ke Beranda
            </router-link>
          </div>
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

.auth-link {
  color: #0284c7;
  font-size: 0.86rem;
  font-weight: 600;
  text-decoration: none;
}

.auth-link:hover {
  text-decoration: underline;
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

.btn-auth-primary:hover:not(:disabled) {
  background: #0369a1;
  transform: translateY(-2px);
}

.auth-divider {
  display: flex;
  align-items: center;
  text-align: center;
  margin: 22px 0;
  color: #94a3b8;
  font-size: 0.82rem;
}

.auth-divider::before,
.auth-divider::after {
  content: '';
  flex: 1;
  border-bottom: 1px solid #e2e8f0;
}

.auth-divider span {
  padding: 0 12px;
}

.auth-back-link {
  display: inline-flex;
  align-items: center;
  gap: 6px;
  color: #64748b;
  font-size: 0.88rem;
  text-decoration: none;
}
</style>


<script setup lang="ts">
import { ref } from 'vue'
import { useAuthStore } from '@/stores/auth'
import AuthLayout from '@/layouts/AuthLayout.vue'

const authStore = useAuthStore()
const email = ref('')
const isSubmitted = ref(false)
const localError = ref<string | null>(null)

const handleSubmit = async () => {
  localError.value = null
  if (!email.value) {
    localError.value = 'Silakan isi email Anda.'
    return
  }

  try {
    await authStore.forgotPassword(email.value)
    isSubmitted.value = true
  } catch (err: any) {
    localError.value = err.message || 'Gagal mengirim permintaan.'
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
          <h2 class="auth-title">Lupa Kata Sandi</h2>
          <p class="auth-subtitle">Masukkan email Anda untuk menerima instruksi reset</p>
        </div>

        <div v-if="isSubmitted" class="text-center py-4">
          <div class="mb-4">
            <i class="bi bi-envelope-check-fill text-success" style="font-size: 3.5rem;"></i>
          </div>
          <h4 class="mb-3">Periksa Email Anda</h4>
          <p class="text-muted mb-4">
            Kami telah mengirimkan instruksi ke <strong>{{ email }}</strong> jika terdaftar dalam sistem kami.
          </p>
          <router-link to="/login" class="btn-auth-primary">Kembali ke Login</router-link>
        </div>

        <form v-else @submit.prevent="handleSubmit">
          <div v-if="localError" class="alert alert-danger py-2 small mb-3">
            <i class="bi bi-exclamation-circle-fill me-1"></i> {{ localError }}
          </div>

          <div class="mb-4">
            <label class="form-label">Email Terdaftar</label>
            <div class="input-group-custom">
              <span class="input-icon-wrapper"><i class="bi bi-envelope"></i></span>
              <input v-model="email" type="email" class="form-control-custom" placeholder="nama@email.com" required />
            </div>
          </div>

          <button type="submit" class="btn-auth-primary mb-3" :disabled="authStore.isLoading">
            <span v-if="authStore.isLoading" class="spinner-border spinner-border-sm me-2"></span>
            {{ authStore.isLoading ? 'Memproses...' : 'Kirim Instruksi' }}
          </button>

          <div class="text-center">
            <router-link to="/login" class="auth-back-link">
              <i class="bi bi-arrow-left"></i> Kembali ke Login
            </router-link>
          </div>
        </form>
      </div>
    </div>
  </AuthLayout>
</template>

<style scoped>
/* scoped styles (match LoginView.vue) */
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
  text-decoration: none;
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

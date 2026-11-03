<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

const authStore = useAuthStore()
const router = useRouter()
const isMobileNavActive = ref(false)

const toggleMobileNav = () => {
  isMobileNavActive.value = !isMobileNavActive.value
}

const closeMobileNav = () => {
  isMobileNavActive.value = false
}

const handleLogout = async () => {
  await authStore.logout()
  router.push('/login')
}

onMounted(() => {
  authStore.checkAuth()
})
</script>

<template>
  <div class="layout-wrapper">
    <!-- Header -->
    <header id="header" class="header d-flex align-items-center fixed-top">
      <div class="container-fluid container-xl position-relative d-flex align-items-center justify-content-between">
        <router-link to="/" class="navbar-brand d-flex align-items-center" @click="closeMobileNav">
          <img src="/assets/img/Logo/Logo.png" alt="PT Tirta" style="max-height: 52px; width: auto;" />
        </router-link>

        <nav id="navmenu" class="navmenu" :class="{ 'mobile-nav-active': isMobileNavActive }">
          <ul>
            <li><router-link to="/" @click="closeMobileNav">Home</router-link></li>
            <li><router-link to="/layanan" @click="closeMobileNav">Service</router-link></li>
            <li><router-link to="/portofolio" @click="closeMobileNav">Portofolio</router-link></li>
            
            <li v-if="authStore.isAuthenticated" class="dropdown-item-user">
              <span class="user-greeting">
                <i class="bi bi-person-circle me-1"></i> {{ authStore.user?.fullName || authStore.user?.userName }}
              </span>
              <button type="button" class="btn-nav-logout" @click="handleLogout">
                Logout
              </button>
            </li>
            <li v-else>
              <router-link to="/login" class="btn-nav-login" @click="closeMobileNav">
                Login
              </router-link>
            </li>
          </ul>
          <i
            class="mobile-nav-toggle d-lg-none bi"
            :class="isMobileNavActive ? 'bi-x' : 'bi-list'"
            @click="toggleMobileNav"
          ></i>
        </nav>
      </div>
    </header>

    <!-- Content -->
    <main class="main-content">
      <slot />
    </main>

    <!-- Footer -->
    <footer id="footer" class="footer dark-background">
      <div class="container footer-top">
        <div class="row gy-4">
          <div class="col-lg-5 col-md-6 footer-about">
            <router-link to="/" class="logo d-flex align-items-center">
              <span class="sitename">PT. Tirta Rekayasa Konsultan</span>
            </router-link>
            <div class="footer-contact pt-3">
              <p>JL. Bengawan NO. 45, Kel. Cihapit,</p>
              <p>Kec. Bandung Wetan, Kota Bandung</p>
              <p>Jawa Barat - Indonesia</p>
              <p class="mt-3">
                <strong>Email:</strong> 
                <a href="mailto:tirtarekayasakonsultanbdg@gmail.com" class="text-white-50 ms-1">
                  tirtarekayasakonsultanbdg@gmail.com
                </a>
              </p>
            </div>
          </div>

          <div class="col-lg-3 col-md-6 footer-links">
            <h4>Navigasi Cepat</h4>
            <ul class="list-unstyled">
              <li class="py-1"><router-link to="/" class="text-white-50 text-decoration-none">Beranda</router-link></li>
              <li class="py-1"><router-link to="/layanan" class="text-white-50 text-decoration-none">Layanan Konsultansi</router-link></li>
              <li class="py-1"><router-link to="/portofolio" class="text-white-50 text-decoration-none">Portofolio Pekerjaan</router-link></li>
            </ul>
          </div>

          <div class="col-lg-4 col-md-12">
            <h4>Tentang Perusahaan</h4>
            <p class="text-white-50 small">
              Konsultan perencana dan pengawasan terkemuka dalam bidang pengelolaan sumber daya air berkelanjutan di Indonesia sejak 2021.
            </p>
          </div>
        </div>
      </div>

      <div class="container copyright text-center mt-4">
        <p class="mb-0 text-white-50 small">
          Copyright © 2021-2026. <strong class="text-white">PT. Tirta Rekayasa Konsultan</strong>. All Rights Reserved.
        </p>
      </div>
    </footer>
  </div>
</template>

<style scoped>
.layout-wrapper {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.main-content {
  flex-grow: 1;
}

/* Header */
.header {
  background: rgba(255, 255, 255, 0.95);
  backdrop-filter: blur(8px);
  box-shadow: 0 2px 15px rgba(0, 0, 0, 0.06);
  padding: 12px 0;
  transition: all 0.3s ease;
  z-index: 997;
}

.navmenu ul {
  list-style: none;
  margin: 0;
  padding: 0;
  display: flex;
  align-items: center;
  gap: 24px;
}

.navmenu a {
  color: #1e293b;
  font-size: 0.95rem;
  font-weight: 500;
  text-decoration: none;
  transition: color 0.25s ease;
}

.navmenu a:hover,
.navmenu a.router-link-exact-active {
  color: #0284c7;
  font-weight: 600;
}

.btn-nav-login {
  background-color: #0284c7;
  color: #ffffff !important;
  padding: 8px 22px;
  border-radius: 50px;
  font-weight: 600;
  transition: all 0.25s ease;
}

.btn-nav-login:hover {
  background-color: #0369a1;
  color: #ffffff !important;
}

.dropdown-item-user {
  display: flex;
  align-items: center;
  gap: 12px;
}

.user-greeting {
  font-size: 0.9rem;
  color: #475569;
  font-weight: 600;
}

.btn-nav-logout {
  background: transparent;
  border: 1px solid #cbd5e1;
  color: #ef4444;
  padding: 4px 14px;
  border-radius: 50px;
  font-size: 0.85rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
}

.btn-nav-logout:hover {
  background: #fee2e2;
  border-color: #ef4444;
}

.mobile-nav-toggle {
  cursor: pointer;
  font-size: 1.7rem;
  line-height: 1;
  color: #0f172a;
  padding: 4px;
}

/* Footer */
.footer {
  background: #0f172a;
  color: #ffffff;
  padding: 60px 0 25px 0;
  margin-top: auto;
}

.footer h4 {
  color: #ffffff;
  font-size: 1.15rem;
  font-weight: 700;
  margin-bottom: 20px;
}

.footer-about .sitename {
  font-size: 1.4rem;
  font-weight: 700;
  color: #ffffff;
}

.copyright {
  border-top: 1px solid rgba(255, 255, 255, 0.1);
  padding-top: 25px;
}

@media (max-width: 575.98px) {
  .footer .col-lg-5,
  .footer .col-lg-3,
  .footer .col-lg-4,
  .footer .col-md-6,
  .footer .col-md-12 {
    flex: 0 0 100%;
    max-width: 100%;
  }
}

@media (max-width: 991.98px) {
  .navmenu ul {
    display: none;
  }

  .navmenu.mobile-nav-active ul {
    display: flex;
    flex-direction: column;
    position: fixed;
    top: 70px;
    left: 15px;
    right: 15px;
    background: #ffffff;
    border-radius: 12px;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.15);
    padding: 20px;
    gap: 16px;
    align-items: flex-start;
  }
}
</style>


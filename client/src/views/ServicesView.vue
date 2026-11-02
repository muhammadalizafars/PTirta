<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import MainLayout from '@/layouts/MainLayout.vue'
import { apiClient } from '@/api/client'
import type { ServiceItem } from '@/types'

const activeCategory = ref<string>('all')
const isLoading = ref<boolean>(false)
const errorMessage = ref<string | null>(null)
const services = ref<ServiceItem[]>([])

const filterOptions = [
  { label: 'Semua Layanan', value: 'all' },
  { label: 'Perencanaan & Desain', value: 'perencanaan' },
  { label: 'Kajian & Dokumen', value: 'kajian' },
  { label: 'Operasi & Pemeliharaan', value: 'op' }
]

const filteredServices = computed(() => {
  if (activeCategory.value === 'all') {
    return services.value
  }
  return services.value.filter(item => item.category === activeCategory.value)
})

const fetchServices = async () => {
  try {
    isLoading.value = true
    errorMessage.value = null
    const response = await apiClient.get<ServiceItem[]>('/services')
    services.value = response.data
  } catch (err: any) {
    errorMessage.value = err.response?.data?.message || 'Gagal memuat katalog layanan dari server.'
  } finally {
    isLoading.value = false
  }
}

onMounted(() => {
  fetchServices()
})
</script>

<template>
  <MainLayout>
    <div class="service-page-wrapper">
      <!-- Header Banner -->
      <div class="page-header-banner">
        <div class="container">
          <span class="page-header-badge"><i class="bi bi-briefcase-fill me-1"></i> Layanan Kami</span>
          <h1 class="page-header-title">Solusi Rekayasa Sumber Daya Air Terpadu</h1>
          <p class="page-header-desc">
            Memberikan jasa konsultansi profesional dengan standar mutu terbaik, didukung tim konsultan ahli berpengalaman dan berdedikasi tinggi.
          </p>
          <ul class="breadcrumb-custom">
            <li><router-link to="/">Beranda</router-link></li>
            <li class="active">Layanan</li>
          </ul>
        </div>
      </div>

      <!-- Main Section -->
      <section class="services-main-section">
        <div class="container">
          <!-- Filter Buttons -->
          <div class="service-filter-wrapper">
            <div class="filter-btn-group">
              <button
                v-for="opt in filterOptions"
                :key="opt.value"
                type="button"
                class="filter-btn"
                :class="{ active: activeCategory === opt.value }"
                @click="activeCategory = opt.value"
              >
                {{ opt.label }}
              </button>
            </div>
          </div>

          <!-- Loading State -->
          <div v-if="isLoading" class="text-center py-5">
            <div class="spinner-border text-primary" role="status">
              <span class="visually-hidden">Memuat data...</span>
            </div>
            <p class="mt-2 text-muted">Memuat katalog layanan dari server...</p>
          </div>

          <!-- Error State -->
          <div v-else-if="errorMessage" class="alert alert-danger text-center my-4">
            <i class="bi bi-exclamation-triangle-fill me-2"></i> {{ errorMessage }}
            <button class="btn btn-sm btn-outline-danger ms-3" @click="fetchServices">Coba Lagi</button>
          </div>

          <!-- Services Grid (v-for) -->
          <div v-else class="row g-4">
            <div
              v-for="service in filteredServices"
              :key="service.id"
              class="col-md-6 col-lg-4"
            >
              <div class="service-card-item">
                <div class="service-item-img">
                  <img :src="service.imageUrl" :alt="service.title" loading="lazy" />
                  <span class="service-cat-pill">{{ service.categoryLabel }}</span>
                </div>
                <div class="service-item-body">
                  <div class="service-icon-box">
                    <i :class="service.iconClass"></i>
                  </div>
                  <h3 class="service-title-text">{{ service.title }}</h3>
                  <p class="service-desc-text">{{ service.description }}</p>
                </div>
              </div>
            </div>

            <div v-if="filteredServices.length === 0" class="col-12 text-center py-5">
              <p class="text-muted">Tidak ada layanan pada kategori ini.</p>
            </div>
          </div>

          <!-- CTA Banner -->
          <div class="cta-consult-box">
            <div class="row align-items-center g-4">
              <div class="col-lg-8">
                <h3>Butuh Konsultasi untuk Proyek Anda?</h3>
                <p>
                  Tim ahli PT. Tirta Rekayasa Konsultan siap berdiskusi dan memberikan solusi teknis terbaik yang akurat, efektif, serta efisien.
                </p>
              </div>
              <div class="col-lg-4 text-lg-end">
                <a href="mailto:tirtarekayasakonsultanbdg@gmail.com" class="btn-cta-contact">
                  <i class="bi bi-envelope-fill"></i> Hubungi Konsultan
                </a>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  </MainLayout>
</template>

<style scoped>
.page-header-banner {
  background: linear-gradient(135deg, rgba(15, 23, 42, 0.92) 0%, rgba(2, 132, 199, 0.88) 100%), 
              url('/assets/img/hero-carousel/hero-carousel-1.jpg') center/cover no-repeat;
  padding: 140px 0 70px 0;
  color: #ffffff;
  text-align: center;
  position: relative;
}

.page-header-badge {
  display: inline-block;
  padding: 6px 18px;
  font-size: 0.85rem;
  font-weight: 600;
  letter-spacing: 1.5px;
  text-transform: uppercase;
  border-radius: 50px;
  background: rgba(255, 255, 255, 0.15);
  backdrop-filter: blur(8px);
  border: 1px solid rgba(255, 255, 255, 0.25);
  color: #ffffff;
  margin-bottom: 16px;
}

.page-header-title {
  font-size: 2.6rem;
  font-weight: 800;
  margin-bottom: 15px;
}

.page-header-desc {
  font-size: 1.1rem;
  color: rgba(255, 255, 255, 0.85);
  max-width: 680px;
  margin: 0 auto 20px auto;
  line-height: 1.6;
}

.breadcrumb-custom {
  display: inline-flex;
  padding: 8px 20px;
  border-radius: 50px;
  background: rgba(0, 0, 0, 0.2);
  list-style: none;
  margin: 0;
  font-size: 0.9rem;
}

.breadcrumb-custom li a {
  color: rgba(255, 255, 255, 0.8);
  text-decoration: none;
}

.breadcrumb-custom li.active {
  color: #38bdf8;
}

.breadcrumb-custom li + li::before {
  content: "/";
  padding: 0 10px;
  color: rgba(255, 255, 255, 0.5);
}

.service-filter-wrapper {
  margin-top: -30px;
  position: relative;
  z-index: 10;
  margin-bottom: 50px;
}

.filter-btn-group {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 10px;
  background: #ffffff;
  padding: 12px;
  border-radius: 50px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
  border: 1px solid #e2e8f0;
  max-width: 820px;
  margin: 0 auto;
}

.filter-btn {
  border: none;
  background: transparent;
  color: #64748b;
  padding: 8px 20px;
  border-radius: 50px;
  font-size: 0.92rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.25s ease;
}

.filter-btn.active {
  background: #0284c7;
  color: #ffffff;
}

.services-main-section {
  padding: 30px 0 80px 0;
  background-color: #f8fafc;
}

.service-card-item {
  background: #ffffff;
  border-radius: 16px;
  border: 1px solid #e2e8f0;
  overflow: hidden;
  box-shadow: 0 6px 18px rgba(0, 0, 0, 0.04);
  transition: all 0.35s ease;
  height: 100%;
  display: flex;
  flex-direction: column;
}

.service-card-item:hover {
  transform: translateY(-8px);
  box-shadow: 0 16px 32px rgba(2, 132, 199, 0.12);
  border-color: rgba(2, 132, 199, 0.35);
}

.service-item-img {
  position: relative;
  height: 190px;
  overflow: hidden;
}

.service-item-img img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform 0.5s ease;
}

.service-card-item:hover .service-item-img img {
  transform: scale(1.08);
}

.service-cat-pill {
  position: absolute;
  bottom: 12px;
  left: 12px;
  background: rgba(15, 23, 42, 0.78);
  color: #ffffff;
  font-size: 0.75rem;
  font-weight: 600;
  padding: 4px 10px;
  border-radius: 50px;
}

.service-item-body {
  padding: 24px;
  display: flex;
  flex-direction: column;
  flex-grow: 1;
}

.service-icon-box {
  width: 48px;
  height: 48px;
  min-width: 48px;
  border-radius: 12px;
  background: rgba(2, 132, 199, 0.1);
  color: #0284c7;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 1.35rem;
  margin-bottom: 16px;
}

.service-title-text {
  font-size: 1.15rem;
  font-weight: 700;
  color: #0f172a;
  margin-bottom: 10px;
}

.service-desc-text {
  font-size: 0.92rem;
  color: #64748b;
  line-height: 1.6;
  flex-grow: 1;
}

.cta-consult-box {
  background: linear-gradient(135deg, #0f172a 0%, #0369a1 100%);
  border-radius: 20px;
  padding: 50px 40px;
  color: #ffffff;
  margin-top: 60px;
}

.cta-consult-box h3 {
  color: #ffffff;
  font-size: 1.9rem;
  font-weight: 800;
  margin-bottom: 12px;
}

.btn-cta-contact {
  background: #ffffff;
  color: #0284c7;
  padding: 12px 30px;
  border-radius: 50px;
  font-weight: 700;
  text-decoration: none;
  display: inline-flex;
  align-items: center;
  gap: 8px;
}
</style>


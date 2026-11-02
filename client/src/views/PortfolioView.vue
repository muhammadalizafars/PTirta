<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import MainLayout from '@/layouts/MainLayout.vue'
import { apiClient } from '@/api/client'
import type { PortfolioItem } from '@/types'

const activeCategory = ref<string>('all')
const isLoading = ref<boolean>(false)
const errorMessage = ref<string | null>(null)
const portfolios = ref<PortfolioItem[]>([])

const filterOptions = [
  { label: 'Semua Pekerjaan', value: 'all' },
  { label: 'Pengendalian Banjir', value: 'banjir' },
  { label: 'Perencanaan SDA', value: 'sda' },
  { label: 'Kajian & SIPPA', value: 'kajian' },
  { label: 'Drainase & Irigasi', value: 'irigasi' }
]

const filteredPortfolios = computed(() => {
  if (activeCategory.value === 'all') {
    return portfolios.value
  }
  return portfolios.value.filter(p => p.category === activeCategory.value)
})

const fetchPortfolios = async () => {
  try {
    isLoading.value = true
    errorMessage.value = null
    const response = await apiClient.get<PortfolioItem[]>('/portfolios')
    portfolios.value = response.data
  } catch (err: any) {
    errorMessage.value = err.response?.data?.message || 'Gagal memuat portofolio pekerjaan dari server.'
  } finally {
    isLoading.value = false
  }
}

onMounted(() => {
  fetchPortfolios()
})
</script>

<template>
  <MainLayout>
    <div class="porto-page-wrapper">
      <!-- Header Banner -->
      <div class="page-header-banner">
        <div class="container">
          <span class="page-header-badge"><i class="bi bi-collection-fill me-1"></i> Rekam Jejak</span>
          <h1 class="page-header-title">Portofolio & Pengalaman Pekerjaan</h1>
          <p class="page-header-desc">
            Kumpulan proyek perencanaan, kajian hidrologi, dan pengawasan teknik sumber daya air yang telah kami selesaikan dengan mutu prima.
          </p>
          <ul class="breadcrumb-custom">
            <li><router-link to="/">Beranda</router-link></li>
            <li class="active">Portofolio</li>
          </ul>
        </div>
      </div>

      <!-- Main Section -->
      <section class="porto-main-section">
        <div class="container">
          <!-- Filter Tabs -->
          <div class="porto-filter-wrapper">
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

          <!-- Loading -->
          <div v-if="isLoading" class="text-center py-5">
            <div class="spinner-border text-primary" role="status">
              <span class="visually-hidden">Memuat data...</span>
            </div>
            <p class="mt-2 text-muted">Memuat proyek dari server...</p>
          </div>

          <!-- Error -->
          <div v-else-if="errorMessage" class="alert alert-danger text-center my-4">
            <i class="bi bi-exclamation-triangle-fill me-2"></i> {{ errorMessage }}
            <button class="btn btn-sm btn-outline-danger ms-3" @click="fetchPortfolios">Coba Lagi</button>
          </div>

          <!-- Grid (v-for) -->
          <div v-else class="row g-4">
            <div
              v-for="project in filteredPortfolios"
              :key="project.id"
              class="col-md-6 col-lg-4"
            >
              <div class="project-card">
                <div class="project-img-wrapper">
                  <img :src="project.imageUrl" :alt="project.title" loading="lazy" />
                  <span class="project-badge-tag">{{ project.categoryLabel }}</span>
                </div>
                <div class="project-card-body">
                  <div class="project-meta-list">
                    <span class="project-meta-item"><i class="bi bi-geo-alt-fill"></i> {{ project.location }}</span>
                    <span class="project-meta-item"><i class="bi bi-calendar3"></i> {{ project.year }}</span>
                    <span class="project-meta-item"><i class="bi bi-check-circle-fill"></i> {{ project.status }}</span>
                  </div>
                  <h3 class="project-card-title">{{ project.title }}</h3>
                  <p class="project-desc-text">{{ project.description }}</p>
                </div>
              </div>
            </div>

            <div v-if="filteredPortfolios.length === 0" class="col-12 text-center py-5">
              <p class="text-muted">Tidak ada proyek dalam kategori ini.</p>
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
              url('/assets/img/hero-carousel/hero-carousel-2.jpg') center/cover no-repeat;
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

.porto-filter-wrapper {
  margin-top: -30px;
  position: relative;
  z-index: 10;
  margin-bottom: 45px;
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
  max-width: 860px;
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

.porto-main-section {
  padding: 30px 0 80px 0;
  background-color: #f8fafc;
}

.project-card {
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

.project-card:hover {
  transform: translateY(-8px);
  box-shadow: 0 16px 32px rgba(2, 132, 199, 0.12);
  border-color: rgba(2, 132, 199, 0.35);
}

.project-img-wrapper {
  position: relative;
  height: 230px;
  overflow: hidden;
}

.project-img-wrapper img {
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform 0.5s ease;
}

.project-card:hover .project-img-wrapper img {
  transform: scale(1.08);
}

.project-badge-tag {
  position: absolute;
  top: 14px;
  left: 14px;
  background: rgba(2, 132, 199, 0.92);
  color: #ffffff;
  font-size: 0.78rem;
  font-weight: 600;
  padding: 4px 12px;
  border-radius: 50px;
}

.project-card-body {
  padding: 24px;
  display: flex;
  flex-direction: column;
  flex-grow: 1;
}

.project-card-title {
  font-size: 1.22rem;
  font-weight: 700;
  color: #0f172a;
  margin-bottom: 12px;
}

.project-meta-list {
  display: flex;
  flex-wrap: wrap;
  gap: 12px;
  margin-bottom: 15px;
  padding-bottom: 15px;
  border-bottom: 1px solid #e2e8f0;
  font-size: 0.84rem;
  color: #64748b;
}

.project-meta-item {
  display: inline-flex;
  align-items: center;
  gap: 5px;
}

.project-meta-item i {
  color: #0284c7;
}

.project-desc-text {
  font-size: 0.92rem;
  color: #64748b;
  line-height: 1.6;
  margin-bottom: 0;
  flex-grow: 1;
}
</style>


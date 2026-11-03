<script setup lang="ts">
import MainLayout from '@/layouts/MainLayout.vue'
</script>

<template>
  <MainLayout>
    <!-- Hero Section -->
    <div class="hero-carousel-wrapper">
      <div id="heroCarousel" class="carousel slide carousel-fade" data-bs-ride="carousel" data-bs-interval="5000">
        <div class="carousel-indicators">
          <button type="button" data-bs-target="#heroCarousel" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
          <button type="button" data-bs-target="#heroCarousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
          <button type="button" data-bs-target="#heroCarousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
        </div>

        <div class="carousel-inner">
          <div class="carousel-item active">
            <img src="/assets/img/hero-carousel/hero-carousel-1.jpg" alt="Hero Image 1" />
            <div class="hero-overlay">
              <div class="hero-content">
                <span class="hero-badge"><i class="bi bi-droplet-half me-1"></i> Rekayasa Sumber Daya Air</span>
                <h1>Solusi Terpadu Rekayasa Sumber Daya Air Berkelanjutan</h1>
                <p>Konsultan profesional dan berpengalaman dalam perencanaan, review desain, dan pengawasan teknis pekerjaan sumber daya air di seluruh wilayah Indonesia.</p>
                <div class="d-flex flex-wrap justify-content-center gap-3">
                  <router-link to="/layanan" class="hero-btn-primary">Lihat Layanan <i class="bi bi-arrow-right"></i></router-link>
                  <a href="#tentang" class="hero-btn-outline">Tentang Kami</a>
                </div>
              </div>
            </div>
          </div>

          <div class="carousel-item">
            <img src="/assets/img/hero-carousel/hero-carousel-2.jpg" alt="Hero Image 2" />
            <div class="hero-overlay">
              <div class="hero-content">
                <span class="hero-badge"><i class="bi bi-shield-check me-1"></i> Presisi & Keandalan</span>
                <h1>Analisa Hidrologi & Sistem Peringatan Dini Banjir</h1>
                <p>Mengintegrasikan kajian hidrologi komprehensif dengan perencanaan sistem peringatan dini (FEWS) untuk perlindungan infrastruktur dan masyarakat.</p>
                <div class="d-flex flex-wrap justify-content-center gap-3">
                  <router-link to="/layanan" class="hero-btn-primary">Katalog Layanan <i class="bi bi-arrow-right"></i></router-link>
                  <router-link to="/portofolio" class="hero-btn-outline">Portofolio Pekerjaan</router-link>
                </div>
              </div>
            </div>
          </div>

          <div class="carousel-item">
            <img src="/assets/img/hero-carousel/hero-carousel-3.jpg" alt="Hero Image 3" />
            <div class="hero-overlay">
              <div class="hero-content">
                <span class="hero-badge"><i class="bi bi-award me-1"></i> Mutu & Dedikasi</span>
                <h1>Komitmen Mutu dan Ketepatan Waktu</h1>
                <p>Didukung oleh tim tenaga ahli bersertifikat dan berdedikasi tinggi demi memberikan solusi bernilai tambah bagi setiap pelanggan.</p>
                <div class="d-flex flex-wrap justify-content-center gap-3">
                  <router-link to="/portofolio" class="hero-btn-primary">Jelajahi Proyek <i class="bi bi-arrow-right"></i></router-link>
                  <a href="#tentang" class="hero-btn-outline">Profil Perusahaan</a>
                </div>
              </div>
            </div>
          </div>
        </div>

        <button class="carousel-control-prev" type="button" data-bs-target="#heroCarousel" data-bs-slide="prev">
          <span class="carousel-control-prev-icon" aria-hidden="true"></span>
          <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#heroCarousel" data-bs-slide="next">
          <span class="carousel-control-next-icon" aria-hidden="true"></span>
          <span class="visually-hidden">Next</span>
        </button>
      </div>
    </div>

    <!-- Stats Card -->
    <div class="container stats-card-container">
      <div class="stats-card">
        <div class="row g-3">
          <div class="col-lg-3 col-sm-6">
            <div class="stat-item">
              <div class="stat-icon"><i class="bi bi-calendar-check"></i></div>
              <div class="stat-info">
                <h4>2021</h4>
                <p>Tahun Berdiri</p>
              </div>
            </div>
          </div>
          <div class="col-lg-3 col-sm-6">
            <div class="stat-item">
              <div class="stat-icon"><i class="bi bi-water"></i></div>
              <div class="stat-info">
                <h4>SDA & Keairan</h4>
                <p>Fokus & Spesialisasi</p>
              </div>
            </div>
          </div>
          <div class="col-lg-3 col-sm-6">
            <div class="stat-item">
              <div class="stat-icon"><i class="bi bi-people-fill"></i></div>
              <div class="stat-info">
                <h4>Tenaga Ahli</h4>
                <p>Profesional & Berlisensi</p>
              </div>
            </div>
          </div>
          <div class="col-lg-3 col-sm-6">
            <div class="stat-item">
              <div class="stat-icon"><i class="bi bi-patch-check-fill"></i></div>
              <div class="stat-info">
                <h4>Presisi & Mutu</h4>
                <p>Standar Kualitas Terbaik</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- About Section -->
    <section id="tentang" class="landing-section">
      <div class="container">
        <div class="row align-items-center g-5">
          <div class="col-lg-6">
            <div class="about-img-box">
              <img src="/assets/img/about.jpg" alt="Tentang PT Tirta" />
              <div class="about-floating-badge">
                <h5>PT. Tirta Rekayasa Konsultan</h5>
                <span>Mitra Rekayasa Terpercaya</span>
              </div>
            </div>
          </div>
          <div class="col-lg-6">
            <span class="section-badge">Tentang Kami</span>
            <h2 class="section-title">Konsultan Perencanaan & Pengawasan Bidang Sumber Daya Air</h2>
            <p class="text-secondary" style="line-height: 1.7; font-size: 1.05rem;">
              <strong>PT. Tirta Rekayasa Konsultan</strong> berdiri sejak tahun 2021 dan bergerak aktif dalam bidang konsultan perencanaan serta pengawasan teknik sumber daya air. Kami didukung oleh tenaga ahli profesional yang berpengalaman dalam merencanakan, mereview, dan mengawasi pekerjaan keairan strategis.
            </p>
            <ul class="feature-check-list">
              <li>
                <i class="bi bi-check-circle-fill"></i>
                <span>Didukung konsultan spesialis yang berdedikasi tinggi dan bersertifikasi keahlian.</span>
              </li>
              <li>
                <i class="bi bi-check-circle-fill"></i>
                <span>Metodologi komprehensif, berbasis data lapangan akurat dan pemodelan mutakhir.</span>
              </li>
              <li>
                <i class="bi bi-check-circle-fill"></i>
                <span>Komitmen penuh terhadap mutu hasil pekerjaan, kepuasan mitra, dan ketepatan waktu.</span>
              </li>
            </ul>
            <div class="mt-4 pt-2">
              <router-link to="/layanan" class="btn-action-primary">
                Pelajari Layanan Kami <i class="bi bi-arrow-right"></i>
              </router-link>
            </div>
          </div>
        </div>
      </div>
    </section>
  </MainLayout>
</template>

<style scoped>
.landing-section {
  padding: 80px 0;
  position: relative;
}

.section-badge {
  display: inline-block;
  padding: 6px 16px;
  font-size: 0.85rem;
  font-weight: 600;
  letter-spacing: 1px;
  text-transform: uppercase;
  border-radius: 50px;
  background-color: rgba(2, 132, 199, 0.1);
  color: #0284c7;
  margin-bottom: 12px;
}

.section-title {
  font-size: 2.2rem;
  font-weight: 700;
  color: #0f172a;
  margin-bottom: 15px;
  line-height: 1.3;
}

/* Hero Carousel */
.hero-carousel-wrapper {
  position: relative;
  overflow: hidden;
}

#heroCarousel {
  width: 100%;
  height: 82vh;
  min-height: 580px;
  max-height: 720px;
}

.hero-carousel-wrapper .carousel-inner,
.hero-carousel-wrapper .carousel-item {
  height: 100%;
}

.hero-carousel-wrapper .carousel-item img {
  height: 100%;
  width: 100%;
  object-fit: cover;
}

.hero-overlay {
  position: absolute;
  inset: 0;
  background: linear-gradient(180deg, rgba(15, 23, 42, 0.55) 0%, rgba(15, 23, 42, 0.75) 100%);
  display: flex;
  align-items: center;
  justify-content: center;
}

.hero-content {
  max-width: 850px;
  color: #ffffff;
  padding: 0 20px;
  text-align: center;
  margin-top: 40px;
}

.hero-content .hero-badge {
  display: inline-block;
  background: rgba(255, 255, 255, 0.2);
  backdrop-filter: blur(8px);
  border: 1px solid rgba(255, 255, 255, 0.3);
  color: #ffffff;
  padding: 6px 18px;
  border-radius: 50px;
  font-size: 0.9rem;
  font-weight: 600;
  margin-bottom: 20px;
}

.hero-content h1 {
  font-size: 2.8rem;
  font-weight: 800;
  color: #ffffff;
  line-height: 1.25;
  margin-bottom: 20px;
}

.hero-content p {
  font-size: 1.15rem;
  color: rgba(255, 255, 255, 0.9);
  margin-bottom: 30px;
  line-height: 1.6;
}

.hero-btn-primary {
  background-color: #0284c7;
  color: #ffffff;
  padding: 12px 28px;
  font-weight: 600;
  border-radius: 50px;
  text-decoration: none;
  display: inline-flex;
  align-items: center;
  gap: 8px;
  transition: all 0.3s ease;
}

.hero-btn-primary:hover {
  background-color: #0369a1;
  transform: translateY(-2px);
}

.hero-btn-outline {
  background: rgba(255, 255, 255, 0.12);
  backdrop-filter: blur(6px);
  color: #ffffff;
  padding: 12px 28px;
  font-weight: 600;
  border-radius: 50px;
  border: 2px solid rgba(255, 255, 255, 0.7);
  text-decoration: none;
  display: inline-flex;
  align-items: center;
  gap: 8px;
  transition: all 0.3s ease;
}

.hero-btn-outline:hover {
  background: #ffffff;
  color: #0f172a;
}

/* Stats Card */
.stats-card-container {
  margin-top: -60px;
  position: relative;
  z-index: 10;
  padding: 0 15px;
}

.stats-card {
  background: #ffffff;
  border-radius: 16px;
  box-shadow: 0 15px 35px rgba(0, 0, 0, 0.08);
  border: 1px solid #e2e8f0;
  padding: 30px 20px;
}

.stat-item {
  display: flex;
  align-items: center;
  gap: 16px;
  padding: 10px 15px;
}

.stat-icon {
  width: 52px;
  height: 52px;
  min-width: 52px;
  border-radius: 12px;
  background: rgba(2, 132, 199, 0.1);
  color: #0284c7;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 1.5rem;
}

.stat-info h4 {
  font-size: 1.35rem;
  font-weight: 700;
  color: #0f172a;
  margin: 0;
}

.stat-info p {
  font-size: 0.88rem;
  color: #64748b;
  margin: 0;
}

/* About */
.about-img-box {
  position: relative;
  border-radius: 16px;
  overflow: hidden;
  box-shadow: 0 15px 35px rgba(0, 0, 0, 0.1);
}

.about-img-box img {
  width: 100%;
  min-height: 380px;
  object-fit: cover;
  display: block;
}

.about-floating-badge {
  position: absolute;
  bottom: 25px;
  left: 25px;
  background: rgba(255, 255, 255, 0.95);
  backdrop-filter: blur(6px);
  padding: 15px 22px;
  border-radius: 12px;
  border-left: 4px solid #0284c7;
}

.feature-check-list {
  list-style: none;
  padding: 0;
  margin: 25px 0 0 0;
}

.feature-check-list li {
  display: flex;
  align-items: flex-start;
  gap: 12px;
  margin-bottom: 14px;
  font-size: 1rem;
  color: #334155;
}

.feature-check-list li i {
  color: #0284c7;
  font-size: 1.2rem;
}

.btn-action-primary {
  background-color: #0284c7;
  color: #ffffff;
  font-weight: 600;
  padding: 10px 28px;
  border-radius: 50px;
  display: inline-flex;
  align-items: center;
  gap: 8px;
  text-decoration: none;
}

/* Tablet */
@media (max-width: 991.98px) {
  #heroCarousel {
    height: 70vh;
    min-height: 520px;
    max-height: 640px;
  }

  .hero-content h1 {
    font-size: 2.2rem;
  }

  .section-title {
    font-size: 1.9rem;
  }

  .landing-section {
    padding: 60px 0;
  }
}

/* Mobile */
@media (max-width: 767.98px) {
  #heroCarousel {
    height: 85vh;
    min-height: 560px;
  }

  .hero-content {
    padding: 0 16px;
    margin-top: 20px;
  }

  .hero-content .hero-badge {
    font-size: 0.7rem;
    padding: 4px 12px;
    margin-bottom: 12px;
  }

  .hero-content h1 {
    font-size: 1.4rem;
    line-height: 1.25;
    margin-bottom: 12px;
  }

  .hero-content p {
    font-size: 0.85rem;
    line-height: 1.5;
    margin-bottom: 16px;
  }

  .hero-btn-primary,
  .hero-btn-outline {
    padding: 8px 16px;
    font-size: 0.82rem;
    width: 100%;
    justify-content: center;
    white-space: normal;
  }

  .hero-content .d-flex {
    flex-direction: column !important;
    gap: 8px !important;
    width: 100%;
  }

  .carousel-control-prev,
  .carousel-control-next {
    width: 30px;
    opacity: 0.5;
  }

  .carousel-control-prev-icon,
  .carousel-control-next-icon {
    width: 16px;
    height: 16px;
  }

  .carousel-indicators {
    bottom: 12px;
    margin-bottom: 0;
  }

  .carousel-indicators button {
    width: 8px;
    height: 8px;
    border-radius: 50%;
  }

  .stats-card-container {
    margin-top: -30px;
  }

  .stats-card {
    padding: 20px 12px;
  }

  .stat-item {
    padding: 8px 10px;
  }

  .stat-info h4 {
    font-size: 1.15rem;
  }

  .about-img-box img {
    min-height: 260px;
  }

  .about-floating-badge {
    bottom: 10px;
    left: 10px;
    padding: 10px 14px;
  }

  .about-floating-badge h5 {
    font-size: 0.9rem;
    margin-bottom: 2px;
  }

  .about-floating-badge span {
    font-size: 0.74rem;
  }

  .feature-check-list li {
    font-size: 0.9rem;
    margin-bottom: 10px;
  }

  .feature-check-list li i {
    font-size: 1.1rem;
    margin-top: 3px;
  }
}

/* Small mobile */
@media (max-width: 575.98px) {
  #heroCarousel {
    height: 100vh;
    min-height: 560px;
  }

  .hero-content h1 {
    font-size: 1.3rem;
  }

  .section-title {
    font-size: 1.55rem;
  }

  .landing-section {
    padding: 45px 0;
  }

  .stats-card-container {
    margin-top: -35px;
  }

  .stat-item {
    flex-direction: column;
    text-align: center;
    padding: 12px 8px;
  }

  .stat-icon {
    margin-bottom: 8px;
  }

  .stat-info h4 {
    font-size: 1rem;
  }

  .stat-info p {
    font-size: 0.8rem;
  }
}
</style>


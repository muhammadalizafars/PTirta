import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import ServicesView from '@/views/ServicesView.vue'
import PortfolioView from '@/views/PortfolioView.vue'
import LoginView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
    meta: { title: 'Beranda - PT. Tirta Rekayasa Konsultan' }
  },
  {
    path: '/layanan',
    name: 'services',
    component: ServicesView,
    meta: { title: 'Layanan - PT. Tirta Rekayasa Konsultan' }
  },
  {
    path: '/portofolio',
    name: 'portfolio',
    component: PortfolioView,
    meta: { title: 'Portofolio - PT. Tirta Rekayasa Konsultan' }
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView,
    meta: { title: 'Masuk - PT. Tirta Rekayasa Konsultan' }
  },
  {
    path: '/register',
    name: 'register',
    component: RegisterView,
    meta: { title: 'Daftar Akun - PT. Tirta Rekayasa Konsultan' }
  },
  {
    path: '/:pathMatch(.*)*',
    redirect: '/'
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior() {
    return { top: 0 }
  }
})

router.beforeEach((to, _from, next) => {
  if (to.meta.title) {
    document.title = to.meta.title as string
  }
  next()
})

export default router


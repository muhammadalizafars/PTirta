<script setup lang="ts">
// AuthLayout provides an isolated canvas for Login, Register, Forgot Password
</script>

<template>
  <div class="auth-layout-container">
    <slot />
  </div>
</template>

<style scoped>
.auth-layout-container {
  min-height: 100vh;
  width: 100%;
}
</style>


<script setup lang="ts">
// Root Component
</script>

<template>
  <router-view />
</template>

<style>
/* Global site styles */
:root {
  --primary-blue: #0284c7;
  --secondary-dark: #0f172a;
}

body {
  font-family: 'Poppins', 'Open Sans', sans-serif;
  color: #334155;
  margin: 0;
  padding: 0;
  background-color: #ffffff;
}

/* Mobile-first touch optimization */
button, a, input[type="button"], input[type="submit"] {
  -webkit-tap-highlight-color: transparent;
  touch-action: manipulation;
}

/* Smooth scrolling */
html {
  scroll-behavior: smooth;
}

/* Tablet optimization */
@media (max-width: 991.98px) {
  body {
    font-size: 15px;
  }
}

/* Mobile optimization */
@media (max-width: 767.98px) {
  body {
    font-size: 14px;
  }

  button, a {
    min-height: 44px;
    min-width: 44px;
    padding: 8px 12px;
  }

  input, textarea, select {
    font-size: 16px !important;
  }
}

/* Small mobile */
@media (max-width: 575.98px) {
  body {
    font-size: 13px;
  }
}
</style>


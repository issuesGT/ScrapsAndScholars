<script setup>
  import { RouterLink, RouterView } from 'vue-router'
  import navMenu from './components/navMenu.vue'
</script>

<!--Router view is all new templates we switch between-->
<template>
  <header>
    <navMenu />
  </header>
  <RouterView />
  <footer>
    <!--This footer will look the same on all pages.-->
  </footer>
</template>

<style scoped>
</style>

<template>
  <HeaderComp />
  <MainComp />
</template>

<script >
import HeaderComp from '@/views/HeaderComp.vue'
import MainComp from './views/MainComp.vue'

export default {
  components: { HeaderComp, MainComp }
}
</script>
<style>
@import "@/assets/css/reset.css";
@import "@/assets/css/main.css";
</style>

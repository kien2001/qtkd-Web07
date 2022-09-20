<template>
  <HeaderComp />
  <MainComp />
</template>

<script >
import HeaderComp from '@/views/components/TheHeaderComp.vue'
import MainComp from './views/components/TheMainComp.vue'

export default {
  components: { HeaderComp, MainComp },
  watch: {
    $route: {
     
      handler(to, from) {
        document.title = to.meta.title || 'AMIS Bán Hàng';
      },
       immediate: true
    },
  }
}
</script>
<style>
@import "@/assets/css/reset.css";
@import "@/assets/css/main.css";
</style>

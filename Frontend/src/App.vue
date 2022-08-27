<template>
  <HeaderComp />
  <!-- <FunctionTool /> -->
  <!-- <SideBarLeft /> -->
  <MainComp />
  <!-- <SideBarRight /> -->
</template>

<script >
// @ is an alias to /src
import HeaderComp from '@/views/HeaderComp.vue'
import MainComp from './views/MainComp.vue'
// import SideBarLeft from './views/SideBarLeft.vue'
// import SideBarRight from './views/SideBarRight.vue'
// import FunctionTool from './views/FunctionTool.vue'

export default {
  components: { HeaderComp, MainComp }
}
</script>
<style>
@import "@/assets/css/reset.css";
@import "@/assets/css/main.css";
</style>

<template>
  <div class="main">
    <FunctionTool />
    <SideBarLeft v-if="this.$route.name === 'TiemNang'" />
    <MainContent />
    <SideBarRight v-if="this.$route.name === 'TiemNang'" />
    <ToastMessage :message="getMessage" :state="getState" :isShow="getIsShow" ref="toast" />
  </div>
</template>
<style scoped>


.main {
  display: grid;
  grid-template-columns: 240px auto 320px;
  grid-template-rows: 54px calc(100% - 54px);
  grid-template-areas:
    "function function function "
    "sideBarLeft mainContent sideBarRight";
  transition: all 2s linear;
}
.sidebar-left {
  grid-area: sideBarLeft;
}

.function {
  grid-area: function;
}

.sidebar-right {
  grid-area: sideBarRight;
}
.main-content{
  grid-area : mainContent;
}
</style>
<script>
import SideBarLeft from '@/views/SideBarLeft.vue'
import FunctionTool from '@/views/FunctionTool.vue'
import MainContent from '@/views/MainContent.vue'
import SideBarRight from '@/views/SideBarRight.vue'
export default {
  name: 'MainComp',
  components: { SideBarLeft, SideBarRight, FunctionTool, MainContent },
  computed:{
    getMessage(){
      return this.$store.state.message
    },
    getState(){
      return this.$store.state.state
    },
    getIsShow(){
      return this.$store.state.isShow
    },
  }
}
</script>

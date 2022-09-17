<template>
  <div class="main">
    <FunctionTool />
    <SideBarLeft v-if="this.$route.name === 'Potential'" />
    <MainContent />
    <SideBarRight v-if="this.$route.name === 'Potential'" />
    <TheToastMessage
      :message="getMessage"
      :state="getState"
      ref="toast"
    />
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
.main-content {
  grid-area: mainContent;
}
</style>
<script>
import SideBarLeft from "@/views/TheSideBarLeft.vue";
import FunctionTool from "@/views/TheFunctionTool.vue";
import MainContent from "@/views/TheMainContent.vue";
import SideBarRight from "@/views/TheSideBarRight.vue";
import emitter from "@/js/emitter";
export default {
  name: "MainComp",
  components: { SideBarLeft, SideBarRight, FunctionTool, MainContent },
  computed: {
    getMessage() {
      return this.$store.state.message;
    },
    getState() {
      return this.$store.state.state;
    },
  },
  mounted(){
    emitter.on("showToast", this.$refs.toast.showAnimateToast);
  }
};
</script>

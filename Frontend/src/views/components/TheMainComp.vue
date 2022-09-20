<template>
  <div class="main">
    <TheFunctionTool />
    <TheSideBarLeft v-if="this.$route.name === 'Potential'" />
    <TheMainContent />
    <TheSideBarRight v-if="this.$route.name === 'Potential'" />
    <BaseToastMessage :message="getMessage" :state="getState" ref="toast" />
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
import TheSideBarLeft from "@/views/components/TheSideBarLeft.vue";
import TheSideBarRight from "@/views/components/TheSideBarRight.vue";
import TheFunctionTool from "@/views/components/TheFunctionTool.vue";
import TheMainContent from "@/views/components/TheMainContent.vue";
import emitter from "@/js/emitter";
export default {
  name: "MainComp",
  components: {
    TheSideBarLeft,
    TheSideBarRight,
    TheFunctionTool,
    TheMainContent,
  },
  computed: {
    getMessage() {
      return this.$store.state.message;
    },
    getState() {
      return this.$store.state.state;
    },
  },
  mounted() {
    emitter.on("showToast", this.$refs.toast.showAnimateToast);
  },
};
</script>

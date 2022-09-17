<template>
  <div class="main-content">
    <!-- <router-view/> -->
    <router-view v-slot="{ Component }">
      <transition name= 'fade' mode="out-in">
        <component :is="Component" />
      </transition>
    </router-view>
  </div>
</template>
<script>
export default {
  name: "MainContent",
};
</script>
<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>

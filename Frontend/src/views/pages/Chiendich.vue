<template>
  <h1>
    Chức năng đang thi công, vui lòng quay lại sau!!! 😘
  </h1>
</template>
<script>
  import emitter from '@/js/emitter';

export default {
  name: 'ChienDich',
  mounted() {
    this.$store.commit("setState", "fail")
    this.$store.commit("setMessage", "Chức năng đang thi công")
    emitter.emit("showToast");
  }
}
</script>

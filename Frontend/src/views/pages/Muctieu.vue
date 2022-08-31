<template>
    <h1>
        Chức năng đang thi công, vui lòng quay lại sau!!! 😘
    </h1>
</template>
<script>
export default {
  name: 'MucTieu',
  mounted(){
    this.$store.commit("setState", "fail")
    this.$store.commit("setMessage", "Chức năng đang thi công")
    this.$store.commit("setIsShow", true)
  }
}
</script>

<template>
    <div class="loading-container" ref="loading" :style="{position: position}">
        <div class="loading"></div>
    </div>
</template>
<style scoped>
.loading-container {
    top:0;
    bottom: 0;
    right: 0;
    left:0;
    background-color: rgba(51, 51, 51, 0.497);
    width:100%;
    height:100%;
    display:flex;
    justify-content: center;
    align-items: center;
    z-index: 1003;
}
.loading{
    background-image: url('../assets/img/loadding-icon.svg');
    background-size: cover;
    background-repeat: no-repeat;
    width: 100px;
    height: 100px;
}
</style>
<script>
export default {
    name:"BaseLoading",
    props:["position"]
}
</script>
<template>
  <div class="toast" :style="objectStyle" ref="toast">
    <div class="toast-icon" :class="state"></div>
    <div class="toast-text">{{ message }}</div>
    <div class="toast-icon toast-icon-close" @click="closeToast"></div>
  </div>
</template>
<script>
export default {
    name: "TheToastMessage",
    data(){
        return{
            animateToast: null,
            delay:0
        }
    },
    computed: {
        objectStyle() {
            return {
                backgroundColor: `var(--${this.state})`
            }
        }
    },
    methods: {
        closeToast() {
            this.animateToast.cancel()
        },
        showAnimateToast(){
            const toastAnimate = [
                 { right: '-30%' },
                 { right: '10px' },
                 { right: '10px' },
                 { right: '10px' },
                 { right: '10px' },
                 { right: '10px' },
                 { right: '10px' },
                 { right: '-30%' },
            ]
            const toastTiming = {
                duration: 3000,
                iterations: 1,
                delay: this.delay || 0,
                easing: "cubic-bezier(0.42, 0, 0.58, 1)"
            }
            // this.$refs.toast.animate()
            this.animateToast = this.$refs.toast.animate(toastAnimate, toastTiming);
        }
    },
    props: ["message", "state"]
}
</script>
<style scoped>
.toast {
  --success: #31b491;
  --fail: #ec4141;
  --warning: #f07d42;
  --info: #4262f0;
  position: fixed;
  right: -100%;
  top: 10%;
  background-color: var(--success);
  justify-content: space-between;
  min-width: 161px;
  height: 40px;
  border-radius: 4px;
  box-shadow: 0 3px 6px #0000002b;
  padding: 12px 12px 12px 16px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  z-index: 1200;
  gap: 10px;
  margin-bottom: 20px;
}

/* .toast.show {
    animation: moveToast 3s ease-in-out;
} */

.toast + .toast {
  margin-top: 8px;
}

.toast-icon {
  background-repeat: no-repeat;
  background-size: cover;
  background-position: center;
  width: 16px;
  height: 16px;
}

.success {
  background-image: url("../assets/img/tich-tron-green.svg");
}

.fail {
  background-image: url("../assets/img/ractangle-close-white.svg");
}

.toast-text {
  max-width: 100px;
  color: #ffffff;
  font-weight: 500;
  font-size: 13px;
  line-height: 16px;
  letter-spacing: 0;
}

.toast-icon-close {
  background-image: url("../assets/img/close-white.svg");
  cursor: pointer;
  width: 12px;
  height: 12px;
}
</style>
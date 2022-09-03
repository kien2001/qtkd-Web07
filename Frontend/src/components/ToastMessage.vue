<template>
    <div class="toast" :class="objectClass" :style="objectStyle">
        <div class="toast-icon" :class="state">
        </div>
        <div class="toast-text">{{  message  }}</div>
        <div class="toast-icon toast-icon-close" @click="closeToast"></div>
    </div>
</template>
<script>
export default {
    name: "ToastMessage",
    computed: {
        objectClass() {
            return {
                "show": this.isShow
            }
        },
        objectStyle() {
            return {
                backgroundColor: `var(--${this.state})`
            }
        }
    },
    methods: {
        closeToast() {
            this.$store.commit("setIsShow", false)
        }
    },
    props: ["message", "state", "isShow"]
}
</script>
<style scoped>
.toast {
    --success: #31B491;
    --fail: #EC4141;
    --warning: #F07D42;
    --info: #4262F0;
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
    margin-bottom:20px;
}

.toast.show {
    animation: moveToast 3s ease-in-out;
}

.toast+.toast {
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

@keyframes moveToast {
    0% {
        right: -100%;
    }

    10% {
        right: 20px;
    }

    15%,
    20%,
    30%,
    40%,
    50%,
    60% {
        right: 10px;
    }

    100% {
        right: -100%;
    }
}
</style>
<template>
    <!-- show: isShow -->
    <div class="pop-up-container" :style="objectStyle">
        <div class="pop-up" :class="{ show: isShow }">
            <div class="pop-up-header">
                <div class="btn-icon close" @click="closePopUp"></div>
            </div>
            <div class="pop-up-content">
                {{ text }}
            </div>
            <div class="pop-up__footer">
                <BaseButton name="Huỷ bỏ" border="#D3D7DE" color="#fff" @clickBtn="closePopUp" colorHover="#E2E4E9" />
                <BaseButton name="Đồng ý" :color="colorBtn" :colorHover="colorHoverBtn" colorText="#FFFFFF"
                    @click="handleClick" />
            </div>
        </div>
    </div>
</template>
<style scoped>
.pop-up-container{
    position: fixed;
    top: 0;
    left: 0;
    right:0;
    bottom:0;
    background-color: rgba(51, 51, 51, 0.497);
    z-index: 1100;
    display:flex;
    justify-content: center;
    align-items: center;
}
.pop-up {
    opacity: 0;
    width: 432px;
    background-color: #fff;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    border-radius: 4px;
    box-shadow: 0px 8px 16px rgba(31, 34, 41, 0.16);
    transition: all 0.4s ease-in-out;
}

.pop-up.show {
    opacity: 1;
}

.pop-up .pop-up-header {
    padding: 16px 16px 0 16px;
    border-radius: 4px 4px 0 0;
    display: flex;
    justify-content: flex-end;
}

.pop-up .btn-icon {
    width: 10px;
    height: 10px;
    background-size: contain;
    cursor:pointer;
}

.pop-up .btn-close:hover {
    color: #ff0000;
    background-color: #bbbbbb;
}

.pop-up-content {
    padding: 16px;
    color: #1F2229;
    font-size: 13px;
    line-height: 16px;
    font-weight: 400;
}

.pop-up__footer {
    padding: 12px 16px 12px 0;
    border-radius: 0 0 4px 4px;
    display: flex;
    justify-content: flex-end;
    gap: 8px;
}
</style>
<script>
export default {
    name: "BasePopUp",
    data() {
        return {
            isShow: false
        }
    },
    computed:{
        objectStyle(){
            return { visibility: this.isShow ? "visible" : "hidden" }
        }
    },
    props: {
        text: {
            type: String,
            required: true
        },
        colorBtn: {
            type: String,
            required: true
        },
        colorHoverBtn: {
            type: String,
            required: true
        }
    },
    methods: {
        closePopUp() {
            this.isShow = false
        },
        handleClick() {
            this.$emit("handlePopUp")
            this.isShow = false
        }
    },
}
</script>
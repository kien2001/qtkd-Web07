<template>
    <div class="pop-up__container" :class="{show: isShow} ">
        <div class="pop-up-header">
            <div class="btn-icon close" @click="closePopUp"></div>
        </div>
        <div class="pop-up__content">
            {{text}}
        </div>
        <div class="pop-up__footer">
            <Button name="Huỷ bỏ" border="#D3D7DE" color="#fff" @clickBtn="closePopUp"
                colorHover="#E2E4E9" />
            <Button name="Đồng ý" :color="colorBtn" :colorHover="colorHoverBtn" colorText="#FFFFFF"
                 @click="handleClick" />
        </div>
    </div>
</template>
<style scoped>

.pop-up__container {
    width: 432px;
    position: fixed;
    top: -50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #fff;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    border-radius: 4px;
    z-index: 1100;
    box-shadow: 0px 8px 16px rgba(31, 34, 41, 0.16);
    transition: all 0.4s ease-in-out;
}
.pop-up__container.show{
    top: 50%;
}
.pop-up__container .pop-up-header {
    padding: 16px 16px 0 16px;
    border-radius: 4px 4px 0 0;
    display: flex;
    justify-content: flex-end;
}

.pop-up__container .btn-icon {
    width: 10px;
    height: 10px;
    background-size: contain;
}

.pop-up__container .btn-close:hover {
    color: #ff0000;
    background-color: #bbbbbb;
}

.pop-up__content {
    padding: 16px;
    color:#1F2229;
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
    name: "PopUp",
    components:["Button"],
    data(){
        return {
            isShow:false
        }
    },
    props:{
        text:{
            type:String,
            required:true
        },
        colorBtn:{
            type:String,
            required: true
        },
        colorHoverBtn:{
            type:String,
            required: true
        }
    },
    methods: {
        closePopUp(){
            this.isShow = false
        },
        handleClick(){
            this.$emit("handlePopUp")
            this.isShow = false
        }
    },
}
</script>
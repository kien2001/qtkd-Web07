<template>
    <div class="toast" :class="objectClass" :style="objectStyle">
        <div class="toast-icon" :class="state">
        </div>
        <div class="toast-text">{{message}}</div>
        <div class="toast-icon toast-icon-close"></div>
    </div>
</template>
<script>
export default {
    name: "ToastMessage",
    data(){
        return {
            isShow:false
        }
    },
    computed:{
        objectClass(){
            return {
                "show": this.isShow
            }
        },
        objectStyle(){
            return {
                backgroundColor: `var(--${this.state})`
            }
        }
    },
    watch:{
        isShow(newValue){
            let me = this
            if(newValue){
                setTimeout(()=>{
                    me.isShow = false
                }, 2000)
            }
        }
    },
    props:["message", "state"]
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
    top:10%;
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
    transition: all 0.5s ease-in-out;
    z-index: 1200;
}
.toast.show{
    right:0;
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
.success{
    background-image: url("../assets/img/tich-tron-green.svg");
}
.fail{
    background-image: url("../assets/img/ractangle-close-white.svg");
}
.toast-text {
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
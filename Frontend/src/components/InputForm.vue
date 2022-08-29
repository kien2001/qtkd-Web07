<template>
  <div class="input-container">
    <input type="text" class="input-text" :class="className" :id="id" v-model="name" :disabled="isDisabled" />
    <div class="clear" v-if="isClear" @click="clearInput"></div>
  </div>
</template>
<style scoped>

.input-container{
  position: relative;
}
.input-text[disabled]{
    background-color: #E2E4E9;
    cursor: not-allowed;
}
.input-text {
    min-height: 32px;
    max-height: 32px;
    border-radius: 4px;
    border-width: 1px;
    border-style: solid;
    border-color: #D3D7DE;
    outline: none;
    width: 100%;
    height: inherit;
    padding: 0 10px;
    background-color: #fff;
    font-size: 13px;
    line-height: 16px;
    font-weight: 400;
    color: #1F2229;
}
.input-text::placeholder{
    font-weight: 400;
    letter-spacing: 0;
    line-height: 16px;
    text-align: left;
    color: #99A1B2;
}
.input-text:not([disabled]):hover {
    border-color: #7189F4;
}

.input-text:not([disabled]):focus,
.input-text:not([disabled]):active {
    border-color: #4262F0;
}
.input-text:not([disabled]) ~ .clear{
  width: 12px;
  height: 12px;
  background-repeat: no-repeat;
  background-size: contain;
  background-position: center;
  background-image: url("../assets/img/blue-active/clear.svg");
  background-image: url("../assets/img/clear.svg");
  position: absolute;
  top:50%;
  right:9.5px;
  transform:translateY(-50%);
  cursor: pointer;
}
.input-text:not([disabled])~.clear:hover
{
  background-image: url("../assets/img/blue-active/clear.svg");
}
</style>
<script>
export default {
  name: 'InputForm',
  data () {
    return {
      name: '',
      className:'',
      isClear: false
    }
  },
  emits: ['changeName'],
  beforeMount(){
    this.className = this.class
  },
  updated(){
    this.$emit('changeName', this.name)
  },  
  methods: {
    clearInput() {
      console.log(1);
      this.name = ''
    }
  },
  watch:{
    name(newValue){
      if (newValue===''){
        this.isClear=false
      }else{
        this.isClear = true
      }
    }
  },
  props: ['isDisabled',  'id', 'class' ]

}
</script>

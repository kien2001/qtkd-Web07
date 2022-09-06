<template>
  <div class="input-container">
    <input type="text" class="input-text" :class="className" :id="id" v-model="name" :disabled="isDisabled"
      :maxlength="maxlength" />
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
    padding: 0 28px 0 10px;
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
  background: url('../assets/img/checkbox-collection.svg') no-repeat -4px -28px;
  width: 12px;
  height: 12px;
  position: absolute;
  top:50%;
  right:9.5px;
  transform:translateY(-50%);
  cursor: pointer;
}
.input-text:not([disabled])~.clear:hover
{
  background: url('../assets/img/checkbox-collection.svg') no-repeat -125px -23px;
  width: 17px;
    height: 17px;
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
  props: ['isDisabled', 'id', 'class', 'maxlength' ]

}
</script>

<template>
    <input type="text" class="input-text" v-model="name" :disabled="isDisabled" >
</template>
<style scoped>

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
</style>
<script>
import $ from 'jquery';
export default {
  name: 'InputForm',
  data () {
    return {
      name: ''
    }
  },
  updated () {
    this.$emit('changeName', this.name)
    if (this.$attrs.handleFullName) {
      $('#fullName').val(this.$attrs.handleFullName)
    }
  },
  props: ['isDisabled', 'changeName', 'fullName']

}
</script>

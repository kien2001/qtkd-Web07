<template>
    <div class="button-container" :style="styleContainer">
        <div class="button" :class="classObject" :style="styleObject" @click="handleClick" >
            {{ name }}
        </div>
        <div class="btn-combo-addition" v-if="isComboBtn" :style="styleObjectSecond"></div>
    </div>

</template>
<style scoped>
.button-container {
    display: flex;
    align-items: center;
    gap: 1px;
    width: fit-content;
}
.button.disabled{
  background-color: #B8C4F9;
    cursor: not-allowed;
}
.button-container>div:not(.disabled):hover{
    background-color: var(--color-hover)!important;
}
.button {
    box-sizing: border-box;
    cursor: pointer;
    height: 32px;
    min-width: max-content;
    padding: 7px 16px;
    font-weight: 600;
    text-align: left;
    line-height: 16px;
    font-size: 13px;
    letter-spacing: 0px;
    border-radius: 4px;
    align-items: center;
    display: flex;
    background-color: var(--color);
    border-style: solid;
    border-width: 1px;
}
.button-icon {
    background-repeat: no-repeat;
    background-position-x: 8px;
    background-position-y: center;
    background-size: 16px 16px;
    width:100%;
}

.btn-combo-addition {
    box-sizing: border-box;
    width: 32px;
    height: 32px;
    background-repeat: no-repeat;
    background-size: 16px 16px;
    background-position: center;
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 0 4px 4px 0;
    padding: 8px;
    cursor: pointer;
    background-color: var(--color);
}
</style>
<script>
export default {
  name: 'Button',
  template: 'Button',
  props: {
    name: {
      type: String,
      required: true
    },
    color: {
      type: String,
      required: true
    },
    hasIcon: {
      type: Boolean,
      required: false,
      default: false
    },
    isComboBtn: {
      type: Boolean,
      required: false,
      default: false
    },
    colorHover: {
      type: String,
      required: true
    }
    // colorText:{
    //     type:String,
    //     required:false,
    //     default: "#1F2229"
    // }
  },
  methods: {
    handleClick () {
      this.$emit('clickBtn')
    }
  },
  computed: {
    styleContainer () {
      return {
        '--color': this.color,
        '--color-hover': this.colorHover
      }
    },
    classObject () {
      return {
        'button-icon': this.hasIcon,
        'button-combo': this.isComboBtn,
        'disabled': this.$attrs.disabled || false
      }
    },
    styleObject () {
      return {
        color: this.$attrs.colorText || '#1F2229',
        paddingLeft: this.hasIcon ? '32px' : '16px',
        backgroundImage: `url(${this.$attrs.urlMainBtn})` || '',
        borderRadius: this.isComboBtn ? '4px 0 0 4px' : '4px',
        border: this.$attrs.border? `1px solid ${this.$attrs.border}` : 'unset'
      }
    },
    styleObjectSecond () {
      return {
        backgroundImage: `url(${this.$attrs.urlSecondBtn})` || ''
      }
    }
  }
}
</script>

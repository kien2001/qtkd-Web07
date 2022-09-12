<template>
  <div class="input-container">
    <input
      type="text"
      class="input-text"
      :class="className"
      :id="id"
      v-model="name"
      :disabled="isDisabled"
      :maxlength="maxlength"
    />
    <div class="clear" v-if="isClear" @click="clearInput"></div>
  </div>
</template>
<style scoped>
.input-container {
  position: relative;
  width: 100%;
}
.input-text[disabled] {
  background-color: #e2e4e9;
  cursor: not-allowed;
}
.input-text {
  min-height: 32px;
  max-height: 32px;
  border-radius: 4px;
  border-width: 1px;
  border-style: solid;
  border-color: #d3d7de;
  outline: none;
  width: 100%;
  height: inherit;
  padding: 0 28px 0 16px;
  background-color: #fff;
  font-size: 13px;
  line-height: 16px;
  font-weight: 400;
  color: #1f2229;
  transition: all .2s cubic-bezier(0.645, 0.045, 0.355, 1);
}
.input-text::placeholder {
  font-weight: 400;
  letter-spacing: 0;
  line-height: 16px;
  text-align: left;
  color: #99a1b2;
}
.input-text:not([disabled]):hover {
  border-color: #7189f4;
}

.input-text:not([disabled]):focus,
.input-text:not([disabled]):active {
  border-color: #4262f0;
}
.input-text:not([disabled]) ~ .clear {
  background: url("../assets/img/checkbox-collection.svg") no-repeat -4px -28px;
  width: 12px;
  height: 12px;
  position: absolute;
  top: 52%;
  right: 8px;
  transform: translateY(-50%);
  cursor: pointer;
}
.input-text:not([disabled]) ~ .clear:hover {
  background: url("../assets/img/checkbox-collection.svg") no-repeat -125px -23px;
  width: 17px;
  height: 17px;
}
</style>
<script>
export default {
  name: "InputForm",
  data() {
    return {
      name: "",
      className: "",
      isClear: false,
    };
  },
  emits: ["changeName"],
  beforeMount() {
    this.className = this.class;
  },
  updated() {
    this.$emit("changeName", this.name);
  },
  methods: {
    clearInput() {
      this.name = "";
    },
  },
  watch: {
    name(newValue) {
      if (newValue === "") {
        this.isClear = false;
      } else {
        this.isClear = true;
      }
    },
  },
  props: ["isDisabled", "id", "class", "maxlength"],
};
</script>

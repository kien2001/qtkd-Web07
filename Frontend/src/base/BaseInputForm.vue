<template>
  <div class="input-container" ref="input-container">
    <div class="input">
      <input
        type="text"
        class="input-text"
        :class="className"
        :id="id"
        v-model="name"
        :disabled="isDisabled"
        :maxlength="maxlength"
        @keyup.prevent="handleAction"
        ref="input"
      />
      <div class="clear" v-if="isClear" @click="clearInput"></div>
    </div>
  </div>
</template>
<style>
.input-container {
  width: 100%;
}
.input {
  position: relative;
  background-color: #fff;
}
.input-text.error {
  border-color: red !important;
}
.input + span {
  display: block;
  margin-top: 8px;
  color: red;
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
  transition: all 0.2s cubic-bezier(0.645, 0.045, 0.355, 1);
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
import StatusCode from "@/config/StatusCode";
import { validateEmail, validatePhoneNumber } from "../js/handleValidate";
import $ from "jquery";
export default {
  name: "BaseInputForm",
  data() {
    return {
      name: "",
      className: "",
      isClear: false,
      timeoutQuery: null,
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
    /**
     * TODO: Xử lý khi click vào button clear
     * !Created by LVKIEN 16/09/2022
     */
    clearInput() {
      this.name = "";
      this.$nextTick(() => this.$refs.input.focus());
      $(this.$refs.input).removeClass("error");
      $(this.$refs.input).parent().next("span").remove();
    },
    /**
     * TODO: Xử lý hành động khi validate
     * !Created by LVKIEN 16/09/2022
     */
    handleAction(e) {
      if (this.isTel) {
        this.handleValidateTel(e);
      } else if (this.isEmail) {
        this.handleValidateEmail(e);
      }
    },
    /**
     * TODO: Xử lý validate số điện thoại
     * @param {*} e
     * !Created by LVKIEN 14/09/2022
     */
    handleValidateTel(e) {
      try {
        if (e.keyCode !== 9) {
          let check = false;
          clearTimeout(this.timeOut);
          this.timeOut = setTimeout(() => {
            if (validatePhoneNumber(this.name) || this.name === "") {
              check = true;
            } else {
              check = false;
            }
            if (!check) {
              if (!$(this.$refs.input).hasClass("error")) {
                $(this.$refs.input).addClass("error");
                $(this.$refs.input)
                  .parent()
                  .after(
                    `<span >${StatusCode.MessageError.NotValidPhone}</span>`
                  );
              }
            } else {
              $(this.$refs.input).removeClass("error");
              $(this.$refs.input).parent().next("span").remove();
            }
          }, 1000);
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Xử lý validate email: Sau 1s, nếu ko nhập đúng định dạng, hiển thị lỗi
     * @param {*} e
     * Created by LVKIEN 1/9/2022
     */
    handleValidateEmail(e) {
      try {
        if (e.keyCode !== 9) {
          let check = false;
          clearTimeout(this.timeOut);
          this.timeOut = setTimeout(() => {
            if (validateEmail(this.name) || this.name === "") {
              check = true;
            } else {
              check = false;
            }
            if (!check) {
              if (!$(this.$refs.input).hasClass("error")) {
                $(this.$refs.input).addClass("error");
                $(this.$refs.input)
                  .parent()
                  .after(
                    `<span >${StatusCode.MessageError.NotValidEmail}</span>`
                  );
              }
            } else {
              $(this.$refs.input).removeClass("error");
              $(this.$refs.input).parent().next("span").remove();
            }
          }, 1000);
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  watch: {
    name(newValue) {
      if(newValue){
        if (newValue.length === 0) {
          this.isClear = false;
        } else {
          this.isClear = true;
        }
      }else{
          this.isClear = false;
      }
    },
  },
  props: ["isDisabled", "id", "class", "maxlength", "isTel", "isEmail"],
};
</script>

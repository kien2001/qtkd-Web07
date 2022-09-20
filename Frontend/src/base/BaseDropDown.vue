<template>
  <div class="dropdown" v-click-outside="exit" ref="dropdown" :name="name" @keydown.tab="exit">
    <div
      @focus="showOptions" tabindex="0"
      class="dropdown-header"
      @mousedown.stop.prevent="showOptions"
      ref="dropdown-header"
      >
      {{ this.oldSearchFilter || placeholder }}
      <span class="icon-dropdown"></span>
    </div>

    <div
      class="dropdown-container"
      :style="{
        visibility: optionsShown ? 'visible' : 'hidden',
        opacity: optionsShown ? 1 : 0,
        transition: 'all 0.2s cubic-bezier(0.645, 0.045, 0.355, 1)',
      }"
    >
      <!-- Dropdown Input -->
      <input
      tabindex="-1"
        class="dropdown-input"
        :id="id"
        v-model="searchFilter"
        placeholder="Tìm kiếm"
        ref="input"
        v-show="showInput"
      />

      <!-- Dropdown Menu -->
      <div class="dropdown-content">
        <div
          v-if="showNoValue"
          :style="{ textAlign: 'center', backgroundColor: '#fff' }"
        >
          Không có dữ liệu
        </div>
        <div
          class="dropdown-item"
          @click.stop.prevent="selectOption(option)"
          v-for="(option, index) in filteredOptions"
          :key="index"
          :value="option.id"
          ref="filteredOptions"
        >
          {{ option.name }}
        </div>
        <BaseLoading v-if="showLoading" />
      </div>
    </div>
  </div>
</template>

<script>
import $ from "jquery";
import { handleTransferObject } from "../js/common";
import emitter from "@/js/emitter";
import axiosInstance from "@/js/axios";

export default {
  name: "BaseDropDown",
  template: "BaseDropDown",
  props: {
    id: {
      type: String,
      required: false,
    },
    placeholder: {
      type: String,
      required: false,
      default: "Please select an option",
      // note: 'Placeholder of dropdown'
    },
    showInput: {
      type: Boolean,
      required: true,
    },
    name: {
      type: String,
      required: false,
    },
    fetchDataWhenClick: {
      type: Boolean,
      required: true,
    },
  },
  data() {
    return {
      showNoValue: false,
      showLoading: false,
      selected: {},
      optionsShown: false,
      searchFilter: "",
      options: [],
      getSuccess: false,
      oldSearchFilter: "",
    };
  },
  computed: {
    /**
     * TODO: Search dựa trên input
     * !Created by LVKIEN 10/09/2022
     */
    filteredOptions() {
      const filtered = [];
      const regOption = new RegExp(this.searchFilter, "ig");
      for (const option of this.options) {
        if (this.searchFilter.length < 1 || option.name.match(regOption)) {
          filtered.push(option);
        }
      }
      return filtered;
    },
  },
  // mounted(){
  //   $(window).click(this.exit)
  // },
  // beforeUnmount(){
  //   $(window).unbind("click")
  // },
  methods: {
    /**
     * TODO: Xử lý khi chọn 1 option
     * @param {*} option
     * ! Created by LVKIEN 10/09/2022
     */
    selectOption(option) {
      this.optionsShown = false;
      this.selected = option;
      this.searchFilter = option.name;
      this.oldSearchFilter = option.name
      $(this.$refs["dropdown-header"]).removeAttr("show");
      if (this.showInput) {
        this.$emit(
          "selected",
          this.selected.id === 0
            ? ""
            : { id: this.selected.id, name: this.selected.name }
        );
      } else {
        this.$emit(
          "selected",
          this.selected.id === 0
            ? ""
            : { id: this.selected.id, name: this.$attrs["name-value"] || "" }
        );
      }
      
    },
    /**
     * TODO: Set options list cho dropdown
     * @param {*} values
     * !Created by LVKIEN 10/09/2022
     */
    setOptions(values) {
      this.options = values;
    },
    /**
     * TODO: Xử lý hiển thị ra danh sách option
     * @param {*} e
     * !Created by LVKIEN 10/09/2022
     */
    async showOptions() {
      if (!this.optionsShown) {
        this.optionsShown = true;
        $(this.$refs["dropdown-header"]).attr("show", "show");
        this.searchFilter = "";
        $(this.$refs["dropdown-header"]).css("border-color", "#7189F4");
        if (this.name) {
          if (
            this.name !== "Cities" &&
            this.name !== "Districts" &&
            this.name !== "Wards"
          ) {
            if (!this.fetchDataWhenClick) {
              if (!this.getSuccess) {
                this.showLoading = true;
                const data = await axiosInstance
                  .get(this.name)
                  .then((res) => res.data)
                  .catch((error) => error.response.data);
                let result;
                this.showLoading = false;
                // get dữ liệu lỗi
                if (!data.flag) {
                  this.$store.commit("setState", "fail");
                  this.$store.commit("setMessage", data.userMsg[0]);
                  emitter.emit("showToast");
                  this.showNoValue = true;
                } else {
                  this.getSuccess = true;
                  result = handleTransferObject(data.data);
                  this.options = result;
                }
              }
            } else {
              if (!sessionStorage.getItem(this.name)) {
                this.showLoading = true;
                const data = await axiosInstance
                  .get(this.name)
                  .then((res) => res.data)
                  .catch((error) => error.response.data);
                this.showLoading = false;
                let result;
                // get dữ liệu lỗi
                if (!data.flag) {
                  this.$store.commit("setState", "fail");
                  this.$store.commit("setMessage", data.userMsg[0]);
                  emitter.emit("showToast");
                  this.showNoValue = true;
                } else {
                  this.success = true;
                  result = handleTransferObject(data.data);
                  this.options = result;
                  sessionStorage.setItem(this.name, JSON.stringify(result));
                }
              } else {
                this.options = JSON.parse(sessionStorage.getItem(this.name));
              }
            }
          }
        }
      } else {
        $(this.$refs["dropdown-header"]).removeAttr("show");
        this.optionsShown = false;
      }
    },
    /**
     * TODO: Xử lý tắt dropdown
     * !Created by LVKIEN 10/09/2022
     */
    exit() {
      // if( $(this.$refs["dropdown"]).find(e.target).length > 0) return;
      // console.log($(this.$refs["dropdown"]).find(e.target));
      if (this.selected.id === undefined) {
        this.selected = {};
        this.searchFilter = "";
      } else {
        this.searchFilter = this.selected.name;
      }
      $(this.$refs["dropdown-header"]).removeAttr("show");
      this.optionsShown = false;
      $(this.$refs["dropdown-header"]).css("border-color", "#D3D7DE");
    },
  },
  updated() {
    if (this.selected.id) {
      const arr = this.$refs.filteredOptions;
      arr?.forEach((item) => {
        $(item).removeClass("selected");
        if ($(item).is(`[value=${this.selected.id}]`)) {
          $(item).addClass("selected");
        }
      });
    }
  },
  watch: {
    selected(newValue) {
      if (Object.keys(newValue).length > 0) {
        $(this.$refs["dropdown-header"]).css("color", "#1F2229");
      }
    },
    optionsShown(newValue) {
      if (newValue) {
        setTimeout(()=>this.$nextTick(() => this.$refs.input.focus()), 100)
        const dropdownContainer = $(".dropdown-container").toArray();
        const currentDropdown = $(this.$refs.dropdown).find(
          ".dropdown-container"
        );
        dropdownContainer.forEach((dropdown) => {
          $(dropdown).not(currentDropdown).css("visibility", "hidden");
        });
      }
    },
  },
};
</script>

<style scoped>
.dropdown {
  --width-dropdown: 100%;
}

.dropdown {
  position: relative;
  display: block;
}

.dropdown .dropdown-header {
  position: relative;
  overflow: hidden;
  border-width: 1px;
  border-style: solid;
  border-color: #d3d7de;
  text-align: left;
  line-height: 32px;
  height: 32px;
  color: #99a1b2;
  padding: 0 16px 0 16px;
  border-radius: 4px;
  user-select: none;
  transition: border-color 0.2s cubic-bezier(0.645, 0.045, 0.355, 1);
  background-color: #fff;
  cursor:pointer;
}
.dropdown .dropdown-header .icon-dropdown {
  position: absolute;
  top: 50%;
  right: 10px;
  background: url("../assets/img/icon_collection.svg") no-repeat -52px -38px;
  width: 9px;
  height: 5px;
  transform: translateY(-50%) rotate(0);
  transition: transform 0.2s cubic-bezier(0.645, 0.045, 0.355, 1);
}
.dropdown .dropdown-header[show] .icon-dropdown {
  transform: rotate(180deg);
}

.dropdown .dropdown-header:hover {
  border-color: #7189f4 !important;
}

.dropdown .dropdown-header:focus,
.dropdown .dropdown-header:active {
  border-color: #4262f0;
}

.dropdown .dropdown-container {
  position: absolute;
  box-shadow: 0 2px 4px rgba(31, 34, 41, 0.57);
  z-index: 1;
  width: var(--width-dropdown);
}

.dropdown .dropdown-container > div:last-child {
  max-height: 200px;
  overflow-y: auto;
}

.dropdown .dropdown-container .dropdown-input {
  outline: none;
  background: #fff;
  border: none;
  border-bottom: 1px solid #99a1b2;
  color: #333;
  display: block;
  padding: 6px;
  min-width: var(--width-dropdown);
  padding: 8px 8px 8px 16px;
  background-image: url("../assets/img/search1.svg");
  background-size: 16px 16px;
  background-repeat: no-repeat;
  background-position-y: center;
  background-position-x: calc(100% - 8px);
}

.dropdown .dropdown-container .dropdown-input::placeholder {
  color: #99a1b2;
}

.dropdown .dropdown-container .dropdown-content {
  background-color: #fff;
  position: relative;
  min-width: var(--width-dropdown);
  max-height: var(--width-dropdown);
  min-height: 100px;
  border: none;
  overflow: auto;
  padding: 5px 0;
}

.dropdown .dropdown-container .dropdown-content .dropdown-item {
  color: black;
  text-align: left;
  line-height: 16px;
  font-size: 13px;
  padding: 8px 16px;
  text-decoration: none;
  display: block;
  cursor: pointer;
  text-align: left;
}

.dropdown-item.selected {
  color: #4262f0 !important;
  background-image: url("../assets/img/blue-active/tich.svg");
  background-size: 16px 16px;
  background-repeat: no-repeat;
  background-position-y: center;
  background-position-x: calc(100% - 8px);
}

.dropdown .dropdown-container .dropdown-content .dropdown-item:hover {
  background-color: #e7ecf5;
}
</style>

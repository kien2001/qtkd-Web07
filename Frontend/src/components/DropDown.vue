<template>
  <div class="dropdown" v-if="options" @click="showOptions" ref="dropdown" :name="name">
    <div class="dropdown-header" ref="dropdown-header">
      {{  this.oldSearchFilter || placeholder || "-Không chọn-"  }}
    </div>
    <div class="dropdown-container" v-show="optionsShown">
      <!-- Dropdown Input -->
      <input class="dropdown-input" :id="id" v-model="searchFilter" placeholder="Tìm kiếm" ref="input"
        v-show="showInput" />

      <!-- Dropdown Menu -->
      <div class="dropdown-content">
        <div v-if="showNoValue" :style="{ textAlign: 'center', backgroundColor: '#fff' }">
          Không có dữ liệu
        </div>
        <div class="dropdown-item" @mousedown.self="selectOption(option)" v-for="(option, index) in filteredOptions"
          :key="index" :value="option.id" ref="filteredOptions">
          {{  option.name  }}
        </div>
        <Loading v-if="showLoading" />
      </div>
    </div>
  </div>
</template>

<script>
import $ from "jquery";
import axios from "axios";
import { rootApi } from "@/js/config";
import Loading from "./Loading.vue";
export default {
  name: "DropDown",
  template: "DropDown",
  props: {
    id: {
      type: String,
      required: false
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
      currentValue: {},
    };
  },
  computed: {
    filteredOptions() {
      const filtered = [];
      const regOption = new RegExp(this.searchFilter, "ig");
      for (const option of this.options) {
        if (this.searchFilter.length < 1 || option.name.match(regOption)) {
          filtered.push(option);
        }
      }
      return filtered;
    }
  },
  methods: {
    selectOption(option) {
      this.optionsShown = false;
      this.selected = option;
      this.searchFilter = option.name;
      if (this.showInput) {
        this.$emit(
          "selected",
          this.selected.id === 0
            ? ""
            : { id: this.selected.id, name: this.selected.name }
        );
      } else {
        this.$emit("selected", this.selected.id === 0 ? "" :{ id:this.selected.id, name:this.$attrs.name1 || ""});
      }
    },
    setOptions(values) {
      this.options = values;
    },
    async showOptions(e) {
      e.stopPropagation();
      const dropdownContainer = $(".dropdown-container").toArray();
      const currentDropdown = $(this.$refs.dropdown).find(".dropdown-container")
      dropdownContainer.forEach(dropdown=>{
        $(dropdown).not(currentDropdown).css("display", "none")
      })
      this.optionsShown = true;
      this.searchFilter = "";
      this.$nextTick(() => this.$refs.input.focus());
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
              const data = await axios
                .get(`${rootApi}${this.name}`)
                .then((res) => res.data)
                .catch((error) => error.response.data);
              let result;
              this.showLoading = false;
              // get dữ liệu lỗi
              if (!data.flag) {
                this.$store.commit("setState", "fail")
                this.$store.commit("setMessage", data.userMsg[0])
                this.$store.commit("setIsShow", true)
                this.showNoValue = true;
              } else {
                this.getSuccess = true;
                result = this.handleTransferObject(data.data);
                this.options = result;
              }
            }
          } else {
            if(!sessionStorage.getItem(this.name)){
              this.showLoading = true;
              const data = await axios
                .get(`${rootApi}${this.name}`)
                .then((res) => res.data)
                .catch((error) => error.response.data);
              this.showLoading = false;
              let result;
              // get dữ liệu lỗi
              if (!data.flag) {
                this.$store.commit("setState", "fail")
                this.$store.commit("setMessage", data.userMsg[0])
                this.$store.commit("setIsShow", true)
                this.showNoValue = true;
              } else {
                this.success = true
                result = this.handleTransferObject(data.data);
                console.log(result);
                this.options = result;
                sessionStorage.setItem(this.name, JSON.stringify(result));
              }
            }else{
              this.options = JSON.parse(sessionStorage.getItem(this.name))
            }
          }
        }
      }
    },
    handleTransferObject(objectArr) {
      const objectKeys = Object.keys(objectArr[0]);
      let initResult = [{ id: 0, name: "-Không chọn-" }];
      const result = objectArr.map((item) => {
        const resultObject = {
          id: item[objectKeys[0]],
          name: item[objectKeys[1]],
        };
        return resultObject;
      });
      initResult = [...initResult, ...result];
      return initResult;
    },
    exit() {
      if (this.selected.id === undefined) {
        this.selected = {};
        this.searchFilter = "";
      } else {
        this.searchFilter = this.selected.name;
      }
      this.optionsShown = false;
      $(this.$refs["dropdown-header"]).css("border-color", "#D3D7DE");
    },
  },
  updated() {
    if (this.selected.id) {
      const arr = this.$refs.filteredOptions;
      arr.forEach((item) => {
        $(item).removeClass("selected");
        if ($(item).is(`[value=${this.selected.id}]`)) {
          $(item).addClass("selected");
        }
      });
    }
  },
  mounted(){
    $(window).click(this.exit);
  },
  watch: {
    searchFilter(newVal, oldVal) {
      if (newVal && newVal !== "") {
        this.oldSearchFilter = newVal;
      }
    }
  },
  components: { Loading },
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
  overflow: hidden;
  border-width: 1px;
  border-style: solid;
  border-color: #d3d7de;
  text-align: left;
  line-height: 32px;
  height: 32px;
  padding: 0 8px 0 16px;
  border-radius: 4px;
  color: #1f2229;
  background-image: url("../assets/img/arrow-down.svg");
  background-size: 16px 16px;
  background-repeat: no-repeat;
  background-position-y: center;
  background-position-x: calc(100% - 8px);
  user-select: none;
}

.dropdown .dropdown-header:hover {
  border-color: #7189f4;
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

.dropdown .dropdown-container>div:last-child {
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
  /* max-width: var(--width-dropdown); */
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

div<template>
  <div class="sidebar-left" ref="sidebarLeft">
    <div class="sidebar-left-header sidebar-title" v-show="isToggle">
      <p class="title-content">bộ lọc đã lưu</p>
      <div class="btn-icon icon-small arrow-up"></div>
    </div>
    <div class="sidebar-left-filter" v-show="isToggle">
      <div class="filter-option-title sidebar-title">
        <p class="title-content">LỌC TIỀM NĂNG THEO</p>
        <div class="icon icon-search icon-small"></div>
      </div>
      <div class="filter-option-list">
        <div
          class="filter-option-item"
          v-for="option in fieldOptions"
          :key="option.id"
          :name="option.id"
          @click.stop.prevent="filterItemOnClick"
          ref="filterOptions"
        >
          <div class="icon-item icon-checkbox icon icon-small"></div>
          <div class="item-content">{{ option.name }}</div>
          <BaseDropDown
            @selected="getSelectedType"
            @click="showOption1"
            :showInput="false"
            :name-value="option.id"
            placeholder="Không chọn"
            ref="option1"
            v-if="option.type === 1"
            :fetchDataWhenClick="false"
          />
          <BaseDropDown
            @selected="getSelectedType"
            @click="showOption2"
            :showInput="false"
            :name-value="option.id"
            placeholder="Không chọn"
            ref="option2"
            v-else
            :fetchDataWhenClick="false"
          />
          <BaseInputForm id="input" ref="input" />
          <BaseComboBox
            :options="
              async () => {
                return await this.getDataComboBox(option.url);
              }
            "
            ref="combobox"
          />
        </div>
      </div>
    </div>
    <Transition name="fade" mode="out-in">
      <div class="sidebar-left-footer" v-if="showButton">
        <BaseButton
          name="Bỏ lọc"
          color="#fff"
          colorHover="#D0D8FB"
          @clickBtn="removeAllFilters"
        />
        <BaseButton
          name="Áp dụng"
          color="#4262F0"
          colorHover="#2B4EEE"
          colorText="#FFFFFF"
          @clickBtn="searchFilter"
        />
      </div>
    </Transition>
    <div class="toggle">
      <div class="btn-toggle" @click="handleClickToggle"></div>
    </div>
  </div>
</template>
<script>
import $ from "jquery";
import handleClickFilterItem from "@/js/checkbox";
import { handleToggleSideBar } from "@/js/toggleSideBar";
import { fieldMappingOptions, optionType1, optionType2 } from "../../js/config";
// import InputForm from "../components/InputForm.vue";
import emitter from "@/js/emitter";
import axiosInstance from "@/js/axios";

export default {
  name: " SideBarLeft",
  // components: { InputForm },
  data() {
    return {
      isToggle: true,
      showButton: false,
      fieldOptions: [],
      listFilterOption: [],
      option1: optionType1,
      option2: optionType2,
      objectResult: {},
      selectedOptionFilter: {},
      isFiltered: false,
    };
  },
  /**
   * TODO: tạo 1 object lưu tất cả các trường dùng để lọc
   * !Created by LVKIEN 7/9/2022
   */
  beforeMount() {
    try {
      this.fieldOptions = fieldMappingOptions;
      let objectType1 = [];
      let objectType2 = [];
      fieldMappingOptions.forEach((field) => {
        if (field.type === 1) {
          objectType1 = [
            ...objectType1,
            { name: field.id, type: "", value: "" },
          ];
        } else if (field.type === 2) {
          // cho value để sửa lỗi backend
          objectType2 = [
            ...objectType2,
            { name: field.id, type: "", value: field.id },
          ];
        }
      });
      this.objectResult = {
        1: objectType1,
        2: objectType2,
      };
    } catch (error) {
      console.log(error);
    }
  },
  /**
   *  TODO: gán sự kiện click vào checkbox
   * ! Created by LVKIEN 8/9/2022
   */
  async mounted() {
    // ẩn tất cả các ô input
    this.$refs.input.forEach((item) => {
      $(item.$el).css("display", "none");
    });
    // ẩn tất cả các ô combobox
    this.$refs.combobox.forEach((item) => {
      $(item.$el).css("display", "none");
    });
  },
  watch: {
    /**
     * TODO: Xử lý hiển thị các ô nhập dữ liệu và thanh button
     * @param {*} newValue
     * !Created by LVKIEN 3/9/2022
     */
    listFilterOption(newValue) {
      try {
        // Khi 1 option bị bỏ check, display none các phần tử trong nó
        $(".filter-option-item:not([checked])")
          .toArray()
          .forEach((item) => {
            $(item).children(".input-container").css("display", "none");
            $(item).children(".multiselect").css("display", "none");
          });
        // bật tắt thanh button
        if (newValue.length > 0) {
          this.showButton = true;
        } else {
          this.showButton = false;
        }
        // reset giá trị value của field khi bỏ checked
        this.objectResult[1].forEach((field) => {
          let check = newValue.includes(field[Object.keys(field)[0]]);
          if (!check) {
            field.type = "";
            field.value = "";
          }
        });
        this.objectResult[2].forEach((field) => {
          let check = newValue.includes(field[Object.keys(field)[0]]);
          if (!check) {
            field.type = "";
            field.value = "";
          }
        });
        // Khi đã search và bỏ check, Không có ô nào đc check, gọi lại api
        if (this.isFiltered && newValue.length === 0) {
          this.$store.commit("setListFilter", []);
          this.isFiltered = false;
        }

        // reset lại giá trị selected của dropdown khi bỏ check
        this.$refs.option1.forEach((option1) => {
          if (!$(option1.$el).parent().attr("checked")) {
            option1.selected = {};
            option1.oldSearchFilter = "";
            $(option1.$el)
              .find(".dropdown-item.selected")
              .removeClass("selected");
          }
        });
        this.$refs.option2.forEach((option2) => {
          if (!$(option2.$el).parent().attr("checked")) {
            option2.selected = {};
            option2.oldSearchFilter = "";
            $(option2.$el)
              .find(".dropdown-item.selected")
              .removeClass("selected");
          }
        });
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * TODO: Xử lý ẩn hiện các ô input nhập dữ liệu khi chọn từng kiểu dropdown
     * @param {*} newValue
     * !Created by LVKIEN 3/9/2022
     */
    selectedOptionFilter(newValue) {
      try {
        // reset giá trị filter khi thay đổi dropdown filter
        this.$refs.combobox.forEach((combobox) => {
          if ($(combobox.$el).parent().attr("name") === newValue.name) {
            combobox.value = [];
          }
        });
        this.$refs.input.forEach((input) => {
          if ($(input.$el).parent().attr("name") === newValue.name) {
            input.name = "";
          }
        });
        // reset lại giá trị của field trong objectResult khi thay đổi dropdown

        this.objectResult[1].forEach((field) => {
          if (field.name === newValue.name) {
            field.value = "";
          }
        });
        if (newValue) {
          this.$refs.option1.forEach((item) => {
            if (newValue.name === item?.$attrs["name-value"]) {
              if (
                newValue.id % 2 === 0 ||
                item.$attrs["name-value"] === "FullName" ||
                item.$attrs["name-value"] === "Owner"
              ) {
                $(item.$el)
                  .siblings(".input-container")
                  .css("display", "block");
                $(item.$el).siblings(".multiselect").css("display", "none");
              } else {
                $(item.$el).siblings(".input-container").css("display", "none");
                $(item.$el).siblings(".multiselect").css("display", "block");
              }
            }
          });
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
  methods: {
    /**
     * Format result, bỏ hết các trường không được chọn
     * @param {*} result
     * Created by LVKIEN 4/9/2022
     */
    formatFilterResult(result) {
      try {
        const listKey = Object.keys(result);
        let formatResult = listKey.map((key) => {
          let tempArr = result[key].map((item) => {
            if (item.type !== "" && item.value !== "") {
              return item;
            }
          });
          tempArr = tempArr.filter((item) => item !== undefined);
          return tempArr;
        });
        return formatResult.flat();
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * TODO: Kiểm tra xem user có nhập đủ các trường filter hay chưa
     * !Created by LVKIEN 8/9/2022
     */
    checkEmptyField() {
      try {
        let checkInput = false;
        let checkCombobox = false;
        // Kiểm tra empty tát cả các input
        this.$refs.input.every((input) => {
          if ($(input.$el).css("display") !== "none") {
            if (!input?.name || !input.name?.trim()) {
              checkInput = true;
              return false;
            }
          }
          return true;
        });
        // Kiểm tra empty tát cả các combobox
        this.$refs.combobox.every((combobox) => {
          if ($(combobox.$el).css("display") !== "none") {
            const objectKeyCombobox = combobox.value
              ? Object.keys(combobox.value)
              : null;
            if (!objectKeyCombobox || objectKeyCombobox.length === 0) {
              checkCombobox = true;
              return false;
            }
          }
          return true;
        });
        // đếm số lượng dropdown đã đc selected
        let countSelectedField = 0;
        this.$refs.option1.forEach((item) => {
          if (Object.keys(item.selected).length > 0) {
            countSelectedField++;
          }
        });
        this.$refs.option2.forEach((item) => {
          if (Object.keys(item.selected).length > 0) {
            countSelectedField++;
          }
        });
        // nếu input, combobox trống hoặc số field đc check khác số giá trị option của dropdown được chọn
        if (
          checkInput ||
          checkCombobox ||
          countSelectedField !== this.listFilterOption.length
        ) {
          return true;
        }
        return false;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Lấy giá trị filter để lọc
     * Created by LVKIEn 4/9/2022
     */
    searchFilter() {
      try {
        if (!this.checkEmptyField()) {
          this.$refs.filterOptions.forEach((option) => {
            if ($(option).attr("checked")) {
              this.$refs.combobox.forEach((combobox) => {
                if (combobox.value) {
                  if (
                    $(combobox.$el).parent().attr("name") ===
                      $(option).attr("name") &&
                    Object.keys(combobox.value).length > 0
                  ) {
                    const indexFilterField = this.objectResult[1].findIndex(
                      (item) =>
                        item[Object.keys(item)[0]] === $(option).attr("name")
                    );
                    if ($(combobox.$el).parent().attr("name") === "Vocative") {
                      this.objectResult[1][indexFilterField].value =
                        combobox.value.map((value) => value.id.toString());
                    } else {
                      this.objectResult[1][indexFilterField].value =
                        combobox.value.map((value) => value.value);
                    }
                  }
                }
              });
              this.$refs.input.forEach((input) => {
                if (
                  $(input.$el).parent().attr("name") ===
                    $(option).attr("name") &&
                  input.name.trim() !== ""
                ) {
                  const indexFilterField = this.objectResult[1].findIndex(
                    (item) =>
                      item[Object.keys(item)[0]] === $(option).attr("name")
                  );
                  this.objectResult[1][indexFilterField].value = input.name;
                }
              });
            }
          });
          this.$store.commit(
            "setListFilter",
            this.formatFilterResult(this.objectResult)
          );
          this.isFiltered = true;
        } else {
          this.$store.commit("setState", "fail");
          this.$store.commit("setMessage", "Bạn chưa nhập đủ thông tin");
          emitter.emit("showToast");
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Lấy giá trị ô checked filter
     * @param {*} selected
     * Created by LVKIEN 2/9/2022
     */
    getSelectedType(selected) {
      try {
        if (selected) {
          this.selectedOptionFilter = selected;
          // chọn vào dropdown loại 1
          if (selected.id < 5) {
            this.objectResult[1].forEach((object) => {
              if (object[Object.keys(object)[0]] === selected.name) {
                object.type = selected.id;
              }
            });
            // chọn vào dropdown loại 2
          } else {
            this.objectResult[2].forEach((object) => {
              if (object[Object.keys(object)[0]] === selected.name) {
                object.type = selected.id;
                object.value = selected.name;
              }
            });
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Set các option cho dropdown
     * Created by LVKIEN 2/9/2022
     */
    showOption1(e) {
      const optionsVocative = [
        { id: 1, name: "Là" },
        { id: 3, name: "Không là" },
      ];

      this.$refs["option1"].forEach((option1) => {
        if ($(option1.$el).attr("name-value") !== "Vocative") {
          option1.setOptions(this.option1);
        } else {
          option1.setOptions(optionsVocative);
        }
      });
    },
    /**
     * Set các option cho dropdown
     * Created by LVKIEN 2/9/2022
     */
    showOption2() {
      this.$refs["option2"].forEach((option2) => {
        option2.setOptions(this.option2);
        // console.log(option2.options);
      });
    },
    /**
     * Bỏ hết các ô check
     * Created by LVKIEN 27.08.2022
     */
    removeAllFilters() {
      try {
        const checkboxItems = $(this.$refs.sidebarLeft)
          .find("[checked]")
          .toArray();
        checkboxItems.forEach((item) => {
          $(item).removeAttr("checked");
          $(item).children(".icon-item").removeClass("icon-checkbox-checked");
          $(item).children(".icon-item").addClass("icon-checkbox");
        });
        this.listFilterOption = [];
        // Khi đã search và bỏ lọc, reset lại listFilter
        if (this.isFiltered) {
          this.$store.commit("setListFilter", []);
          this.isFiltered = false;
        }
        $(this.$refs.sidebarLeft)
          .find("[current-active]")
          .removeAttr("current-active");
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Gọi mỗi khi click vào ô checkbox, xử lý bật tắt và check
     * Created by LVKIEN 5/9/2022
     */
    filterItemOnClick(e) {
      try {
        if ($(e.target).parent(".filter-option-item").length > 0) {
          // thêm vào để fix lỗi liên quan đến component combobox
          if (!$(e.target).hasClass("multiselect")) {
            const checkedItem = $(e.target).parent(".filter-option-item");
            handleClickFilterItem(checkedItem);
            const listChecked = $(this.$refs.sidebarLeft)
              .find("[checked]")
              .toArray();
            const resultCheck = listChecked.map((item) =>
              $(item).children(".item-content").text().trim()
            );
            const idResultCheck = resultCheck.map((name) => {
              let result = "";
              this.fieldOptions.forEach((field) => {
                if (name === field.name) {
                  result = field.id;
                }
              });
              return result;
            });
            this.listFilterOption = [...idResultCheck];
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    handleClickToggle(e) {
      handleToggleSideBar("sidebar-right", e, "240px");
      this.isToggle = !this.isToggle;
    },
    /**
     * lấy về dữ liệu cho Combobox
     * Created by LVKIEn 29/08/2022
     */
    async getDataComboBox(prefix) {
      try {
        let result = null;
        const response = await axiosInstance
          .get(prefix)
          .then((res) => res.data)
          .catch((error) => error.response.data);
        if (response.flag) {
          if (prefix === "Organizations/Domains") {
            result = this.formatDataComboBox(response.data);
          } else {
            result = this.formatDataComboBox(
              response.data.map((item) => {
                return {
                  id: item[Object.keys(item)[0]],
                  name: item[Object.keys(item)[1]],
                };
              })
            );
          }
          result.unshift({ value: "Không chọn", label: "Không chọn", id: 0 });
        } else {
          this.$store.commit("setState", "fail");
          this.$store.commit("setMessage", response.userMsg);
          emitter.emit("showToast");
        }
        return result;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * xử lý dữ liệu theo format của combobox
     * @param {*} data
     * Created by LVKIEn 30/08/2022
     */
    formatDataComboBox(data) {
      return data.map((item) => {
        return {
          value: item.name || item,
          label: item.name || item,
          id: item.id || item,
        };
      });
    },
  },
};
</script>
<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
.btn-icon {
  width: 16px;
  height: 16px;
}
.sidebar-left {
  padding-top: 5px;
  gap: 10px;
  position: relative;
  display: flex;
  flex-direction: column;
  border-right: 1px solid rgb(226, 228, 233);
  box-sizing: border-box;
  transition: all 0.5s linear;
}

.sidebar-title {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 15px 15px 20px 10px;
}

.sidebar-left-header.sidebar-title {
  padding-bottom: 0;
}

.sidebar-left-filter {
  overflow-x: hidden;
  overflow-y: auto;
  position: relative;
}
.sidebar-left-filter::-webkit-scrollbar-thumb:hover {
  background-color: #99a1b2;
}
.sidebar-left-filter::-webkit-scrollbar-thumb:active {
  background-color: #7c869c;
}
.filter-option-title.sidebar-title {
  padding-right: 8px;
  position: sticky;
  top: 0;
  left: 0;
  background-color: #fff;
  z-index: 1000;
}

.title-content {
  text-transform: uppercase;
  font-size: 13px;
  font-weight: 500;
}

.filter-option-list {
  display: flex;
  flex-direction: column;
}

.filter-option-item {
  display: flex;
  gap: 6px;
  align-items: center;
  flex-wrap: wrap;
  padding: 6px 8px 6px 10px;
  transition: background-color 0.2s ease;
  margin-bottom: 6px;
}
.filter-option-item[current-active]{
  background-color: #F0F2F4;

}
.dropdown {
  width: 100%;
}

.filter-option-item > .dropdown,
.filter-option-item > .multiselect,
.filter-option-item > .input-container {
  display: none;
}

.filter-option-item[checked]>.dropdown

/* ,
.filter-option-item[checked]>.multiselect,
.filter-option-item[checked]>.input-container  */ {
  display: block;
}

.item-content {
  font-weight: 500;
}

.sidebar-left-footer {
  height: 56px;
  background-color: #f0f2f4;
  padding: 12px 16px 12px 0;
  border-radius: 0 0 4px 4px;
  display: flex;
  justify-content: flex-end;
  gap: 8px;
  z-index: 100;
}

.toggle {
  background-color: #fff;
  z-index: 1000;
  position: absolute;
  width: 12px;
  height: 40px;
  top: 50%;
  right: -12px;
  border-radius: 0 8px 8px 0;
  box-shadow: 2px 2px 7px rgba(31, 34, 41, 0.16);
  box-shadow: 2px 2px 7px rgba(31, 34, 41, 0.16);
  cursor: pointer;
  display: flex;
  justify-content: center;
  align-items: center;
}

.btn-toggle {
  transition: all 0.2s ease-out;
  margin: auto;
  width: 12px;
  height: 100%;
  background-repeat: no-repeat;
  background-position: center;
  background-image: url("@/assets/img/toggle-left.svg");
}

.btn-toggle[toggle] {
  transform: rotate(180deg);
}
.icon-small {
  width: 16px;
  height: 16px;
}
.icon-search {
  background-image: url("@/assets/img/search1.svg");
}
</style>

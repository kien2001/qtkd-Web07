<template>
    <div class="sidebar-left" ref="sidebarLeft">
        <div class="sidebar-left-header sidebar-title" v-show="isToggle">
            <p class="title-content">bộ lọc đã lưu</p>
            <div class="btn-icon icon-small arrow-up"></div>
        </div>
        <div class="sidebar-left-filter" v-show="isToggle">
            <div class="filter-option-title sidebar-title">
                <p class="title-content">
                    lọc tiềm năng theo
                </p>
                <div class="icon icon-search icon-small"></div>
            </div>
            <div class="filter-option-list">
                <div class="filter-option-item" v-for="option in fieldOptions" :key="option.id" :name="option.id"
                    ref="filterOptions">
                    <div class="icon-item icon-checkbox icon icon-small"></div>
                    <div class="item-content">{{ option.name }}</div>
                    <DropDown @selected="getSelectedType" @click="showOption1" :showInput="false" :name1="option.id"
                        placeholder="Không chọn" :options="option1" ref="option1" v-if="option.type === 1"
                        :fetchDataWhenClick="false" />
                    <DropDown @selected="getSelectedType" @click="showOption2" :showInput="false" :name1="option.id"
                        placeholder="Không chọn" :options="option1" ref="option2" v-else :fetchDataWhenClick="false" />
                    <!-- <template v-if="option.type === 1 && checkType"> -->
                    <InputForm id="input" ref="input" />
                    <ComboBox :options="async () => {
                        return await this.getDataComboBox(option.url)
                    }" ref="combobox" />
                    <!-- </template> -->
                </div>
            </div>
        </div>
        <div class="sidebar-left-footer" v-if="showButton">
            <Button name="Bỏ lọc" color="#fff" colorHover="#D0D8FB" @clickBtn="removeAllFilters" />
            <Button name="Áp dụng" color="#4262F0" colorHover="#2B4EEE" colorText="#FFFFFF" @clickBtn="searchFilter" />
        </div>
        <div class="toggle">
            <div class="btn-toggle" @click="handleClickToggle"></div>
        </div>
    </div>
</template>
<script>
import $ from 'jquery'
import handleClickFilterItem from '../js/checkbox'
import { handleToggleSideBar } from '../js/toggleSideBar'
import { rootApi, fieldMappingOptions, optionType1, optionType2 } from '../js/config'
import InputForm from "../components/InputForm.vue";
import axios from 'axios'
export default {
    name: ' SideBarLeft',
    components: { InputForm },
    data() {
        return {
            isToggle: true,
            showButton: false,
            fieldOptions: [],
            listFilterOption: [],
            option1: [{ id: 1, name: "Là" }, { id: 2, name: "Chứa" }, { id: 3, name: "Không là" }, { id: 4, name: "Không chứa" }],
            option2: [{ id: 5, name: "Không trống" }, { id: 6, name: "Trống" }],
            objectResult: {},
            selectedOptionFilter: {}
        }
    },
    beforeMount() {
        // fieldMappingOptions.unshift({ id: "Tag", name: "Thẻ" })
        this.fieldOptions = fieldMappingOptions
        let objectType1 = []
        let objectType2 = []
        fieldMappingOptions.forEach(field => {
            if (field.type === 1) {
                objectType1 = [...objectType1, { name: field.id, type: '', value: '' }]
            } else if (field.type === 2) {
                objectType2 = [...objectType2, { name: field.id, type: '' }]
            }
        })
        this.objectResult = {
            1: objectType1, 2: objectType2
        }
        console.log(this.objectResult);
    },
    mounted() {
        // gán sự kiện click vào checkbox
        const filterOptionItems = $('.filter-option-item').toArray()
        filterOptionItems.forEach((item) => {
            $(item).click(this.filterItemOnClick)
        })
        // ẩn tất cả các ô input
        this.$refs.input.forEach(item => {
            $(item.$el).css("display", "none")
        })
        // ẩn tất cả các ô combobox
        this.$refs.combobox.forEach(item => {
            $(item.$el).css("display", "none")
        })
    },
    watch: {
        /**
         * Xử lý hiển thị các ô nhập dữ liệu và thanh button
         * @param {*} newValue 
         * Created by LVKIEN 3/9/2022
         */
        listFilterOption(newValue) {
            // Khi 1 option bị bỏ check, display none các phần tử trong nó
            $(".filter-option-item:not([checked])").toArray().forEach(item => {
                $(item).children(".input-container").css("display", "none")
                $(item).children(".multiselect").css("display", "none")
            })
            // bật tắt thanh button
            if (newValue.length > 0) {
                this.showButton = true
            } else {
                this.showButton = false
            }

            // console.log(newValue);
            // reset giá trị value của field khi bỏ checked
            this.objectResult[1].forEach(field => {
                let check = this.listFilterOption.includes(field[Object.keys(field)[0]])
                if (!check) {
                    field.type = ''
                    field.value = ''
                }
            })
            this.objectResult[2].forEach(field => {
                let check = this.listFilterOption.includes(field[Object.keys(field)[0]])
                if (!check) {
                    field.type = ''
                }

            })
        },
        /**
         * Xử lý ẩn hiện các ô input nhập dữ liệu khi chọn từng kiểu dropdown
         * @param {*} newValue 
         * Created by LVKIEN 3/9/2022
         */
        selectedOptionFilter(newValue) {
            // reset giá trị filter khi thay đổi dropdown filter
            this.$refs.combobox.forEach(combobox => {
                if ($(combobox.$el).parent().attr("name") === newValue.name) {
                    combobox.value = [];
                }
            })
            this.$refs.input.forEach(input => {
                if ($(input.$el).parent().attr("name") === newValue.name) {
                    input.name = ""
                }
            })
            if (newValue) {
                this.$refs.option1.forEach(item => {
                    if (newValue.name === item?.$attrs.name1) {
                        if (newValue.id % 2 === 0 || (item.$attrs.name1 === "FullName") || (item.$attrs.name1 === "Owner")) {
                            $(item.$el).siblings(".input-container").css("display", "block")
                            $(item.$el).siblings(".multiselect").css("display", "none")
                        } else {
                            $(item.$el).siblings(".input-container").css("display", "none")
                            $(item.$el).siblings(".multiselect").css("display", "block")
                        }
                    }
                })
            }
        }
    },
    methods: {
        searchFilter() {
            this.$refs.filterOptions.forEach(option => {
                if ($(option).attr("checked")) {
                    this.$refs.combobox.forEach(combobox => {
                        if (combobox.value) {
                            if ($(combobox.$el).parent().attr("name") === $(option).attr("name") && Object.keys(combobox.value).length > 0) {
                                console.log(combobox.value);
                                const indexFilterField = this.objectResult[1].findIndex(item => item[Object.keys(item)[0]] === $(option).attr("name"))
                                this.objectResult[1][indexFilterField].value = combobox.value.map(value => value.value)
                            }
                        }
                    })
                    this.$refs.input.forEach(input => {
                        if ($(input.$el).parent().attr("name") === $(option).attr("name") && input.name.trim() !== '') {
                            console.log(input.name);
                            const indexFilterField = this.objectResult[1].findIndex(item => item[Object.keys(item)[0]] === $(option).attr("name"))
                            this.objectResult[1][indexFilterField].value = input.name
                        }
                    })
                }
            })

        },
        /**
         * Lấy giá trị ô checked filter
         * @param {*} selected 
         * Created by LVKIEN 2/9/2022
         */
        getSelectedType(selected) {
            if (selected) {
                // this.checkType = true
                // this.isCheck = selected.id
                // this.operator = selected.id
                this.selectedOptionFilter = selected
                // chọn vào dropdown loại 1
                if (selected.id < 5) {
                    this.objectResult[1].forEach(object => {
                        if (object[Object.keys(object)[0]] === selected.name) {
                            object.type = selected.id
                        }
                    })
                    // chọn vào dropdown loại 2
                } else {
                    this.objectResult[2].forEach(object => {
                        if (object[Object.keys(object)[0]] === selected.name) {
                            object.type = selected.id
                        }
                    })
                }
            }
        },
        /**
         * Set các option cho dropdown
         * Created by LVKIEN 2/9/2022
         */
        showOption1() {
            this.$refs["option1"].forEach(option1 => {
                option1.setOptions(this.option1)
            })
        },
        /**
         * Set các option cho dropdown
         * Created by LVKIEN 2/9/2022
         */
        showOption2() {
            this.$refs["option2"].forEach(option2 => {
                option2.setOptions(this.option2)
            })
        },
        /**
         * Bỏ hết các ô check
         * Created by LVKIEN 27.08.2022
         */
        removeAllFilters() {
            const checkboxItems = $(this.$refs.sidebarLeft).find('[checked]').toArray()
            checkboxItems.forEach((item) => {
                $(item).removeAttr('checked')
                $(item).children('.icon-item').removeClass('icon-checkbox-checked')
                $(item).children('.icon-item').addClass('icon-checkbox')
            })
            this.listFilterOption = []
        },
        // A method that is called when the user clicks on a filter option checkbox item in the list.
        filterItemOnClick(e) {
            e.preventDefault()
            const checkedItem = $(e.target).parent('.filter-option-item')
            handleClickFilterItem(checkedItem)
            const listChecked = $(this.$refs.sidebarLeft).find('[checked]').toArray()
            const resultCheck = listChecked.map(item =>
                $(item).children(".item-content").text().trim()
            )
            const idResultCheck = resultCheck.map(name => {
                let result = ""
                this.fieldOptions.forEach(field => {
                    if (name === field.name) {
                        result = field.id
                    }
                })
                return result
            })
            this.listFilterOption = [...idResultCheck]
        },
        handleClickToggle(e) {
            handleToggleSideBar('sidebar-right', e, '240px')
            this.isToggle = !this.isToggle
        },
        /**
        * lấy về dữ liệu cho Combobox
        * Created by LVKIEn 29/08/2022
        */
        async getDataComboBox(prefix) {
            let result = null;
            const response = await axios.get(`${rootApi}${prefix}`).then(res => res.data).catch(error => error.response.data)
            if (response.flag) {
                if (prefix === "Organizations/Domains") {
                    result = this.formatDataComboBox(response.data)
                } else {
                    result = this.formatDataComboBox(response.data.map(item => {
                        const keyName = Object.keys(item)[1]
                        return item[keyName]
                    }))
                }
            } else {
                console.log(response.userMsg);
            }
            return result;
        },
        /**
        * xử lý dữ liệu theo format của combobox
        * @param {*} data 
        * Created by LVKIEn 30/08/2022
        */
        formatDataComboBox(data) {
            return data.map(item => {
                return { value: item, label: item }
            })
        },
    }
}
</script>
<style scoped>
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
    padding-bottom: 15px;
}

.sidebar-left-header.sidebar-title {
    padding: 15px 15px 20px 10px;
}

.sidebar-left-filter {

    padding: 0 15px 10px 10px;
    overflow-x: hidden;
    overflow-y: auto;
    position: relative;
}

.filter-option-title.sidebar-title {
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
    gap: 16px;
}

.filter-option-item {
    display: flex;
    gap: 6px;
    align-items: center;
    flex-wrap: wrap;
}

.dropdown {
    width: 100%;
}

.filter-option-item>.dropdown,
.filter-option-item>.multiselect,
.filter-option-item>.input-container {
    display: none;
}

.filter-option-item[checked]>.dropdown

/* ,
.filter-option-item[checked]>.multiselect,
.filter-option-item[checked]>.input-container  */
    {
    display: block;
}

.item-content {
    font-weight: 500;
}

.sidebar-left-footer {
    height: 56px;
    background-color: #F0F2F4;
    padding: 12px 16px 12px 0;
    border-radius: 0 0 4px 4px;
    display: flex;
    justify-content: flex-end;
    gap: 8px;
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
    background-image: url("../assets/img/toggle-left.svg");
}

.btn-toggle[toggle] {
    transform: rotate(180deg)
}
</style>

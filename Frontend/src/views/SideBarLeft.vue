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
                <div class="filter-option-item" v-for="option in fieldOptions" :key="option.id" ref="fieldOptions">
                    <div class="icon-item icon-checkbox icon icon-small"></div>
                    <div class="item-content">{{ option.name}}</div>
                </div>
            </div>
        </div>
        <div class="sidebar-left-footer" v-if="showButton">
            <KienButton name="Bỏ lọc" color="#fff" colorHover="#D0D8FB" @clickBtn="removeAllFilters" />
            <KienButton name="Áp dụng" color="#4262F0" colorHover="#2B4EEE" colorText="#FFFFFF" />
        </div>
        <div class="btn-icon btn-toggle toggle-left" @click="handleClickToggle"></div>
    </div>
</template>
<script>
import $ from 'jquery'
import handleClickFilterItem from '../js/checkbox'
import { handleToggleSideBar } from '../js/toggleSideBar'
import { fieldMappingOptions } from '../js/config'
export default {
    name: ' SideBarLeft',
    data() {
        return {
            isToggle: true,
            showButton:false,
            fieldOptions:[],
            listFilterOption:[]
        }
    },
    beforeMount(){
        fieldMappingOptions.unshift({ id: 1, name: "Thẻ" })
        this.fieldOptions = fieldMappingOptions
        
    },
    mounted() {
        const filterOptionItems = $('.filter-option-item').toArray()
        filterOptionItems.forEach((item) => {
            $(item).click(this.filterItemOnClick)
        })
    },
    watch:{
        listFilterOption(newValue){
            if(newValue.length>0){
                this.showButton = true
            }else{
                this.showButton = false

            }
        }
    },
    methods: {
        /**
         * Bỏ hết các ô check
         * Created by LVKIEN 27.08.2022
         */
        removeAllFilters(){
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
            console.log(this.$refs.sidebarLeft);
            const checkedItem = $(e.target).parent('.filter-option-item')
            handleClickFilterItem(checkedItem)
            const listChecked = $(this.$refs.sidebarLeft).find('[checked]').toArray()
            const resultCheck = listChecked.map(item =>
                $(item).children(".item-content").text()
            )
            this.listFilterOption = [...resultCheck]
        },
        handleClickToggle(e) {
            handleToggleSideBar('sidebar-right', e, '240px')
            this.isToggle = !this.isToggle
        }
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
}

.sidebar-title {
    height: fit-content;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding-bottom: 15px;
    overflow: hidden;
}

.sidebar-left-header.sidebar-title {
    padding: 15px 15px 20px 10px;
}

.sidebar-left-filter {

    padding: 0 15px 0 10px;
    overflow-x: hidden;
    overflow-y: auto;
    position: relative;
}

.filter-option-title.sidebar-title {
    position: sticky;
    top: 0;
    left: 0;
    background-color: #fff;
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
    overflow-x: hidden;
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
</style>

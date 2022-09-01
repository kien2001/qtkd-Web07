<template>
  <div class="function" ref="function">
    <div class="function-filter" v-if="isItemChecked">
      <div class="filter-option">
        Tất cả tiềm năng
        <div class="btn-icon small arrow-down"></div>
      </div>
      <div class="edit-btn btn-hover">Sửa</div>
      <div class=" btn-icon small btn-hover reload"></div>
    </div>
    <div class="function-filter" v-else>
      <div class="selected-item">Đã chọn <span> {{ getListIdChecked.length }}</span></div>
      <div class="edit-btn btn-hover">Bỏ chọn</div>
      <KienButton name="Cập nhật thông tin" color="#fff" colorHover="#E2E4E9" :hasIcon="true" :urlMainBtn="iconEdit"
        @clickBtn="editMultipleRow" />
      <div class="option-detail">
        <div class="btn-icon more-option" @click="showOptions"></div>
        <div class="more-option-container" v-show="showOptionList">
          <KienButton name="Xuất khẩu" color="#fff" colorHover="#E2E4E9" :hasIcon="true" :urlMainBtn="iconExport"
            @clickBtn="exportExcel" />
          <KienButton name="Xoá" color="#fff" colorHover="#E2E4E9" :hasIcon="true" :urlMainBtn="iconDelete"
            colorText="#EC4141" @clickBtn="deleteRow" />
        </div>
      </div>
    </div>
    <div class="function-manipulation">
      <div class=" ">
        <KienButton name="Thêm" color="#4262F0" colorHover="#2B4EEE" :hasIcon="true" :urlMainBtn="iconAdd"
          :isComboBtn="true" :urlSecondBtn="iconArrowDownWhite" @clickBtn="openForm" colorText="#fff" />
      </div>
      <div class="btn-icon more-option" @click="handleExport"></div>
      <div class="btn-icon more-option2">
        <div class="item-sub btn-addition icon-3dot3line"></div>
        <div class="item-sub btn-addition icon-arrow-down"></div>
      </div>
    </div>
  </div>
</template>
<script>
import $ from 'jquery'
import axios from 'axios'
import iconEdit from '../assets/img/edit.svg'
import iconAdd from '../assets/img/add.svg'
import iconExport from '../assets/img/export.svg'
import iconDelete from '../assets/img/delete.svg'
import iconArrowDownWhite from '../assets/img/arrow-down-white.svg'
import { rootApi } from '@/js/config'
import exportToExcelPro from '@/js/exportToExcel'
import { fieldOptions } from '../js/config'
import CustomerTable from '@/entities/CustomerTable'
import { capitalizeFirstLetter, lowerCaseFirstLetter } from '@/js/common'
export default {
  name: 'FunctionTool',
  data() {
    return {
      iconAdd,
      iconArrowDownWhite,
      iconEdit,
      iconExport,
      iconDelete,
      showOptionList: false,
      isItemChecked: true // biến kiểm tra xem liệu đã có row nào trong table đc check hay chưa
    }
  },
  watch: {
    getListIdChecked(newValue) {
      if (newValue.length !== 0) {
        this.isItemChecked = false
      } else {
        this.isItemChecked = true
      }
    }
  },
  computed: {
    getListIdChecked() {
      return this.$store.state.listIdChecked
    }
  },
  created() {
    $(window).click(this.exit)
  },
  methods: {
    
    // tắt thẻ div show detail option
    exit(e) {
      if ($(e.target).attr('class') !== $(this.$refs.function).find('.btn-icon.more-option').attr('class')) {
        this.showOptionList = false
      }
    },
    /**
     * Delete nhiều hàng
     * Created by LVKIEN 28/08/2022
     */
    async deleteRow() {
      const resDeleteRows = await axios.post(`${rootApi}Customers/ListCustomerId`, this.getListIdChecked)
        .then(res => res.data)
        .catch(error => error.response.data)
      if (resDeleteRows.flag) {
        alert(resDeleteRows.data)
        this.$store.commit('resetListIdChecked')
        this.$store.commit('setIsDeleted', true)
      } else {
        alert(resDeleteRows.data)
      }
    },
    exportExcel() {
      let resultData = []
      const customerTable = new CustomerTable();
      const keyCustomerTableArr = Object.keys(customerTable)
      const keyLowerArr = keyCustomerTableArr.map(key => lowerCaseFirstLetter(key) )
      if (this.$store.state.listCheckedCustomer?.length > 0 ){
        this.$store.state.listCheckedCustomer.forEach(checkedCustomer=>{
          const customerTable = new CustomerTable(); 
          keyLowerArr.forEach(key=>{
            customerTable[capitalizeFirstLetter(key)] = checkedCustomer[key]
          })
          if (checkedCustomer.lastMiddleName){
            customerTable.FullName = `${checkedCustomer.lastMiddleName.trim()} ${checkedCustomer.firstName.trim() }`
          }else{
            customerTable.FullName = checkedCustomer.firstName.trim()
          }
          resultData = [...resultData, customerTable]
        })
      }
      console.log(resultData);
      const widthArr = [
        { width: 15 },
        { width: 30 },
        { width: 20 },
        { width: 20 },
        { width: 15 },
        { width: 30 },
        { width: 30 },
        { width: 20 },
        { width: 30 },
        { width: 25 },
        { width: 25 },
        { width: 20 },
        { width: 20 },
        { width: 20 },
        { width: 20 },
        { width: 40 },
        { width: 30 },
        { width: 20 },
        { width: 30 },
        { width: 20 },
        { width: 30 }
      ]
      exportToExcelPro(resultData, "ListCustomer", "ListCustomer", "Danh sách khách hàng", fieldOptions, widthArr)
    },
    editMultipleRow() {
      this.$store.commit('setEditMultipleRow', true)
    },
    showOptions() {
      this.showOptionList = !this.showOptionList
    },
    openForm() {
      if (this.$route.name === "TiemNang") {
        this.$store.commit('setFormState', true)
        this.$store.commit('setEditForm', false)
        this.$store.commit('resetDropdown')
      }
    }
  }
}
</script>
<style scoped>
/* =======Header-footer ========== */
.function {
  background-color: #E2E4E9;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 10px;
}

.function-filter {
  height: fit-content;
  display: flex;
  gap: 10px;
}

.function-filter>div {
  align-self: center;
  /* height: inherit; */
  display: flex;
  justify-content: center;
  align-items: center;
}

.btn-icon.small.arrow-down {
  padding-left: 3px;
}

.filter-option {
  font-size: 16px;
  font-weight: 700;
  min-width: 150px;
  gap: 5px;
  cursor: pointer;
}

.option-detail {
  position: relative;
}

.more-option-container {
  display: flex;
  flex-direction: column;
  bottom: 0;
  position: absolute;
  z-index: 100;
  width: 120px;
  transform: translate(36%, 100%);
}

.more-option-container>div {
  width: 100%;
}

.edit-btn {
  color: #4262F0;
  cursor: pointer;
  font-weight: 500;
  padding: 5px 7px;
  border-radius: 5px;
}

.selected-item {
  display: flex;
  gap: 6px;
}

.selected-item>span {
  font-weight: 600;
}

.function-manipulation {
  display: flex;
  gap: 5px;
}

.more-option {
  width: 32px;
  height: 32px;
  background-color: #fff;
  background-size: 20px 20px !important;
  border-radius: 4px;
}

.more-option:hover,
.more-option:focus,
.more-option2>div:hover,
.more-option2>div:focus {
  background-color: #E2E4E9;
  border: 1px solid #D3D7DE;
  border-radius: 4px;
}

.more-option:active,
.more-option2>div:active {
  background-color: #D3D7DE;
}

.more-option2 {
  display: flex;
  width: 54px;
  background-color: #fff;
  border-radius: 4px;
}

.more-option2>.item-sub {
  width: 32px;
}
</style>

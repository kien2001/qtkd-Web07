<template>
  <div class="page-table" ref="table">
    <table class="table-container">
      <div class="table-header">
        <div class="thead">
          <div class="tr">
            <div class="th icon icon-rectangle-add"></div>
            <div class="th icon icon-checkbox" @click="checkAll" ref="check-all"></div>
            <div class="th">Thẻ</div>
            <div class="th">Xưng hô</div>
            <div class="th">Họ và tên</div>
            <div class="th">Chức danh</div>
            <div class="th">ĐT di động</div>
            <div class="th">ĐT cơ quan</div>
            <div class="th">Email cơ quan</div>
            <div class="th">Email cá nhân</div>
            <div class="th">Tổ chức</div>
            <div class="th">Địa chỉ</div>
            <div class="th">Tỉnh/Thành phố</div>
            <div class="th">Quận/Huyện</div>
            <div class="th">Phường/Xã</div>
            <div class="th">Nguồn gốc</div>
            <div class="th">Loại hình</div>
            <div class="th">Lĩnh vực</div>
            <div class="th">Mô tả</div>
            <div class="th">Bố cục</div>
            <div class="th">Chủ sở hữu</div>
            <div class="th">Doanh thu</div>
            <div class="th">Dùng chung</div>
            <div class="th">Facebook</div>
          </div>
        </div>
      </div>
      <div class="table-content" @scroll="handleHorizontalScrollBar">
        <div class="tbody">
          <div class="tr" v-for="customer in customerList" :key="customer.customerId" :key-name="customer.customerId"
            @mouseover="showEditWhenHover" @mouseleave="removeEditWhenHover">
            <div class="td" @click="openEditForm"></div>
            <div class="td icon icon-checkbox"></div>
            <div class="td"></div>
            <div class="td">{{ customer.vocativeName !== "" ? customer.vocativeName : "-" }}</div>
            <div class="td">{{ customer.fullName || "-" }}</div>
            <div class="td">{{ customer.positionName || '-' }}</div>
            <div class="td call-background">{{ customer.customerPhoneNum || '-' }}</div>
            <div class="td call-background">{{ customer.companyPhoneNum || '-' }}</div>
            <div class="td">{{ customer.companyEmail || '-' }}</div>
            <div class="td">{{ customer.customerEmail || '-' }}</div>
            <div class="td">{{ customer.organizationName || '-' }}</div>
            <div class="td">{{ customer.addressName || '-' }}</div>
            <div class="td">{{ customer.cityName || '-' }}</div>
            <div class="td">{{ customer.districtName || '-' }}</div>
            <div class="td">{{ customer.wardName || '-' }}</div>
            <div class="td">{{ customer.sourceName || '-' }}</div>
            <div class="td">{{ customer.typeName || '-' }}</div>
            <div class="td">{{ customer.domain || '-' }}</div>
            <div class="td">{{ customer.description || '-' }}</div>
            <div class="td">{{ customer.layout || '-' }}</div>
            <div class="td">{{ customer.owner || '-' }}</div>
            <div class="td">{{ customer.revenueName || '-' }}</div>
            <div class="td">{{ customer.sharingUse ? 'Có' : 'Không' }}</div>
            <div class="td">{{ customer.facebook || '-' }}</div>
          </div>
        </div>
        <Loading v-if="isLoading" />
      </div>
    </table>
    <div class="table-paging paging">
      <div class="paging-left">
        Tổng số: <strong>{{ totalCount }}</strong>
      </div>
      <div class="paging-right">
        <div class="paging-center-left">
          <DropDown @selected="getNumPerPage" id="limit-num" :showInput="false" placeholder="10 Bản ghi trên trang"
            @click="showLimitNum" ref="limit-num" :fetchDataWhenClick="false" />
        </div>
        <div class="paging-center-right">
          <div class="btn-icon medium btn-nav btn-first" @click="returnFirstPage"></div>
          <div class="btn-icon medium btn-nav btn-prev" @click="returnPrevPage"></div>
          <span><strong>{{ this.pageCurrent }} </strong> đến <strong>{{
              Math.ceil(this.totalCount / this.numberPerPage)
          }}</strong></span>
          <div class="btn-icon medium btn-nav btn-next" @click="toNextPage"></div>
          <div class="btn-icon medium btn-nav btn-last" @click="toLastPage"></div>
        </div>
      </div>
    </div>
  </div>
  <FormSubmit v-if="showForm" />
  <UpdateMultipleForm v-if="getEditMultipleForm" />
</template>
<script>
import $ from 'jquery'
import axios from 'axios'
import { rootApi } from '@/js/config'
import handleClickFilterItem from '@/js/checkbox'
export default {
  name: 'TiemNang',
  computed: {
    showForm() {
      return this.$store.state.isShowForm
    },
    getIsDeleted() {
      return this.$store.state.isDeleted
    },
    getEditMultipleForm() {
      return this.$store.state.editMultipleRow
    },
    getKeyWordSearch() {
      return this.$store.state.conditionSearch
    }
  },
  data() {
    return {
      customerList: [],
      totalCount: 0,
      numPerPage: [
        { id: 10, name: '10 Bản ghi trên trang' },
        { id: 20, name: '20 Bản ghi trên trang' },
        { id: 50, name: '50 Bản ghi trên trang' },
        { id: 100, name: '100 Bản ghi trên trang' }
      ],
      pageCurrent: 1,
      numberPerPage: 10,
      listChecked: [],
      customerEdit: {},
      isLoading:true
    }
  },
  mounted() {
    console.log(this.$refs.loading);

  },
  async beforeMount() {
    try {
      this.isLoading = true;
      const offSet = (this.pageCurrent - 1) * this.numberPerPage
      const responsePaging = await axios(`${rootApi}Customers?pageSize=${this.numberPerPage}&pageIndex=${offSet}&keyword=${this.getKeyWordSearch}`).then(res => res.data).catch(error => error.response.data)
      this.isLoading = false;
      if (!responsePaging.flag) {
        console.log(responsePaging.userMsg)
      } else {
        console.log(responsePaging)
        const { totalCount, customers } = responsePaging.data
        this.customerList = [...customers]
        this.totalCount = totalCount
      }
    } catch (error) {
      console.log(error);
    }

  },
  updated() {
    const checkboxItems = $('.td.icon.icon-checkbox').toArray()
    checkboxItems.forEach((item) => {
      $(item).click(this.checkInput)
    })
  },
  watch: {
    async getKeyWordSearch(newValue) {
      this.isLoading = true;
      this.pageCurrent = 1
      const offSet = (this.pageCurrent - 1) * this.numberPerPage
      const responsePaging = await axios(`${rootApi}Customers?pageSize=${this.numberPerPage}&pageIndex=${offSet}&keyword=${newValue}`).then(res => res.data).catch(error => error.response.data)
      this.isLoading = false;
      if (!responsePaging.flag) {
        console.log(responsePaging.userMsg)
      } else {
        console.log(responsePaging)
        const { totalCount, customers } = responsePaging.data
        this.customerList = [...customers]
        this.totalCount = totalCount
      }
    },
    // gán sự kiện click vào checkbox khi customerList thay đổi
    customerList() {
      const checkboxItems = $('.td.icon.icon-checkbox').toArray()
      checkboxItems.forEach((item) => {
        $(item).click(this.checkInput)
      })
    },
    // xử lý khi delete thành công
    async getIsDeleted(newValue) {
      if (newValue) {
        // reset lại biến isDeleted
        this.$store.commit('setIsDeleted', false)
        // bỏ hết các ô checked
        const checkAllBtn = this.$refs['check-all']
        $(checkAllBtn).removeAttr('all-checked')
        $(checkAllBtn).removeClass('icon-all-checked')
        $(checkAllBtn).addClass('icon-checkbox')

        const checkboxItems = $(this.$refs.table).find('[checked]').toArray()
        checkboxItems.forEach((item) => {
          $(item).removeAttr('checked')
          $(item).children('.td.icon').removeClass('icon-checkbox-checked')
          $(item).children('.td.icon').addClass('icon-checkbox')
          $(item).css('background-color', '#fff')
        })
        // reset mảng listchecked
        this.listChecked = []
        // gọi lại api để lấy dữ liệu customer mới nhất
        this.isLoading = true;
        this.pageCurrent = 1
        const offSet = (this.pageCurrent - 1) * this.numberPerPage
        const responsePaging = await axios(`${rootApi}Customers?pageSize=${this.numberPerPage}&pageIndex=${offSet}&keyword=${this.getKeyWordSearch}`).then(res => res.data).catch(error => error.response.data)
        this.isLoading = false;
        if (!responsePaging.flag) {
          console.log(responsePaging.userMsg)
        } else {
          console.log(responsePaging)
          const { totalCount, customers } = responsePaging.data
          this.customerList = [...customers]
          this.totalCount = totalCount
        }
      }
    },
    // thay đổi giá trị trong store khi checkList thay đổi
    listChecked(newValue) {
      let checkedResult = [];
      newValue.forEach(id => {
        const temp = this.customerList.filter(customer => {
          return customer.customerId === id
        })
        checkedResult.push(temp[0])
      })
      this.$store.commit('setListCheckedCustomer', checkedResult)
      this.$store.commit('setListIdChecked', this.listChecked)
    },
    // khi page size thay đổi, gọi lại api
    async numberPerPage() {
      this.isLoading = true;
      const offSet = (this.pageCurrent - 1) * this.numberPerPage
      const responsePaging = await axios(`${rootApi}Customers?pageSize=${this.numberPerPage}&pageIndex=${offSet}&keyword=${this.getKeyWordSearch}`).then(res => res.data).catch(error => error.response.data)
      this.isLoading = false;
      if (!responsePaging.flag) {
        console.log(responsePaging.userMsg)
      } else {
        console.log(responsePaging)
        const { totalCount, customers } = responsePaging.data
        this.customerList = [...customers]
        this.totalCount = totalCount
      }
    },
    // khi page current thay đổi, gọi lại api
    async pageCurrent() {
      this.isLoading = true;
      const offSet = (this.pageCurrent - 1) * this.numberPerPage
      const responsePaging = await axios(`${rootApi}Customers?pageSize=${this.numberPerPage}&pageIndex=${offSet}&keyword=${this.getKeyWordSearch}`).then(res => res.data).catch(error => error.response.data)
      this.isLoading = false;
      if (!responsePaging.flag) {
        console.log(responsePaging.userMsg)
      } else {
        console.log(responsePaging)
        const { totalCount, customers } = responsePaging.data
        this.customerList = [...customers]
      }
    }
  },
  methods: {
    openEditForm(e) {
      if (this.listChecked.length === 0) {
        this.$store.commit('setFormState', true)
        this.$store.commit('setEditForm', true)
        const customerEditId = $(e.target).parent(".tr").attr("key-name");
        this.customerEdit = this.customerList.find(customer => customer.customerId === customerEditId)
      }
      // this.$store.commit("resetDropdown")
    },
    showEditWhenHover(e) {
      if ($(e.target).parent('.tr').attr('class') === $(this.$refs.table).find('.tbody .tr').attr('class')) {
        // nếu ko có ô nào đc check mới hiện nút edit
        if (this.listChecked.length === 0) {
          $(e.target).parent('.tr').find('.td:first-child').addClass('icon-edit')
        } else {
          $(this.$refs.table).find('.tbody .td:first-child').removeClass('icon-edit')
        }
      } else if ($(e.target).attr('class') === $(this.$refs.table).find('.tbody .tr').attr('class')) {
        // nếu ko có ô nào đc check mới hiện nút edit
        if (this.listChecked.length === 0) {
          $(e.target).find('.td:first-child').addClass('icon-edit')
        } else {
          $(this.$refs.table).find('.tbody .td:first-child').removeClass('icon-edit')
        }
      }
    },
    removeEditWhenHover() {
      // nếu ko có ô nào đc check mới hiện nút edit
      if (this.listChecked.length === 0) {
        $(this.$refs.table).find('.tbody .td:first-child').removeClass('icon-edit')
      }
    },
    // xử lý check all row
    checkAll() {
      const checkAllBtn = this.$refs['check-all']
      // khi bỏ check all
      if ($(checkAllBtn).attr('all-checked') === '') {
        $(checkAllBtn).removeAttr('all-checked')
        $(checkAllBtn).removeClass('icon-all-checked')
        $(checkAllBtn).addClass('icon-checkbox')

        const checkboxItems = $(this.$refs.table).find('[checked]').toArray()
        checkboxItems.forEach((item) => {
          $(item).removeAttr('checked')
          $(item).children('.td.icon').removeClass('icon-checkbox-checked')
          $(item).children('.td.icon').addClass('icon-checkbox')
          $(item).css('background-color', '#fff')
        })
        const listChecked = $(this.$refs.table).find('[checked]').toArray()
        if (listChecked.length <= 0) {
          this.listChecked = []
        }
        // khi check all
      } else {
        $(checkAllBtn).attr('all-checked', '')
        $(checkAllBtn).removeClass('icon-checkbox')
        $(checkAllBtn).addClass('icon-all-checked')

        const checkboxItems = $(this.$refs.table).find('.tbody .tr').toArray()
        checkboxItems.forEach((item) => {
          $(item).attr('checked', '')
          $(item).children('.td.icon').removeClass('icon-checkbox')
          $(item).children('.td.icon').addClass('icon-checkbox-checked')
          $(item).css('background-color', '#FAD1D1')
        })
        const listChecked = $(this.$refs.table).find('[checked]').toArray()
        const resultCheck = listChecked.map(item =>
          $(item).attr('key-name')
        )
        this.listChecked = [...resultCheck]
      }
    },
    // show value dropdown
    showLimitNum() {
      console.log(this.numPerPage)
      this.$refs['limit-num'].setOptions(this.numPerPage)
    },
    // xử lý check cho từng row
    checkInput(e) {
      e.preventDefault()
      const checkedItem = $(e.target).parent('.tr')
      const checkAllBtn = this.$refs['check-all']
      handleClickFilterItem(checkedItem)
      // remove all edit icon
      $(this.$refs.table).find('.tbody .td:first-child').removeClass('icon-edit')

      const listChecked = $(this.$refs.table).find('[checked]').toArray()
      const resultCheck = listChecked.map(item =>
        $(item).attr('key-name')
      )
      this.listChecked = [...resultCheck]
      // khi có ít nhất 1 row đc check, enable checkall
      if (listChecked.length > 0) {
        $(checkAllBtn).attr('all-checked', '')
        $(checkAllBtn).removeClass('icon-checkbox')
        $(checkAllBtn).addClass('icon-all-checked')
        // khi ko có row nào đc check, disable checkall
      } else {
        $(checkAllBtn).removeAttr('all-checked')
        $(checkAllBtn).removeClass('icon-all-checked')
        $(checkAllBtn).addClass('icon-checkbox')
      }
    },
    handleHorizontalScrollBar(e) {
      const translateXHead = $(e.target).scrollLeft()
      if (translateXHead > 0) {
        $('.table-header').css('transform', `translateX(-${translateXHead}px)`)
      } else if (translateXHead < 40) {
        $('.table-header').css('transform', 'translateX(0)')
      }
    },
    getNumPerPage(value) {
      if (!isNaN(value)) {
        this.numberPerPage = value
        this.pageCurrent = 1
      }
    },
    returnFirstPage() {
      this.pageCurrent = 1
    },
    returnPrevPage() {
      if (this.pageCurrent > 1) {
        this.pageCurrent = this.pageCurrent - 1
      } else {
        this.pageCurrent = 1
      }
    },
    toNextPage() {
      if (this.pageCurrent < Math.ceil(this.totalCount / this.numberPerPage)) {
        this.pageCurrent = this.pageCurrent + 1
      } else {
        this.pageCurrent = Math.ceil(this.totalCount / this.numberPerPage)
      }
    },
    toLastPage() {
      this.pageCurrent = Math.ceil(this.totalCount / this.numberPerPage)
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>

.page-table {
  height: 100%;
  width: 100%;
  position: relative;
  display: grid;
  grid-template-columns: 100%;
  grid-template-rows: auto 50px;
}

.paging-center-left {
  width: 170px;
}

div.paging-center-left .dropdown .dropdown-container {
  bottom: 33px !important;
}
.table-content{
  position: relative;
}
</style>

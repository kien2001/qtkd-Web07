<template>
  <div class="page-table" ref="table">
    <table class="table-container">
      <div class="table-header">
        <div class="thead">
          <div class="tr">
            <div class="th icon icon-rectangle-add"></div>
            <div
              class="th icon icon-checkbox"
              @click="checkAll"
              ref="check-all"
            ></div>
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
          <div
            class="tr"
            v-for="customer in customerList"
            :key="customer.customerId"
            :key-name="customer.customerId"
            @mouseover="showEditWhenHover"
            @mouseleave="removeEditWhenHover"
          >
            <div class="td" @click="openEditForm"></div>
            <div class="td icon icon-checkbox"></div>
            <div class="td"></div>
            <div class="td">
              {{ customer.vocativeName !== "" ? customer.vocativeName : "-" }}
            </div>
            <div class="td">{{ customer.fullName || "-" }}</div>
            <div class="td">{{ customer.positionName || "-" }}</div>
            <div class="td call-background">
              {{ customer.customerPhoneNum || "-" }}
            </div>
            <div class="td call-background">
              {{ customer.companyPhoneNum || "-" }}
            </div>
            <div class="td">{{ customer.companyEmail || "-" }}</div>
            <div class="td">{{ customer.customerEmail || "-" }}</div>
            <div class="td">{{ customer.organizationName || "-" }}</div>
            <div class="td">{{ customer.addressName || "-" }}</div>
            <div class="td">{{ customer.cityName || "-" }}</div>
            <div class="td">{{ customer.districtName || "-" }}</div>
            <div class="td">{{ customer.wardName || "-" }}</div>
            <div class="td">{{ customer.sourceName || "-" }}</div>
            <div class="td">{{ customer.typeName || "-" }}</div>
            <div class="td">{{ customer.domain || "-" }}</div>
            <div class="td">{{ customer.description || "-" }}</div>
            <div class="td">{{ customer.layout || "-" }}</div>
            <div class="td">{{ customer.owner || "-" }}</div>
            <div class="td">{{ customer.revenueName || "-" }}</div>
            <div class="td">{{ customer.sharingUse ? "Có" : "Không" }}</div>
            <div class="td" v-if="customer.facebook">
              <a
                :href="customer.facebook"
                target="_blank"
                :style="{
                  color: 'hsl(206,100%,40%)',
                  textDecoration: 'underline',
                }"
                >{{ customer.facebook }}</a
              >
            </div>
            <div class="td" v-else>-</div>
          </div>
        </div>
        <div v-if="showNoValue" class="not-value">Không có dữ liệu</div>
      </div>
      <TheLoading v-if="isLoading" position="absolute" />
    </table>
    <div class="table-paging paging">
      <div class="paging-left">
        Tổng số: <strong>{{ totalCount }}</strong>
      </div>
      <div class="paging-right">
        <div class="paging-center-left">
          <TheDropDown
            @selected="getNumPerPage"
            id="limit-num"
            :showInput="false"
            placeholder="10 Bản ghi trên trang"
            @mousedown="showLimitNum"
            ref="limit-num"
            color="#1f2229"
            :fetchDataWhenClick="false"
          />
        </div>
        <div class="paging-center-right">
          <div
            class="btn-icon medium btn-nav btn-first"
            @click="returnFirstPage"
          ></div>
          <div
            class="btn-icon medium btn-nav btn-prev"
            @click="returnPrevPage"
          ></div>
          <span
            ><strong
              >{{ (this.pageCurrent - 1) * this.numberPerPage + 1 }}
            </strong>
            đến <strong>{{ getLastPage }}</strong></span
          >
          <div
            class="btn-icon medium btn-nav btn-next"
            @click="toNextPage"
          ></div>
          <div
            class="btn-icon medium btn-nav btn-last"
            @click="toLastPage"
          ></div>
        </div>
      </div>
    </div>
    <Transition name="fade">
      <TheFormSubmit v-if="showForm" />
    </Transition>
    <Transition duration="550" name="nested">
      <TheUpdateMultipleForm v-if="getEditMultipleForm" />
    </Transition>
  </div>
</template>
<script>
import $ from "jquery";
import axiosInstance from "@/js/axios";
import { numPerPage } from "@/js/config";
import handleClickFilterItem from "@/js/checkbox";
import emitter from "@/js/emitter";
export default {
  name: "Potential",
  computed: {
    getListToast() {
      return this.listToast;
    },
    /**
     * Đi đến cuối của pageSize
     * Created by LVKIEN 30/08/2022
     */
    getLastPage() {
      if (this.pageCurrent * this.numberPerPage > this.totalCount) {
        return this.totalCount;
      }
      return this.pageCurrent * this.numberPerPage;
    },
    /**
     * Mở form edit khi click vào ô sửa ở từng dòng
     * Created by LVKIEN 30/08/2022
     */
    showForm() {
      return this.$store.state.isShowForm;
    },
    /**
     * lấy trạng thái của isDeleted
     * Created by LVKIEN 30/08/2022
     */
    getIsDeleted() {
      return this.$store.state.isDeleted;
    },
    /**
     * lấy trạng thái của isInserted
     * Created by LVKIEN 30/08/2022
     */
    getIsInserted() {
      return this.$store.state.isInserted;
    },
    /**
     * lấy trạng thái của isUpdated
     * Created by LVKIEN 30/08/2022
     */
    getIsUpdated() {
      return this.$store.state.isUpdated;
    },
    /**
     * lấy trạng thái của editMultipleRow
     * Created by LVKIEN 30/08/2022
     */
    getEditMultipleForm() {
      return this.$store.state.editMultipleRow;
    },
    /**
     * lấy giá trị của từ khoá search
     * Created by LVKIEN 30/08/2022
     */
    getKeyWordSearch() {
      return this.$store.state.conditionSearch;
    },
    /**
     * lấy giá trị của list filter
     * Created by LVKIEN 30/08/2022
     */
    getListFilter() {
      return this.$store.state.listFilter;
    },
  },
  data() {
    return {
      customerList: [],
      totalCount: 0,
      pageCurrent: 1,
      numberPerPage: 10,
      listChecked: [],
      isLoading: true,
      // hiển thị khi không có dữ liệu
      showNoValue: false,
    };
  },
  /**
   * Lấy dữ liệu của phân trang
   * Created by LVKIEN 30/08/2022
   */
  async beforeMount() {
    await this.getDataPaging(this.getKeyWordSearch, this.getListFilter);
  },
  /**
   * TODO: Đăng ký sự kiện global
   * !Created by LVKIEN 10/09/2022
   */
  created() {
    emitter.on("deselectAll", this.removeCheckAll);
  },
  /**
   * Thêm sự kiện cho checkbox
   * Created by LVKIEN 30/08/2022
   */
  updated() {
    const checkboxItems = $(".td.icon.icon-checkbox").toArray();
    checkboxItems.forEach((item) => {
      $(item).unbind();
      $(item).click(this.checkInput);
    });
  },
  mounted() {
    this.$refs["limit-num"].selected = {
      id: 10,
      name: "10 Bản ghi trên trang",
    };
    $(this.$refs["limit-num"]?.$el)
      .find(".dropdown-header")
      .css("color", "#1F2229");
  },
  watch: {
    /**
     * Khi list filter thay đổi, gọi API
     * @param {*} newValue
     *  Created by LVKIEN 30/08/2022
     */
    async getListFilter(newValue) {
      this.pageCurrent = 1;
      // this.listToast = [...this.listToast, {message: "failt", state: "fail"}]
      await this.getDataPaging(this.getKeyWordSearch, newValue);
    },
    /**
     * Khi keyword thay đổi, gọi API
     * @param {*} newValue
     *  Created by LVKIEN 30/08/2022
     */
    async getKeyWordSearch(newValue) {
      this.pageCurrent = 1;
      await this.getDataPaging(newValue, this.getListFilter);
    },
    /**
     * xử lý khi delete thành công
     * @param {*} newValue
     *  Created by LVKIEN 30/08/2022
     */
    async getIsDeleted(newValue) {
      if (newValue) {
        // reset lại biến isDeleted
        this.$store.commit("setIsDeleted", false);
        // bỏ hết các ô checked
        const checkAllBtn = this.$refs["check-all"];
        $(checkAllBtn).removeAttr("all-checked");
        $(checkAllBtn).removeClass("icon-all-checked");
        $(checkAllBtn).addClass("icon-checkbox");
        const checkboxItems = $(this.$refs.table).find("[checked]").toArray();
        checkboxItems.forEach((item) => {
          $(item).removeAttr("checked");
          $(item).children(".td.icon").removeClass("icon-checkbox-checked");
          $(item).children(".td.icon").addClass("icon-checkbox");
          $(item).css("background-color", "#fff");
        });
        // reset mảng listchecked
        this.listChecked = [];
        // gọi lại api để lấy dữ liệu customer mới nhất
        this.pageCurrent = 1;
        await this.getDataPaging(this.getKeyWordSearch, this.getListFilter);
      }
    },
    /**
     * xử lý khi insert thành công
     * @param {*} newValue
     * Created by LVKIEN 30/08/2022
     */
    async getIsUpdated(newValue) {
      if (newValue) {
        // reset lại biến isUpdated
        this.$store.commit("setIsUpdated", false);
        // gọi lại api để lấy dữ liệu customer mới nhất
        await this.getDataPaging(this.getKeyWordSearch, this.getListFilter);
      }
    },
    /**
     * TODO: xử lý khi insert thành công
     * @param {*} newValue
     * !Created by LVKIEN 10/09/2022
     */
    async getIsInserted(newValue) {
      if (newValue) {
        // reset lại biến isInserted
        this.$store.commit("setIsInserted", false);
        // gọi lại api để lấy dữ liệu customer mới nhất
        await this.getDataPaging(this.getKeyWordSearch, this.getListFilter);
      }
    },
    /**
     * TODO: thay đổi giá trị trong store khi checkList thay đổi
     * @param {*} newValue
     * !Created by LVKIEN 10/9/2022
     */
    listChecked(newValue) {
      let checkedResult = [];
      newValue.forEach((id) => {
        const temp = this.customerList.filter((customer) => {
          return customer.customerId === id;
        });
        checkedResult.push(temp[0]);
      });
      this.$store.commit("setListCheckedCustomer", checkedResult);
      this.$store.commit("setListIdChecked", this.listChecked);
    },
    /**
     * TODO: khi page size thay đổi, gọi lại api
     * !Created by LVKIEN 10/9/2022
     */
    async numberPerPage() {
      await this.getDataPaging(this.getKeyWordSearch, this.getListFilter);
    },
    /**
     * TODO:khi page current thay đổi, gọi lại api
     * !Created by LVKIEN 10/9/2022
     */
    async pageCurrent() {
      await this.getDataPaging(this.getKeyWordSearch, this.getListFilter);
    },
  },
  methods: {
    /**
     * Gọi API phân trang
     * @param {*} keySearch
     * created by LVKIEN 29/08/2022
     */
    async getDataPaging(keySearch, listFilter) {
      try {
        this.isLoading = true;
        const offSet = (this.pageCurrent - 1) * this.numberPerPage;
        const responsePaging = await axiosInstance({
          method: "put",
          url: "Customers",
          params: {
            pageSize: this.numberPerPage,
            pageIndex: offSet,
            keyword: keySearch.trim(),
          },
          data: listFilter,
        })
          .then((res) => res.data)
          .catch((error) => error.response.data);
        this.isLoading = false;
        if (!responsePaging.flag) {
          this.showNoValue = true;
          this.customerList = [];
          this.totalCount = 0;
          this.$store.commit("setState", "fail");
          this.$store.commit("setMessage", responsePaging.userMsg[0]);
          emitter.emit("showToast");
        } else {
          this.showNoValue = false;
          const { totalCount, customers } = responsePaging.data;
          this.customerList = [...customers];
          this.totalCount = totalCount;
        }
      } catch (error) {
        this.$store.commit("setState", "fail");
        this.$store.commit("setMessage", error);
        emitter.on("showToast");
      }
    },
    /**
     * TODO: Mở form edit
     * @param {*} e
     * !created by LVKIEN 28/08/2022
     */
    openEditForm(e) {
      if (this.listChecked.length === 0) {
        this.$store.commit("setFormState", true);
        this.$store.commit("setEditForm", true);
        const customerEditId = $(e.target).parent(".tr").attr("key-name");
        const customerEdit = this.customerList.find(
          (customer) => customer.customerId === customerEditId
        );
        this.$store.commit("setCustomerUpdated", customerEdit);
      }
    },
    /**
     * TODO: Hiện button edit khi hover vào 1 dòng
     * @param {*} e
     * !Created by LVKIEN 10/9/2022
     */
    showEditWhenHover(e) {
      if (
        $(e.target).parent(".tr").attr("class") ===
        $(this.$refs.table).find(".tbody .tr").attr("class")
      ) {
        // nếu ko có ô nào đc check mới hiện nút edit
        if (this.listChecked.length === 0) {
          $(e.target)
            .parent(".tr")
            .find(".td:first-child")
            .addClass("icon-edit");
        } else {
          $(this.$refs.table)
            .find(".tbody .td:first-child")
            .removeClass("icon-edit");
        }
      } else if (
        $(e.target).attr("class") ===
        $(this.$refs.table).find(".tbody .tr").attr("class")
      ) {
        // nếu ko có ô nào đc check mới hiện nút edit
        if (this.listChecked.length === 0) {
          $(e.target).find(".td:first-child").addClass("icon-edit");
        } else {
          $(this.$refs.table)
            .find(".tbody .td:first-child")
            .removeClass("icon-edit");
        }
      }
    },
    /**
     * TODO: Xử lý khi có 1 bản ghi được check, không hiện button edit nữa
     * !Created by LVKIEN 10/9/2022
     */
    removeEditWhenHover() {
      // nếu ko có ô nào đc check mới hiện nút edit
      if (this.listChecked.length === 0) {
        $(this.$refs.table)
          .find(".tbody .td:first-child")
          .removeClass("icon-edit");
      }
    },
    /**
     * TODO: Bỏ check all của table
     * !Created by LVKIEN 10/9/2022
     */
    removeCheckAll() {
      const checkAllBtn = this.$refs["check-all"];
      // khi bỏ check all
      if ($(checkAllBtn).attr("all-checked") === "") {
        $(checkAllBtn).removeAttr("all-checked");
        $(checkAllBtn).removeClass("icon-all-checked");
        $(checkAllBtn).addClass("icon-checkbox");
        const checkboxItems = $(this.$refs.table).find("[checked]").toArray();
        checkboxItems.forEach((item) => {
          $(item).removeAttr("checked");
          $(item).children(".td.icon").removeClass("icon-checkbox-checked");
          $(item).children(".td.icon").addClass("icon-checkbox");
          $(item).css("background-color", "#fff");
        });
        const listChecked = $(this.$refs.table).find("[checked]").toArray();
        if (listChecked.length <= 0) {
          this.listChecked = [];
        }
      }
    },
    /**
     * TODO: xử lý check all row
     * !Created by LVKIEN 6/9/2022
     */
    checkAll() {
      const checkAllBtn = this.$refs["check-all"];
      // khi bỏ check all
      if ($(checkAllBtn).attr("all-checked") === "") {
        $(checkAllBtn).removeAttr("all-checked");
        $(checkAllBtn).removeClass("icon-all-checked");
        $(checkAllBtn).addClass("icon-checkbox");
        const checkboxItems = $(this.$refs.table).find("[checked]").toArray();
        checkboxItems.forEach((item) => {
          $(item).removeAttr("checked");
          $(item).children(".td.icon").removeClass("icon-checkbox-checked");
          $(item).children(".td.icon").addClass("icon-checkbox");
        });
        const listChecked = $(this.$refs.table).find("[checked]").toArray();
        if (listChecked.length <= 0) {
          this.listChecked = [];
        }
      }
      // khi check all
      else {
        $(checkAllBtn).attr("all-checked", "");
        $(checkAllBtn).removeClass("icon-checkbox");
        $(checkAllBtn).addClass("icon-all-checked");
        const checkboxItems = $(this.$refs.table).find(".tbody .tr").toArray();
        checkboxItems.forEach((item) => {
          $(item).attr("checked", "");
          $(item).children(".td.icon").removeClass("icon-checkbox");
          $(item).children(".td.icon").addClass("icon-checkbox-checked");
        });
        const listChecked = $(this.$refs.table).find("[checked]").toArray();
        const resultCheck = listChecked.map((item) => $(item).attr("key-name"));
        this.listChecked = [...resultCheck];
      }
    },
    /**
     * TODO: show value dropdown
     * !Created by LVKIEN 10/09/2022
     */
    showLimitNum() {
      this.$refs["limit-num"].setOptions(numPerPage);
    },
    /**
     * TODO: xử lý check cho từng row
     * @param {*} e
     * !Created by LVKIEN 10/09/2022
     */
    checkInput(e) {
      console.log(e.target);
      e.preventDefault();
      const checkedItem = $(e.target).parent(".tr");
      const checkAllBtn = this.$refs["check-all"];
      handleClickFilterItem(checkedItem);
      // remove all edit icon
      $(this.$refs.table)
        .find(".tbody .td:first-child")
        .removeClass("icon-edit");
      const listChecked = $(this.$refs.table).find("[checked]").toArray();
      const resultCheck = listChecked.map((item) => $(item).attr("key-name"));
      this.listChecked = [...resultCheck];
      // khi có ít nhất 1 row đc check, enable checkall
      if (listChecked.length > 0) {
        $(checkAllBtn).attr("all-checked", "");
        $(checkAllBtn).removeClass("icon-checkbox");
        $(checkAllBtn).addClass("icon-all-checked");
        // khi ko có row nào đc check, disable checkall
      } else {
        $(checkAllBtn).removeAttr("all-checked");
        $(checkAllBtn).removeClass("icon-all-checked");
        $(checkAllBtn).addClass("icon-checkbox");
      }
    },
    /**
     * TODO: Xử lý hiển thị cho bảng table
     * @param {*} e
     * !Created by LVKIEN 10/09/2022
     */
    handleHorizontalScrollBar(e) {
      const translateXHead = $(e.target).scrollLeft();
      if (translateXHead > 0) {
        $(".table-header").css("transform", `translateX(-${translateXHead}px)`);
      } else if (translateXHead < 40) {
        $(".table-header").css("transform", "translateX(0)");
      }
    },
    /**
     * TODO: Lấy giá trị của page size
     * @param {*} value
     * !Created by LVKIEN 10/09/2022
     */
    getNumPerPage(value) {
      if (!isNaN(value.id)) {
        this.numberPerPage = value.id;
        this.pageCurrent = 1;
      }
    },
    /**
     * TODO: Xử lý khi click vào button trở về đầu trang
     * !Created by LVKIEN 10/09/2022
     */
    returnFirstPage() {
      this.pageCurrent = 1;
      // if (this.pageCurrent <= this.numberPerPage) {
      //     this.pageCurrent = 1;
      // }
      // else {
      //     this.pageCurrent = this.pageCurrent - this.numberPerPage;
      // }
    },
    /**
     * TODO: Xử lý khi click vào button trở về trang trước
     * !Created by LVKIEN 10/09/2022
     */
    returnPrevPage() {
      if (this.pageCurrent * this.numberPerPage <= this.numberPerPage) {
        this.pageCurrent = 1;
      } else {
        this.pageCurrent = this.pageCurrent - 1;
      }
      // if (this.pageCurrent > 1) {
      //     this.pageCurrent = this.pageCurrent - 1;
      // }
      // else {
      //     this.pageCurrent = 1;
      // }
    },
    /**
     * TODO: Xử lý khi click vào button sang trang kế tiếp
     * !Created by LVKIEN 10/09/2022
     */
    toNextPage() {
      if (this.pageCurrent * this.numberPerPage < this.totalCount) {
        this.pageCurrent = this.pageCurrent + 1;
      } else {
        this.pageCurrent = this.pageCurrent;
      }
    },
    /**
     * TODO: Xử lý khi click vào button chuyển về trang cuối
     * !Created by LVKIEN 10/09/2022
     */
    toLastPage() {
      this.pageCurrent = Math.ceil(this.totalCount / this.numberPerPage);
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

.nested-enter-active,
.nested-leave-active {
  transition: all 0.3s ease-in-out;
}
/* delay leave of parent element */
.nested-leave-active {
  transition-delay: 0.25s;
}

.nested-enter-from,
.nested-leave-to {
  /* transform: translateY(-200px); */
  opacity: 0;
}

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

.table-content {
  position: relative;
}

.not-value {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 100%;
  background-color: #fff;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>

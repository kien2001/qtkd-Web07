<template>
  <div class="header">
    <div class="header-logo">
      <div class="header-icon-left">
        <div class="icon-toggle"></div>
        <div class="icon-logo"></div>
      </div>
      <div class="header-search">
        <input
          class="input input-icon icon-search"
          type="text"
          @keyup.prevent="handleSearch"
          placeholder="Tìm kiếm tiềm năng, liên hệ, khách hàng"
          ref="searchInput"
        />
      </div>
      <div class="header-icon-right">
        <div class="btn-icon btn-menu-header facebook"></div>
        <div class="btn-icon btn-menu-header lapphuongtimkiem"></div>
        <div class="btn-icon btn-menu-header lich"></div>
        <div class="btn-icon btn-menu-header chuong"></div>
        <div class="btn-icon btn-menu-header caidat"></div>
        <div class="btn-icon btn-menu-header tronchanhoi"></div>
        <div class="btn-icon btn-menu-header default-avt"></div>
      </div>
    </div>
    <div class="header-menu">
      <router-link to="/">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-home"></div>
          <span class="item-content">Bàn làm việc</span>
        </div>
      </router-link>

      <router-link to="/potential">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-tiemnang"></div>
          <span class="item-content">Tiềm năng</span>
        </div>
      </router-link>
      <router-link to="/offer">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-chaohang"></div>
          <span class="item-content">Chào hàng</span>
        </div>
      </router-link>
      <router-link to="/contact">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-lienhe"></div>
          <span class="item-content">Liên hệ</span>
        </div>
      </router-link>
      <router-link to="/customer">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-khachhang"></div>
          <span class="item-content">Khách hàng</span>
        </div>
      </router-link>
      <router-link to="/opportunity">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-cohoi"></div>
          <span class="item-content">Cơ hội</span>
        </div>
      </router-link>
      <router-link to="/quote">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-baogia"></div>
          <span class="item-content">Báo giá</span>
        </div>
      </router-link>
      <router-link to="/order">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-donhang"></div>
          <span class="item-content">Đơn hàng</span>
        </div>
      </router-link>
      <router-link to="/invoice">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-hoadon"></div>
          <span class="item-content">Hoá đơn</span>
        </div>
      </router-link>
      <router-link to="/pondOpportunity">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-aocohoi"></div>
          <span class="item-content">Ao cơ hội</span>
        </div>
      </router-link>
      <router-link to="/activity">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-hoatdong"></div>
          <span class="item-content">Hoạt động</span>
        </div>
      </router-link>
      <router-link to="/target">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-muctieu"></div>
          <span class="item-content">Mục tiêu</span>
        </div>
      </router-link>
      <router-link to="/campaign">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-chiendich"></div>
          <span class="item-content">Chiến dịch</span>
        </div>
      </router-link>
      <router-link to="/report">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-baocao"></div>
          <span class="item-content">Báo cáo</span>
        </div>
      </router-link>
      <router-link to="/other">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-khac"></div>
          <span class="item-content">Khác</span>
        </div>
      </router-link>
      <div class="header-menu-item btn-icon-text" id="other">
        <div class="item-icon icon-khac"></div>
        <span class="item-content">Khác</span>
        <div class="other-menu-item">
          <router-link to="/activity">
            <div class="header-menu-item btn-icon-text">
              <div class="item-icon icon-hoatdong"></div>
              <span class="item-content">Hoạt động</span>
            </div>
          </router-link>
          <router-link to="/target">
            <div class="header-menu-item btn-icon-text">
              <div class="item-icon icon-muctieu"></div>
              <span class="item-content">Mục tiêu</span>
            </div>
          </router-link>
          <router-link to="/campaign">
            <div class="header-menu-item btn-icon-text">
              <div class="item-icon icon-chiendich"></div>
              <span class="item-content">Chiến dịch</span>
            </div>
          </router-link>
          <router-link to="/report">
            <div class="header-menu-item btn-icon-text">
              <div class="item-icon icon-baocao"></div>
              <span class="item-content">Báo cáo</span>
            </div>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import $ from "jquery";
export default {
  name: "HeaderComp",
  data() {
    return {
      oldClassImage: "",
      newClassImage: "",
      timeout: null,
      timer: 1000,
    };
  },
  mounted() {
    $(window).click((e) => {
      if (
        $(e.target).parent().has(".other-menu-item").length === 0 ||
        $(e.target).parents(".header-menu").length === 0
      ) {
        $(".other-menu-item").css("display", "none");
      } else {
        $(".other-menu-item").css("display", "flex");
      }
    });
    const headerMenuItems = $(".header-menu-item").toArray();
    headerMenuItems.forEach((item) => {
      $(item).click(this.menuItemOnClick);
    });
  },
  methods: {
    /**
     * TODO:truyền giá trị key search lên store mỗi timer sau khi người dùng gõ
     * !Created by LVKIEN 1/9/2022
     */
    handleSearch() {
      let me = this;
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        me.$store.commit("setConditionSearch", $(this.$refs.searchInput).val());
      }, this.timer);
    },
  },
};
</script>
<style scoped>
.header {
  display: flex;
  flex-direction: column;
  --imgUrl: url("../../assets/img/icon_toggle.svg");
}

/* =======Header-logo ========== */
.header-logo {
  padding: 5px;
  padding-left: 10px;
  flex-basis: 45px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0px 4px #fff;
}

.header-icon-left {
  display: flex;
  gap: 10px;
}

.header-icon-left .icon-toggle {
  align-self: center;
  width: 24px;
  height: 24px;
  background-image: url("../../assets/img/icon_toggle.svg");
  background-repeat: no-repeat;
  cursor: pointer;
}

.header-icon-left .icon-logo {
  align-self: center;
  background-image: url("../../assets/img/Logo_Ban_hang.svg");
  background-repeat: no-repeat;
  background-size: contain;
  background-position: center;
  min-width: 124px;
  height: 32px;
  cursor: pointer;
}

.header-search {
  flex-basis: 310px;
  height: 32px;
}

.header-icon-right {
  height: 32px;
  flex-basis: 250px;
  display: flex;
  justify-content: flex-end;
  align-items: center;
  gap:10px;
}
.btn-menu-header {
  background-repeat: no-repeat;
    background-position: center;
    cursor: pointer;
  width: 20px;
    height: 20px;
    background-size: contain;
}
.btn-menu-header:last-child{
  width: 40px;
    height: 40px;
}
.input {
    border-radius: 4px;
    border-width: 1px;
    border-style: solid;
    border-color: #F0F2F4;
    outline: none;
    width: 100%;
    height: inherit;
    padding: 0 3px;
    background-color: #F0F2F4;
    font-size: 13px;
    line-height: 16px;
    font-weight: 400;
    color: #1F2229;
}

.input:hover {
    border-color: #7C869C;
}

.input:focus {
    border-color: #4262F0;
}

.input::placeholder {
    font-size: 13px;
    color: #333;
}

.icon-search {
    background-image: url("../../assets/img/search1.svg");
}

.input-icon {
    background-repeat: no-repeat;
    background-position-x: 8px;
    background-position-y: center;
    background-size: 16px 16px;
    padding-left: 32px;
}
/* =======Header-menu ========== */
.header-menu {
  z-index:1;
  display: flex;
  align-items: center;
  flex-basis: 31px;
  padding-left: 10px;
  box-shadow: 0px 4px 8px -4px rgba(31, 34, 41, 0.16);
}

.header-menu > a {
  height: 100%;
  margin: 0 9px;
}

.header-menu > a:first-child {
  margin-left: 0;
}

.header-menu-item {
  position: relative;
  height: 100%;
  align-items: center;
}
.header-menu-item::before
{
  content: '';
  position: absolute;
  bottom: 0px;
  height: 1.5px;
  width: 100%;
  transform: scaleX(0);
  background-color: #5a76f2;
  transition: all .2s cubic-bezier(0.4, 0, 0.2, 1);
}

.header-menu a:not(.router-link-active.router-link-exact-active) .header-menu-item:hover::before
{
  transform: scaleX(1);
}

.router-link-active.router-link-exact-active .header-menu-item::before{
  transform: scaleX(1);
}

.router-link-active.router-link-exact-active .header-menu-item > .item-content{
    color: #2B4EEE !important;
}

.header-menu > .header-menu-item:last-child {
  display: none;
  position: relative;
}

.header-menu > .header-menu-item:last-child > .other-menu-item {
  position: absolute;
  height: fit-content;
  padding: 4px;
  transform: translateY(60%);
  min-width: 150px;
  z-index: 2000;
  display: none;
  flex-direction: column;
  background-color: #fff;
  border-radius: 4px;
  box-shadow: -2px 2px 7px rgba(31, 34, 41, 0.16);
}
.other-menu-item .router-link-active.router-link-exact-active .header-menu-item{
  border:none;
}
.btn-icon-text {
    cursor: pointer;
    display: flex;
    gap: 8px;
}

.item-icon {
    width: 16px;
    height: 16px;
}

.item-content {
    font-weight: 500px;
}

.other-menu-item .header-menu-item {
  padding: 4px 6px;
}

/******************Responsive****************/
@media screen and (max-width: 1280px) {
  .header-menu > a[href="#/campaign"],
  .header-menu > a[href="#/target"],
  .header-menu > a[href="#/activity"],
  .header-menu > a[href="#/other"],
  .header-menu > a[href="#/report"] {
    display: none;
  }

  .header-menu > .header-menu-item:last-child {
    display: flex;
    margin-left: 7px;
  }

  .header-menu > .header-menu-item:last-child .icon-khac {
    background-color: #333;
  }

  .header-menu > .header-menu-item:last-child .item-content {
    color: #333;
  }

  .header-menu > .header-menu-item.active:last-child .other-menu-item {
    display: flex;
  }
}
</style>

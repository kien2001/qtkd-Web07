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

      <router-link to="/tiemnang">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-tiemnang"></div>
          <span class="item-content">Tiềm năng</span>
        </div>
      </router-link>
      <router-link to="/chaohang">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-chaohang"></div>
          <span class="item-content">Chào hàng</span>
        </div>
      </router-link>
      <router-link to="/lienhe">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-lienhe"></div>
          <span class="item-content">Liên hệ</span>
        </div>
      </router-link>
      <router-link to="/khachhang">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-khachhang"></div>
          <span class="item-content">Khách hàng</span>
        </div>
      </router-link>
      <router-link to="/cohoi">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-cohoi"></div>
          <span class="item-content">Cơ hội</span>
        </div>
      </router-link>
      <router-link to="/baogia">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-baogia"></div>
          <span class="item-content">Báo giá</span>
        </div>
      </router-link>
      <router-link to="/donhang">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-donhang"></div>
          <span class="item-content">Đơn hàng</span>
        </div>
      </router-link>
      <router-link to="/hoadon">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-hoadon"></div>
          <span class="item-content">Hoá đơn</span>
        </div>
      </router-link>
      <router-link to="/aocohoi">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-aocohoi"></div>
          <span class="item-content">Ao cơ hội</span>
        </div>
      </router-link>
      <router-link to="/hoatdong">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-hoatdong"></div>
          <span class="item-content">Hoạt động</span>
        </div>
      </router-link>
      <router-link to="/muctieu">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-muctieu"></div>
          <span class="item-content">Mục tiêu</span>
        </div>
      </router-link>
      <router-link to="/chiendich">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-chiendich"></div>
          <span class="item-content">Chiến dịch</span>
        </div>
      </router-link>
      <router-link to="/baocao">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-baocao"></div>
          <span class="item-content">Báo cáo</span>
        </div>
      </router-link>
      <router-link to="/khac">
        <div class="header-menu-item btn-icon-text">
          <div class="item-icon icon-khac"></div>
          <span class="item-content">Khác</span>
        </div>
      </router-link>
      <div class="header-menu-item btn-icon-text" id="other">
        <div class="item-icon icon-khac"></div>
        <span class="item-content">Khác</span>
        <div class="other-menu-item">
          <router-link to="/hoatdong">
            <div class="header-menu-item btn-icon-text">
              <div class="item-icon icon-hoatdong"></div>
              <span class="item-content">Hoạt động</span>
            </div>
          </router-link>
          <router-link to="/muctieu">
            <div class="header-menu-item btn-icon-text">
              <div class="item-icon icon-muctieu"></div>
              <span class="item-content">Mục tiêu</span>
            </div>
          </router-link>
          <router-link to="/chiendich">
            <div class="header-menu-item btn-icon-text">
              <div class="item-icon icon-chiendich"></div>
              <span class="item-content">Chiến dịch</span>
            </div>
          </router-link>
          <router-link to="/baocao">
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
  --imgUrl: url("../assets/img/icon_toggle.svg");
}

/* =======Header-logo ========== */
.header-logo {
  padding: 5px;
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
  background-image: url("../assets/img/icon_toggle.svg");
  background-repeat: no-repeat;
  cursor: pointer;
}

.header-icon-left .icon-logo {
  align-self: center;
  background-image: url("../assets/img/Logo_Ban_hang.svg");
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
/* =======Header-menu ========== */
.header-menu {
  display: flex;
  align-items: center;
  flex-basis: 31px;
  padding-left: 7px;
  box-shadow: 0px 4px 8px -4px rgba(31, 34, 41, 0.16);
}

.header-menu > a {
  height: 100%;
  margin: 0 7px;
}

.header-menu > a:first-child {
  margin-left: 0;
}

.header-menu-item {
  position: relative;
  height: 100%;
  align-items: center;
}

.header-menu a:not(.router-link-active.router-link-exact-active) .header-menu-item:hover{
  border-radius: 4px;
  background-color: #d3d7de;
}

.router-link-active.router-link-exact-active .header-menu-item{
  border-bottom: 2px solid #5a76f2;
}

.router-link-active.router-link-exact-active .header-menu-item > .item-content{
    color: #4262f0 !important;
}


/* .header-menu-item.active:not(.header-menu > .header-menu-item:last-child) {
  border-bottom: 2px solid #5a76f2;
} */

.header-menu-item.active > .item-content {
  color: #4262f0 !important;
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
  .header-menu > a[href="#/chiendich"],
  .header-menu > a[href="#/muctieu"],
  .header-menu > a[href="#/hoatdong"],
  .header-menu > a[href="#/khac"],
  .header-menu > a[href="#/baocao"] {
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

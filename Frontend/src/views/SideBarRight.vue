<template>
    <div class="sidebar-right">
        <div class="sidebar-right-menu">
            <div class="menu-item btn-icon small btn-hover phone-fill"></div>
            <div class="menu-item btn-icon small btn-hover note-checked-fill"></div>
            <div class="menu-item btn-icon small btn-hover lich-fill"></div>
            <div class="menu-item btn-icon small btn-hover mail-fill"></div>
        </div>
        <div class="sidebar-right-main">
            <div class="main-header">Lịch sử giao dịch</div>
            <div class="main-list">
                <div class="main-item">
                    <div class="main-item-icon icon icon-document-background medium"></div>
                    <div class="main-item-content">
                        <div class="content-header">ghi chú</div>
                        <div class="content-context">
                            <span class="name">Nguyễn Văn Hà</span><span class="id"> (A006) . </span><span
                                class="date">12/08/2021</span>
                        </div>
                    </div>
                </div>
                <div class="main-item">
                    <div class="main-item-icon icon icon-document-background medium "></div>
                    <div class="main-item-content">
                        <div class="content-header">ghi chú</div>
                        <div class="content-context">
                            <span class="name">Nguyễn Văn Hà</span><span class="id"> (A006) . </span><span
                                class="date">12/08/2021</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="btn-icon btn-toggle toggle-right" @click="handleClickToggle"></div>
    </div>
</template>
<script>
import $ from 'jquery'
import { handleToggleSideBar } from '../js/toggleSideBar'
export default {
  name: 'SideBarRight',
  // data() {
  //     return {
  //         isToggle: false
  //     }
  // },
  methods: {
    handleClickToggle (e) {
      handleToggleSideBar('sidebar-left', e, '320px')
      console.log($(e.target).attr('toggle'))
    }
  }
}
</script>
<style scoped>
.sidebar-right {
    position: relative;
    display: flex;
    flex-direction: column;
    border-left: 1px solid rgb(226, 228, 233);
    /* padding: 0 15px; */
}

.sidebar-right-menu {
    display: flex;
    align-items: center;
    border-bottom: 1px solid rgb(226, 228, 233);
    justify-content: space-around;
    flex-basis: 37px;
}

.sidebar-right-main {
    padding: 10px 10px 0 10px;
    display: flex;
    flex-direction: column;
    gap: 15px;
}

.main-header {
    font-size: 13px;
    font-weight: 700;
    line-height: 16px;
}

.main-list {
    display: flex;
    flex-direction: column;
    gap: 10px;
}

.main-item {
    display: flex;
    gap: 10px;
    align-items: center;
}

.main-item-content .content-header {
    margin-bottom: 3px;
}
</style>

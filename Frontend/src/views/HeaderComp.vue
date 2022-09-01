<template>
    <div class="header">
        <div class="header-logo">
            <div class="header-icon-left">
                <div class="icon-toggle"></div>
                <div class="icon-logo">
                </div>
            </div>
            <div class="header-search">
                <input class="input input-icon icon-search" type="text" @keyup.prevent="handleSearch"
                    placeholder="Tìm kiếm tiềm năng, liên hệ, khách hàng" ref="searchInput">
            </div>
            <div class="header-icon-right">
                <div class="btn-icon btn-menu-header btn-hover facebook">
                </div>
                <div class="btn-icon btn-menu-header btn-hover lapphuongtimkiem">
                </div>
                <div class="btn-icon btn-menu-header btn-hover lich"></div>
                <div class="btn-icon btn-menu-header btn-hover chuong"></div>
                <div class="btn-icon btn-menu-header btn-hover caidat"></div>
                <div class="btn-icon btn-menu-header btn-hover tronchanhoi"> </div>
                <div class="btn-icon btn-menu-header btn-hover default-avt"> </div>
            </div>
        </div>
        <div class="header-menu">
            <router-link to="/" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text active" :href="href" @click="navigate">
                    <div class=" item-icon icon-ngoinha-active">
                    </div>
                    <span class="item-content">Bàn làm việc</span>
                </div>
            </router-link>

            <router-link to="/tiemnang" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-tiemnang">
                    </div>
                    <span class=" item-content">Tiềm năng</span>
                </div>
            </router-link>
            <router-link to="/chaohang" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-chaohang">
                    </div>
                    <span class="item-content">Chào hàng</span>
                </div>
            </router-link>
            <router-link to="/lienhe" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-lienhe ">
                    </div>
                    <span class="item-content">Liên hệ</span>
                </div>
            </router-link>
            <router-link to="/khachhang" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-khachhang">
                    </div>
                    <span class="item-content">Khách hàng</span>
                </div>
            </router-link>
            <router-link to="/cohoi" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-cohoi">
                    </div>
                    <span class="item-content">Cơ hội</span>
                </div>
            </router-link>
            <router-link to="/baogia" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-baogia">
                    </div>
                    <span class="item-content">Báo giá</span>
                </div>
            </router-link>
            <router-link to="/donhang" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-donhang">
                    </div>
                    <span class="item-content">Đơn hàng</span>
                </div>
            </router-link>
            <router-link to="/hoadon" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-hoadon">
                    </div>
                    <span class="item-content">Hoá đơn</span>
                </div>
            </router-link>
            <router-link to="/aocohoi" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-aocohoi">
                    </div>
                    <span class="item-content">Ao cơ hội</span>
                </div>
            </router-link>
            <router-link to="/hoatdong" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-hoatdong">
                    </div>
                    <span class="item-content">Hoạt động</span>
                </div>
            </router-link>
            <router-link to="/muctieu" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-muctieu">
                    </div>
                    <span class="item-content">Mục tiêu</span>
                </div>
            </router-link>
            <router-link to="/chiendich" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-chiendich">
                    </div>
                    <span class="item-content">Chiến dịch</span>
                </div>
            </router-link>
            <router-link to="/baocao" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-baocao">
                    </div>
                    <span class="item-content">Báo cáo</span>
                </div>
            </router-link>
            <router-link to="/khac" custom v-slot="{ href, navigate }">
                <div class="header-menu-item btn-icon-text" :href="href" @click="navigate">
                    <div class="item-icon icon-khac">
                    </div>
                    <span class="item-content">Khác</span>
                </div>
            </router-link>
        </div>
    </div>
</template>
<script>
import $ from 'jquery'
export default {
    name: 'HeaderComp',
    data() {
        return {
            oldClassImage: '',
            newClassImage: '',
            timeout: null,
            timer: 1000
        }
    },
    mounted() {
        const headerMenuItems = $('.header-menu-item').toArray()
        headerMenuItems.forEach((item) => {
            $(item).click(this.menuItemOnClick)
        })
    },
    methods: {
        handleSearch() {
            let me = this;
            clearTimeout(this.timeOut);
            this.timeOut = setTimeout(() => {
                me.$store.commit("setConditionSearch", $(this.$refs.searchInput).val())
            }, this.timer);
        },
        menuItemOnClick(e) {
            this.oldClassImage = $('.active .item-icon').attr('class').split(' ')[1]
            $('.active .item-icon').removeClass(this.oldClassImage)
            $('.active .item-icon').addClass(this.oldClassImage.replace(this.oldClassImage.slice(-7), ''))
            $('.header-menu-item').removeClass('active')
            const activeItem = $(e.target).parent('.header-menu-item')
            if ($(activeItem).length > 0) {
                this.handleActive(activeItem)
            } else {
                this.handleActive(e.target)
            }
        },
        handleActive(element) {
            this.newClassImage = $(element).children('.item-icon').attr('class').split(' ')[1]
            $(element).addClass('active')
            $(element).children('.item-icon').removeClass(this.newClassImage)
            $(element).children('.item-icon').addClass(this.newClassImage + '-active')
        }
    }
}
</script>

<style scoped>

.header {
    display: flex;
    flex-direction: column;
    /* background-image: url("../assets/img/blue-hover"); */
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
}

/* =======Header-menu ========== */
.header-menu {
    display: flex;
    align-items: center;
    flex-basis: 31px;
    padding-left: 7px;
    box-shadow: 0px 4px 8px -4px rgba(31, 34, 41, 0.16);
}

.header-menu-item {
    position: relative;
    height: 100%;
    align-items: center;
    margin: 0 7px;
}
.header-menu-item:first-child{
    margin-left:0;
}

.header-menu-item:not(.active):hover {
    border-radius: 4px;
    background-color: #D3D7DE;
}

.header-menu-item.active {
    border-bottom: 2px solid #5A76F2;
}

.header-menu-item.active>.item-content {
    color: #4262F0;
}
</style>

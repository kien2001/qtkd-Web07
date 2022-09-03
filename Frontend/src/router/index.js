import { createRouter, createWebHashHistory } from 'vue-router'
import AoCoHoi from '../views/pages/Aocohoi'
import BanLamViec from '../views/pages/Banlamviec'
import BaoGia from '../views/pages/Baogia'
import BaoCao from '../views/pages/Baocao'
import ChaoHang from '../views/pages/Chaohang'
import ChienDich from '../views/pages/Chiendich'
import DonHang from '../views/pages/Donhang'
import CoHoi from '../views/pages/Cohoi'
import HoaDon from '../views/pages/Hoadon'
import HoatDong from '../views/pages/Hoatdong'
import Khac from '../views/pages/Khac'
import KhachHang from '../views/pages/Khachhang'
import LienHe from '../views/pages/Lienhe'
import MucTieu from '../views/pages/Muctieu'
import TiemNang from '../views/pages/Tiemnang'
const routes = [
  {
    path: '/',
    name: 'home',
    component: BanLamViec,
    meta:{title:"Bàn làm việc"}
  },
  {
    path: '/tiemnang',
    name: 'TiemNang',
    component: TiemNang,
    meta:{title: "Tiềm năng"}
  },
  {
    path: '/chaohang',
    name: 'ChaoHang',
    component: ChaoHang,
    meta:{title: "Chào hàng"}
  },
  {
    path: '/lienhe',
    name: 'LienHe',
    component: LienHe,
    meta:{title: "Liên hệ"}
  },
  {
    path: '/khachhang',
    name: 'KhachHang',
    component: KhachHang,
    meta:{title: "Khách hàng"}
  },
  {
    path: '/cohoi',
    name: 'CoHoi',
    component: CoHoi,
    meta:{title: "Cơ hội"}
  },
  {
    path: '/baogia',
    name: 'BaoGia',
    component: BaoGia,
    meta:{title: "Báo giá"}
  },
  {
    path: '/donhang',
    name: 'DonHang',
    component: DonHang,
    meta:{title: "Đơn hàng"}
  },
  {
    path: '/hoadon',
    name: 'HoaDon',
    component: HoaDon,
    meta:{title: "Hoá đơn"}
  },
  {
    path: '/aocohoi',
    name: 'AoCoHoi',
    component: AoCoHoi,
    meta:{title: "Ao cơ hội"}
  },
  {
    path: '/hoatdong',
    name: 'HoatDong',
    component: HoatDong,
    meta:{title: "Hoạt động"}
  },
  {
    path: '/muctieu',
    name: 'MucTieu',
    component: MucTieu,
    meta:{title: "Mục tiêu"}
  },
  {
    path: '/chiendich',
    name: 'ChienDich',
    component: ChienDich,
    meta:{title: "Chiến dịch"}
  },
  {
    path: '/baocao',
    name: 'BaoCao',
    component: BaoCao,
    meta:{title: "Báo cáo"}
  },
  {
    path: '/khac',
    name: 'Khac',
    component: Khac,
    meta:{title: "Khác"}
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router

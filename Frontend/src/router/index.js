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
    component: BanLamViec
  },
  {
    path: '/tiemnang',
    name: 'TiemNang',
    component: TiemNang
  },
  {
    path: '/chaohang',
    name: 'ChaoHang',
    component: ChaoHang
  },
  {
    path: '/lienhe',
    name: 'LienHe',
    component: LienHe
  },
  {
    path: '/khachhang',
    name: 'KhachHang',
    component: KhachHang
  },
  {
    path: '/cohoi',
    name: 'CoHoi',
    component: CoHoi
  },
  {
    path: '/baogia',
    name: 'BaoGia',
    component: BaoGia
  },
  {
    path: '/donhang',
    name: 'DonHang',
    component: DonHang
  },
  {
    path: '/hoadon',
    name: 'HoaDon',
    component: HoaDon
  },
  {
    path: '/aocohoi',
    name: 'AoCoHoi',
    component: AoCoHoi
  },
  {
    path: '/hoatdong',
    name: 'HoatDong',
    component: HoatDong
  },
  {
    path: '/muctieu',
    name: 'MucTieu',
    component: MucTieu
  },
  {
    path: '/chiendich',
    name: 'ChienDich',
    component: ChienDich
  },
  {
    path: '/baocao',
    name: 'BaoCao',
    component: BaoCao
  },
  {
    path: '/khac',
    name: 'Khac',
    component: Khac
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router

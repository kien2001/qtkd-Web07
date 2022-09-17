import { createRouter, createWebHashHistory } from 'vue-router'
import PondOpportunity from '../views/pages/ThePondOpportunity'
import Home from '../views/pages/TheHome'
import Quote from '../views/pages/TheQuote'
import Report from '../views/pages/TheReport'
import Offer from '../views/pages/TheOffer'
import Campaign from '../views/pages/TheCampaign'
import Order from '../views/pages/TheOrder'
import Opportunity from '../views/pages/TheOpportunity'
import Invoice from '../views/pages/TheInvoice'
import Activity from '../views/pages/TheActivity'
import Other from '../views/pages/TheOther'
import Customer from '../views/pages/TheCustomer'
import Contact from '../views/pages/TheContact'
import Target from '../views/pages/TheTarget'
import Potential from '../views/pages/ThePotential'
const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
    meta:{title:"Bàn làm việc"}
  },
  {
    path: '/potential',
    name: 'Potential',
    component: Potential,
    meta:{title: "Tiềm năng"}
  },
  {
    path: '/offer',
    name: 'Offer',
    component: Offer,
    meta:{title: "Chào hàng"}
  },
  {
    path: '/contact',
    name: 'Contact',
    component: Contact,
    meta:{title: "Liên hệ"}
  },
  {
    path: '/customer',
    name: 'Customer',
    component: Customer,
    meta:{title: "Khách hàng"}
  },
  {
    path: '/opportunity',
    name: 'Opportunity',
    component: Opportunity,
    meta:{title: "Cơ hội"}
  },
  {
    path: '/quote',
    name: 'Quote',
    component: Quote,
    meta:{title: "Báo giá"}
  },
  {
    path: '/order',
    name: 'Order',
    component: Order,
    meta:{title: "Đơn hàng"}
  },
  {
    path: '/invoice',
    name: 'Invoice',
    component: Invoice,
    meta:{title: "Hoá đơn"}
  },
  {
    path: '/pondOpportunity',
    name: 'PondOpportunity',
    component: PondOpportunity,
    meta:{title: "Ao cơ hội"}
  },
  {
    path: '/activity',
    name: 'Activity',
    component: Activity,
    meta:{title: "Hoạt động"}
  },
  {
    path: '/target',
    name: 'Target',
    component: Target,
    meta:{title: "Mục tiêu"}
  },
  {
    path: '/campaign',
    name: 'Campaign',
    component: Campaign,
    meta:{title: "Chiến dịch"}
  },
  {
    path: '/report',
    name: 'Report',
    component: Report,
    meta:{title: "Báo cáo"}
  },
  {
    path: '/other',
    name: 'Other',
    component: Other,
    meta:{title: "Khác"}
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router

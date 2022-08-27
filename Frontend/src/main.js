import { createApp } from 'vue'
import FormSubmit from './components/FormSubmit'
import UpdateMultipleForm from './components/UpdateMultipleForm'
import DropDown from './components/DropDown'
import ComboBox from './components/ComboBox'
import KienButton from './components/KienButton'
import Tooltip from './components/Tooltip'
import Loading from './components/Loading'
import App from './App.vue'
import store from './store'
import router from './router/index'
import Datepicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'
const app = createApp(App)
app.component('FormSubmit', FormSubmit)
app.component('UpdateMultipleForm', UpdateMultipleForm)
app.component('DropDown', DropDown)
app.component('KienButton', KienButton)
app.component('Tooltip', Tooltip)
app.component('Datepicker', Datepicker)
app.component('ComboBox', ComboBox)
app.component('Loading', Loading)
app
  .use(router)
  .use(store)
  .mount('#app')

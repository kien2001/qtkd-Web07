import { createApp } from 'vue'
import TheFormSubmit from './components/TheFormSubmit'
import TheInputForm from "./components/TheInputForm.vue";
import TheUpdateMultipleForm from './components/TheUpdateMultipleForm'
import TheDropDown from './components/TheDropDown'
import TheComboBox from './components/TheComboBox'
import TheButton from './components/TheButton'
import TheTooltip from './components/TheTooltip'
import TheLoading from './components/TheLoading'
import ThePopUp from './components/ThePopUp'
import TheToastMessage from './components/TheToastMessage'
import TheDatepicker from "./components/TheDatepicker.vue";
import clickOutside from './directives/clickOutside';
import App from './App.vue'
import store from './store'
import router from './router/index'
const app = createApp(App)
app.component('TheFormSubmit', TheFormSubmit)
app.component('TheUpdateMultipleForm', TheUpdateMultipleForm)
app.component('TheDropDown', TheDropDown)
app.component('TheButton', TheButton)
app.component('TheTooltip', TheTooltip)
app.component('TheDatepicker', TheDatepicker)
app.component('TheComboBox', TheComboBox)
app.component('TheLoading', TheLoading)
app.component('ThePopUp', ThePopUp)
app.component('TheToastMessage', TheToastMessage)
app.component('TheInputForm', TheInputForm)
app.directive("click-outside", clickOutside)
app
  .use(router)
  .use(store)
  .mount('#app')

import { createApp } from 'vue'
import BaseFormSubmit from './base/BaseFormSubmit'
import BaseInputForm from "./base/BaseInputForm.vue";
import BaseUpdateMultipleForm from './base/BaseUpdateMultipleForm'
import BaseDropDown from './base/BaseDropDown'
import BaseComboBox from './base/BaseComboBox'
import BaseButton from './base/BaseButton'
import BaseTooltip from './base/BaseTooltip'
import BaseLoading from './base/BaseLoading'
import BasePopUp from './base/BasePopUp'
import BaseToastMessage from './base/BaseToastMessage'
import BaseDatepicker from "./base/BaseDatepicker.vue";
import clickOutside from './directives/clickOutside';
import App from './App.vue'
import store from './store'
import router from './router/index'
const app = createApp(App)
app.component('BaseFormSubmit', BaseFormSubmit)
app.component('BaseUpdateMultipleForm', BaseUpdateMultipleForm)
app.component('BaseDropDown', BaseDropDown)
app.component('BaseButton', BaseButton)
app.component('BaseTooltip', BaseTooltip)
app.component('BaseDatepicker', BaseDatepicker)
app.component('BaseComboBox', BaseComboBox)
app.component('BaseLoading', BaseLoading)
app.component('BasePopUp', BasePopUp)
app.component('BaseToastMessage', BaseToastMessage)
app.component('BaseInputForm', BaseInputForm)
app.directive("click-outside", clickOutside)
app
  .use(router)
  .use(store)
  .mount('#app')

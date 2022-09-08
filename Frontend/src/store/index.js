import { createStore } from 'vuex'

export default createStore({
  state: {
    isShowForm: false,
    listIdChecked: [],
    isDeleted: false, // check xem đã delete thành công hay chưa
    isInserted: false, // check xem đã insert thành công hay chưa
    isUpdated: false, // check xem đã update thành công hay chưa
    editForm: true,
    editMultipleRow: false,
    listCheckedCustomer: [],
    conditionSearch: "",
    customerUpdated: {},
    state: "",
    message: "",
    isShow: false,
    listFilter: []
  },
  getters: {
  },
  mutations: {
    setListFilter(state, payload) {
      this.state.listFilter = payload
    },

    setState(state, payload) {
      this.state.state = payload
    },
    setMessage(state, payload) {
      this.state.message = payload
    },
    setIsShow(state, payload) {
      let me = this.state
      this.state.isShow = payload
      if (me.isShow) {
        setTimeout(() => {
          me.isShow = false
        }, 3000)
      }
    },
    setCustomerUpdated(state, payload) {
      this.state.customerUpdated = payload
    },
    // set giá trị cho điều kiện tìm kiếm
    setConditionSearch(state, payload) {
      // if (payload.trim() === '') {
      //   this.state.conditionSearch = null
      // } else {
        this.state.conditionSearch = payload
      // }
    },
    // set giá trị cho mảng listCheckedCustomer'
    setListCheckedCustomer(state, payload) {
      this.state.listCheckedCustomer = payload
    },
    //set giá trị biến editMultipleRow
    setEditMultipleRow(state, payload) {
      this.state.editMultipleRow = payload
    },
    // set giá trị biến editForm
    setEditForm(state, payload) {
      this.state.editForm = payload
    },
    // set value isDeleted
    setIsDeleted(state, payload) {
      this.state.isDeleted = payload
    },
    // set value isUpdated
    setIsUpdated(state, payload) {
      this.state.isUpdated = payload
    },
    // set value isInserted
    setIsInserted(state, payload) {
      this.state.isInserted = payload
    },
    resetListIdChecked() {
      this.state.listIdChecked = []
    },
    setListIdChecked(state, payload) {
      this.state.listIdChecked = [...payload]
    },
    setFormState(state, payload) {
      this.state.isShowForm = payload
    }
  },
  actions: {
  },
  modules: {
  }
})

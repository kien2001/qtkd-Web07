import { createStore } from 'vuex'

export default createStore({
  state: {
    isShowForm: false,
    dropDownSelected: {
      vocative: {},
      departmentName: {},
      positionName: {},
      sourceName: {},
      typeName: {},
      revenueName: {},
      country: {},
      city: {},
      district: {},
      ward: {}
    },
    listIdChecked: [],
    isDeleted: false, // check xem đã delete thành công hay chưa
    editForm:true,
    editMultipleRow: false,
    listCheckedCustomer:[],
    conditionSearch:null
  },
  getters: {
  },
  mutations: {
    // set giá trị cho điều kiện tìm kiếm
    setConditionSearch(state, payload){
      this.state.conditionSearch = payload
    },
    // set giá trị cho mảng listCheckedCustomer'
    setListCheckedCustomer(state, payload){
      this.state.listCheckedCustomer = payload
    },
    //set giá trị biến editMultipleRow
    setEditMultipleRow(state, payload){
      this.state.editMultipleRow = payload
    },
    // set giá trị biến editForm
    setEditForm(state, payload){
      this.state.editForm = payload
    },
    // set value isDeleted
    setIsDeleted (state, payload) {
      this.state.isDeleted = payload
    },
    resetListIdChecked () {
      this.state.listIdChecked = []
    },
    setListIdChecked (state, payload) {
      this.state.listIdChecked = [...payload]
    },
    setFormState (state, payload) {
      this.state.isShowForm = payload
    },
    setDropDownSelect (state, payload) {
      const key = Object.keys(payload)[0]
      this.state.dropDownSelected[key] = { ...payload[key] }
    },
    resetDropdown () {
      const arrKey = Object.keys(this.state.dropDownSelected)
      arrKey.forEach((item) => {
        this.state.dropDownSelected[item] = {}
      })
    }
  },
  actions: {
  },
  modules: {
  }
})

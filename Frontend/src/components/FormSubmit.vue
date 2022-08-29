<template>
  <div class="form-submit-container" ref="container">
    <ToastMessage :message="message" :state="state" ref="toast" />
    <PopUp
      :text="!editForm ? 'Bạn có chắc chắn muốn thêm tiềm năng này không?' : 'Bạn có chắc chắn muốn sửa các tiềm năng này không?' "
      :colorBtn="!editForm ? '#4262F0' : '#31B491'" :colorHoverBtn="!editForm ? '#2B4EEE' : '#2EA888'" ref="showConfirm"
      @handlePopUp="sendRequest" />
    <div class="form-submit">
      <div class="form-header">
        <div class="header-title">
          <div class="header-title-context">
            {{ editForm ? "Sửa tiềm năng" : "Thêm Tiềm năng" }}<span>- Mẫu tiêu chuẩn</span>
          </div>
          <div class="edit-btn">Sửa bố cục</div>
        </div>
        <div class="header-btn">
          <Button name="Huỷ bỏ" color="#fff" @clickBtn="closeForm" colorHover="#D0D8FB" />
          <Button name="Lưu và thêm" color="#fff" colorHover="#D0D8FB" @clickBtn="saveAndAddForm" />
          <Button name="Lưu" color="#4262F0" colorHover="#2B4EEE" @clickBtn="saveForm" colorText="#FFFFFF" />
        </div>
      </div>
      <div class="form-main">
        <div class="form-main-item" id="avatar-partition">
          <div class="form-main-header">Ảnh tiềm năng</div>
          <div class="avatar default-avt"></div>
        </div>
        <div class="form-main-item" id="customer-partition">
          <div class="form-main-header">Thông tin chung</div>
          <div class="form-main-context">
            <div class="context-item">
              <div class="context-label">Xưng hô</div>
              <div class="context-input">
                <DropDown id="vocative" :showInput="true" placeholder="-Không chọn-" :fetchDataWhenClick="false"
                  name="Vocatives" ref="vocative" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Họ và đệm</div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="lastMiddleName" @changeName="setLastMiddleName"
                  ref="lastMiddleName" />
              </div>
            </div>
            <div class="context-item" required>
              <div class="context-label">Tên</div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="firstName" @changeName="setFirstName" ref="firstName" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Họ và tên</div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="fullName" :isDisabled="true" ref="fullName" />
                <!-- :handleFullName="handleFullName" -->
              </div>
            </div>
            <div class=" context-item">
              <div class="context-label">Phòng ban</div>
              <div class="context-input">
                <DropDown :fetchDataWhenClick="false" id="departmentName" :showInput="true" placeholder="-Không chọn-"
                  name="Departments" ref="departmentId" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Chức danh</div>
              <div class="context-input">
                <DropDown :fetchDataWhenClick="false" id="positionName" :showInput="true" placeholder="-Không chọn-"
                  name="Positions" ref="positionId" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">
                ĐT di động
                <Tooltip tooltipText="Điện thoại di động" />
              </div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="customerPhoneNum" ref="customerPhoneNum" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">
                ĐT cơ quan
                <Tooltip tooltipText="Điện thoại cơ quan" />
              </div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="companyPhoneNum" ref="companyPhoneNum" />
              </div>
            </div>
            <div class=" context-item" v-if="editForm">
              <div class="context-label">
                ĐT khác
                <Tooltip tooltipText="Điện thoại khác" />
              </div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="otherPhoneNum" ref="otherPhoneNum" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Nguồn gốc</div>
              <div class="context-input">
                <DropDown id="sourceName" :showInput="true" placeholder="-Không chọn-" :fetchDataWhenClick="false"
                  name="Sources" ref="sourceId" />

              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Loại tiềm năng</div>
              <div class="context-input">
                <ComboBox :options="async () => {
                  return await this.getDataComboBox(this.prefixPotentialName)
                }" ref="potentialNames" />
              </div>
            </div>
            <div class="context-item" v-if="editForm">
              <div class="context-label">Không gọi điện</div>
              <div class="context-input">
                <div class="icon icon-checkbox" id="disableCall"></div>
              </div>
            </div>
            <div class="context-item" v-if="editForm">
              <div class="context-label">Không gửi email</div>
              <div class="context-input">
                <div class="icon icon-checkbox" id="disableMail"></div>
              </div>
            </div>

            <div class="context-item">
              <div class="context-label">Zalo</div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="zalo" ref="zalo" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Tổ chức</div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="organizationName" ref="organizationName" />
              </div>
            </div>
            <div class=" context-item">
              <div class="context-label">Email cá nhân</div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="customerEmail" ref="customerEmail" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Email cơ quan</div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="companyEmail" ref="companyEmail" />
              </div>
            </div>
            <div class="context-item" v-if="editForm"></div>
            <div class="context-item">
              <div class="context-label">Mã số thuế</div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="taxCode" ref="taxCode" />
              </div>
            </div>
          </div>
        </div>
        <div class="form-main-item" id="person-partition" v-if="editForm">
          <div class="form-main-header">Thông tin cá nhân</div>
          <div class="form-main-context">
            <div class="context-item">
              <div class="context-label">Giới tính</div>
              <div class="context-input">
                <DropDown id="genderName" :showInput="true" placeholder="-Không chọn-" :fetchDataWhenClick="false"
                  name="Genders" ref="gender" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Ngày sinh</div>
              <div class="context-input">
                <DatePicker inputClassName="dateOfBirth" @getDate="handleGetBirthDate" ref="dateOfBirth" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Facebook</div>
              <div class="context-input">
                <InputFormVue class="inputForm" id="facebook" ref="facebook" />
              </div>
            </div>
          </div>
        </div>
        <template v-else>
          <div class="form-main-item" id="organization-partition">
            <div class="form-main-header">Thông tin tổ chức</div>
            <div class="form-main-context">
              <div class="context-item">
                <div class="context-label">Tài khoản ngân hàng</div>
                <div class="context-input">
                  <InputFormVue class="inputForm" id="bankAccount" ref="bankAccount" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Mở tại ngân hàng</div>
                <div class="context-input">
                  <InputFormVue class="inputForm" id="bankName" ref="bankName" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Ngày thành lập</div>
                <div class="context-input">
                  <DatePicker inputClassName="createdAccountAt" @getDate="handleGetCreatedAccountDate" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Loại hình</div>
                <div class="context-input">
                  <DropDown id="typeName" :showInput="true" placeholder="-Không chọn-" :fetchDataWhenClick="false"
                    name="Types" ref="typeId" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Lĩnh vực</div>
                <div class="context-input">
                  <ComboBox :options="async () => {
                    return await this.getDataComboBox(this.prefixDomainName)
                  }" ref="domainNames" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Ngành nghề</div>
                <div class="context-input">
                  <ComboBox :options="async () => {
                    return await this.getDataComboBox(this.prefixCareerName)
                  }" ref="careerNames" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Doanh thu</div>
                <div class="context-input">
                  <DropDown id="revenueName" :showInput="true" placeholder="-Không chọn-" :fetchDataWhenClick="false"
                    name="Revenues" ref="revenueId" />
                </div>
              </div>
            </div>
          </div>
          <div class="form-main-item" id="address-partition">
            <div class="form-main-header">Thông tin địa chỉ</div>
            <div class="form-main-context">
              <div class="context-item">
                <div class="context-label">Quốc gia</div>
                <div class="context-input">
                  <DropDown id="country" @selected="getSelectedCountry" :showInput="true" placeholder="-Không chọn-"
                    :fetchDataWhenClick="false" name="Countries" ref="countryId" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Tỉnh/Thành phố</div>
                <div class="context-input">
                  <DropDown id="city" @selected="getSelectedCity" @click="handleShowCities" :showInput="true"
                    :fetchDataWhenClick="false" placeholder="-Không chọn-" name="Cities" ref="cityId" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Quận/Huyện</div>
                <div class="context-input">
                  <DropDown id="district" @selected="getSelectedDistrict" @click="handleShowDistricts" :showInput="true"
                    :fetchDataWhenClick="false" placeholder="-Không chọn-" name="Districts" ref="districtId" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Phường/Xã</div>
                <div class="context-input">
                  <DropDown id="ward" @selected="getSelectedWard" @click="handleShowWards" :showInput="true"
                    :fetchDataWhenClick="false" placeholder="-Không chọn-" name="Wards" ref="wardId" />

                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Số nhà, Đường phố</div>
                <div class="context-input">
                  <InputFormVue class="inputForm" id="homeNumber" ref="homeNumber" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Mã vùng</div>
                <div class="context-input">
                  <InputFormVue class="inputForm" id="postalCode" ref="postalCode" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Địa chỉ</div>
                <div class="context-input">
                  <textarea name="" id="addressName" class="text-area" cols="30" rows="10"
                    v-model="addressName"></textarea>
                </div>
              </div>
            </div>
          </div>

          <div class="form-main-item" id="description-partition">
            <div class="form-main-header">Thông tin mô tả</div>
            <div class="form-main-context">
              <div class="context-item">
                <div class="context-label">Mô tả</div>
                <div class="context-input">
                  <textarea name="" id="description" class="text-area" cols="30" rows="10"></textarea>
                </div>
              </div>
            </div>
          </div>
          <div class="form-main-item" id="system-partition">
            <div class="form-main-header">Thông tin hệ thống</div>
            <div class="form-main-context">
              <div class="context-item">
                <div class="context-label">Dùng chung</div>
                <div class="context-input">
                  <div class="icon icon-checkbox" id="sharingUse"></div>
                </div>
              </div>
              <div class="context-item"></div>
              <div class="context-item">
                <div class="context-label">Mã tiềm năng</div>
                <div class="context-input">
                  <InputFormVue class="inputForm" id="potentialCode" ref="potentialCode" />
                </div>
              </div>
            </div>
          </div>
        </template>

      </div>
    </div>

  </div>
  <Loading v-if="isLoading" />

</template>

<style>
.form-submit-container {
  z-index: 1001;
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(51, 51, 51, 0.497);
  display: flex;
  justify-content: center;
  align-items: center;
}

.form-submit {
  width: 1200px;
  background-color: #fff;
  max-height: 700px;
  border-radius: 4px;
}

.form-header {
  background-color: #e2e4e9;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  border-radius: 4px 4px 0 0;
}

.header-title {
  display: flex;
  gap: 10px;
}

.header-title-context {
  font-size: 20px;
  line-height: 28px;
  font-weight: 500;
}

.header-title-context>span {
  font-size: 16px;
  line-height: 20px;
  font-weight: 500;
}

.edit-btn {
  align-self: end;
  color: #4262f0;
  cursor: pointer;
  font-weight: 500;
  padding: 5px 0;
}

.header-btn {
  display: flex;
  gap: 8px;
}

.form-main {
  overflow-y: auto;
  height: 648px;
  display: flex;
  flex-direction: column;
  gap: 30px;
  padding: 20px 50px;
  border-radius: 0 0 4px 4px;
}

.form-main-header {
  font-size: 16px;
  line-height: 20px;
  font-weight: 700;
  margin-bottom: 15px;
}

.avatar {
  width: 50px;
  height: 50px;
  background-size: cover !important;
  border-radius: 50%;
}

.form-main-context {
  margin-top: 10px;
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  grid-template-rows: auto;
  column-gap: 50px;
  row-gap: 15px;
  grid-auto-rows: minmax(1px, auto);
}

#description-partition>.form-main-context {
  grid-template-columns: 1fr;
}

#description-partition .context-input {
  flex-basis: 86%;
}

#address-partition>.form-main-context>.context-item:last-child,
#description-partition>.form-main-context>.context-item:last-child {
  height: 100px;
}

.text-area {
  min-height: 100px;
  max-height: 100px;
  border-radius: 4px;
  border-width: 1px;
  border-style: solid;
  border-color: #D3D7DE;
  outline: none;
  padding: 5px 10px;
  background-color: #fff;
  font-size: 13px;
  line-height: 16px;
  font-weight: 400;
  color: #1F2229;
  resize: none;
}

.text-area:not([disabled]):focus,
.text-area:not([disabled]):active {
  border-color: #4262F0;
}

.context-item {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.context-item[required]>.context-label:after {
  content: "*";
  color: red;
  margin-left: 4px;
}

.context-input {
  position: relative;
  flex-basis: 70%;
  display: flex;
  flex-direction: column;
  gap: 10px;
  height: fit-content;
}

.context-input:where(.context-input>#sharingUse) {
  display: block;
}

.context-input>* {
  width: 100%;
  height: fit-content;
}

.context-label {
  display: flex;
  align-items: center;
}
</style>
<script>
import $ from 'jquery'
import axios from 'axios'
import { rootApi, dropdownField } from '@/js/config'
import StatusCode from '../entities/StatusCode'
import Customer from '../entities/Customer'
import CustomerUpdate from '../entities/CustomerUpdate'
import Organization from '../entities/Organization'
import Address from '../entities/Address'
import Potential from '../entities/Potential'
import handleClickFilterItem from '../js/checkbox'
import { handleTransferObject } from '../js/common'
import InputFormVue from './InputForm.vue'
import DatePicker from './DatePicker.vue'
import PopUp from './PopUp.vue'
export default {
  name: 'FormSubmit',
  data() {
    return {
      message: '',
      state: '',
      lastMiddleName: '',
      firstName: '',
      addressName: '',
      createdAccountAt: '',
      dateOfBirth: '',
      countryName: '',
      cityName: '',
      districtName: '',
      wardName: '',
      errorField: [],
      prefixPotentialName: "Potentials/PotentialNames",
      prefixCareerName: "Organizations/Careers",
      prefixDomainName: "Organizations/Domains",
      editForm: this.$store.state.editForm,
      customerEdit: this.$store.state.customerUpdated,
      isLoading:false,
      isSaveAndAdd:false
    }
  },
  async mounted() {
    const checkboxItems = $('.context-input>.icon.icon-checkbox').toArray()
    console.log(checkboxItems)
    checkboxItems.forEach((item) => {
      $(item).click(this.checkInput)
      $(item).css("background-color", "#fff!important")
    })
    console.log(this.customerEdit);
    if(this.customerEdit.firstName){
      this.mountDataEditForm()
    }

    const requiredFields = $('.context-item[required] .input-text').toArray()
    requiredFields.forEach(field => {
      $(field).blur(this.checkRequiredField)
      $(field).focus(function (e) {
        $(e.target).css('border-color', '#D3D7DE')
        $(e.target).parent().next('span').remove()
      })
    })
    // xử lý hiển thị cho checkbox(liên quan đến mặt hình ảnh)
    $('#sharingUse').parent('.context-input').css('display', 'block')
    $('#disableMail').parent('.context-input').css('display', 'block')
    $('#disableCall').parent('.context-input').css('display', 'block')
    if (!this.editForm) {
      const data = await axios.get(`${rootApi}Potentials/MaxCode`)
        .then(res => res.data)
        .catch(error => console.log(error))
      if (!data.flag) {
        console.log(data.userMgs)
      } else {

        this.$refs['potentialCode'].name = data.data
      }
    }
  },
  computed: {
    handleFullName() {
      return `${this.lastMiddleName} ${this.firstName}`
    },
  },
  watch: {
    customerEdit(newValue){
      console.log(1);
      if(newValue.firstName){
        // this.customerEdit = newValue
        this.mountDataEditForm()
        console.log(1);
      }
    },
    countryName() {
      // this.countryName = ""
      this.cityName = ""
      this.districtName = ""
      this.wardName = ""
      let addressArr = []
      this.$refs.cityId.selected = {}
      this.$refs.cityId.setOptions([])

      this.$refs.districtId.selected = {}
      this.$refs.districtId.setOptions([])

      this.$refs.wardId.selected = {}
      this.$refs.wardId.setOptions([])

      addressArr = [this.wardName.name, this.districtName.name, this.cityName.name, this.countryName.name]
      console.log(addressArr);
      this.addressName = addressArr.join(' ').trim()

    },
    cityName() {
      this.districtName = ""
      this.wardName = ""
      let addressArr = []

      this.$refs.districtId.selected = {}
      this.$refs.districtId.setOptions([])

      this.$refs.wardId.selected = {}
      this.$refs.wardId.setOptions([])

      addressArr = [this.wardName.name, this.districtName.name, this.cityName.name, this.countryName.name]
      console.log(addressArr);
      this.addressName = addressArr.join(' ').trim()

    },
    districtName() {
      this.wardName = ""
      let addressArr = []

      this.$refs.wardId.selected = {}
      this.$refs.wardId.setOptions([])

      addressArr = [this.wardName.name, this.districtName.name, this.cityName.name, this.countryName.name]
      console.log(addressArr);
      this.addressName = addressArr.join(' ').trim()
    },
    wardName() {
      let addressArr = []

      addressArr = [this.wardName.name, this.districtName.name, this.cityName.name, this.countryName.name]
      console.log(addressArr);
      this.addressName = addressArr.join(' ').trim()
    },
    firstName() {
      $("#fullName").val(`${this.lastMiddleName} ${this.firstName}`);
    },
    lastMiddleName() {
      $("#fullName").val(`${this.lastMiddleName} ${this.firstName}`);
    },
    // xử lý hiển thị lỗi
    errorField(newValue) {
      newValue.forEach(item => {
        const keyErr = Object.keys(item)
        const fieldError = $(this.$refs.container).find(`#${keyErr}`)
        if (item[keyErr] !== '') {
          $(fieldError).parent().next('span').remove()
          $(fieldError).css('border-color', 'red')

          $(fieldError).parent().after(`<span style="color:red"}}>${item[keyErr]}</span>`)
        } else {
          $(fieldError).parent().next('span').remove()
          $(fieldError).css('border-color', '#D3D7DE')
        }
      })
    }
    
  },
  components: { InputFormVue, DatePicker, PopUp },
  template: 'FormSubmit',
  methods: {
    mountDataEditForm(){
      if (this.editForm) {
        this.$refs.vocative.oldSearchFilter = this.customerEdit.vocative ? this.customerEdit.vocativeName : "Không chọn"
        this.$refs.vocative.currentValue = { id: this.customerEdit.vocative || 0, name: this.customerEdit.vocative ? this.customerEdit.vocativeName : "Không chọn" }

        this.$refs.departmentId.oldSearchFilter = this.customerEdit.departmentId ? this.customerEdit.departmentName : "Không chọn"
        this.$refs.departmentId.currentValue = { id: this.customerEdit.departmentId || 0, name: this.customerEdit.departmentId ? this.customerEdit.departmentName : "Không chọn" }

        this.$refs.positionId.oldSearchFilter = this.customerEdit.positionId ? this.customerEdit.positionName : "Không chọn"
        this.$refs.positionId.currentValue = { id: this.customerEdit.positionId || 0, name: this.customerEdit.positionId ? this.customerEdit.positionName : "Không chọn" }

        this.$refs.sourceId.oldSearchFilter = this.customerEdit.sourceId ? this.customerEdit.sourceName : "Không chọn"
        this.$refs.sourceId.currentValue = { id: this.customerEdit.sourceId || 0, name: this.customerEdit.sourceId ? this.customerEdit.sourceName : "Không chọn" }

        this.$refs.gender.oldSearchFilter = this.customerEdit.gender ? this.customerEdit.genderName : 'Không chọn'
        this.$refs.gender.currentValue = { id: this.customerEdit.gender || 0, name: this.customerEdit.gender ? this.customerEdit.genderName : 'Không chọn' }
        $(this.$refs.container).find("#lastMiddleName").val(this.customerEdit.lastMiddleName)
        $(this.$refs.container).find("#firstName").val(this.customerEdit.firstName)
        // xử lý khi có họ và đệm hay ko
        if (!this.customerEdit.lastMiddleName) {
          this.firstName = this.customerEdit.firstName?.trim()
          $(this.$refs.container).find("#fullName").val(this.customerEdit.firstName?.trim())
        } else {
          this.firstName = this.customerEdit.firstName?.trim()
          this.lastMiddleName = this.customerEdit.lastMiddleName?.trim()
          $(this.$refs.container).find("#fullName").val(this.customerEdit.lastMiddleName?.trim().concat(' ' + this.customerEdit.firstName?.trim()))
        }
        $(this.$refs.container).find("#customerPhoneNum").val(this.customerEdit.customerPhoneNum)
        $(this.$refs.container).find("#companyPhoneNum").val(this.customerEdit.companyPhoneNum)
        $(this.$refs.container).find("#otherPhoneNum").val(this.customerEdit.otherPhoneNum)
        $(this.$refs.container).find("#zalo").val(this.customerEdit.zalo)
        $(this.$refs.container).find("#organizationName").val(this.customerEdit.organizationName)
        $(this.$refs.container).find("#customerEmail").val(this.customerEdit.customerEmail)
        $(this.$refs.container).find("#companyEmail").val(this.customerEdit.companyEmail)
        $(this.$refs.container).find("#taxCode").val(this.customerEdit.taxCode)
        this.customerEdit.disableCall && $(this.$refs.container).find("#disableCall").trigger("click")
        this.customerEdit.disableMail && $(this.$refs.container).find("#disableMail").trigger("click")
        if (this.customerEdit.potentialName) {
          const potentialArray = this.customerEdit.potentialName.split("\n ").map(item => {
            if (item !== '') {
              return item.trim()
            }
          }).filter(element => {
            return element !== undefined;
          });
          this.$refs.potentialNames.value = this.formatDataComboBox(potentialArray);
        }
        $(this.$refs.container).find("#facebook").val(this.customerEdit.facebook)

        // format dateOfBirth
        if (this.customerEdit?.dateOfBirth) {
          this.$refs.dateOfBirth.setDate(this.formatDateTime(this.customerEdit.dateOfBirth))
        }
      }
    },

    getSelectedCountry(selected) {
      this.countryName = selected
    },
    getSelectedCity(selected) {
      this.cityName = selected
    },
    getSelectedDistrict(selected) {
      this.districtName = selected
    },
    getSelectedWard(selected) {
      this.wardName = selected
    },
    getSelectedWard(selected) {
      this.wardName = selected
    },
    // xử lý dữ liệu theo format của combobox
    formatDataComboBox(data) {
      return data.map(item => {
        return { value: item, label: item }
      })
    },
    // lấy về dữ liệu cho Combobox
    async getDataComboBox(prefix) {
      let result = null;
      try {
        const response = await axios.get(`${rootApi}${prefix}`).then(res => res.data).catch(error => error.response.data)
        if (response.flag) {
          result = this.formatDataComboBox(response.data)

        } else {
          console.log(response.userMsg);
        }
      } catch (error) {
        console.log(error);

        result = null;
      }
      return result;
    },
    // lay du lieu datepicker
    handleGetCreatedAccountDate(date) {
      this.createdAccountAt = date
    },
    // lay du lieu datepicker
    handleGetBirthDate(date) {
      this.dateOfBirth = date
    },
    // xử lý checkbox
    checkInput(e) {
      e.preventDefault()
      handleClickFilterItem(e.target)
    },
    // set value cho lastMiddleName
    setLastMiddleName(value) {
      this.lastMiddleName = value
    },
    // set value cho firstName
    setFirstName(value) {
      this.firstName = value
    },
    // Xử lý hiển thị tỉnh thành phố chỉ khi chọn quốc gia
    handleShowCities() {
      this.handleGetAddress('countryId', 'cityId', 'Cities', 'Bạn chưa chọn quốc gia')
    },
    // Xử lý hiển thị quận/huyện chỉ khi chọn tỉnh thành phố
    handleShowDistricts() {
      this.handleGetAddress('cityId', 'districtId', 'Districts', 'Bạn chưa chọn tỉnh/thành phố')
    },
    // Xử lý hiển thị xã/ phường chỉ khi chọn quận/huyện
    handleShowWards() {
      this.handleGetAddress('districtId', 'wardId', 'Wards', 'Bạn chưa chọn quận/huyện')
    },
    // xử lý format dữ liệu của tên tiềm năng, ngành nghề, lĩnh vực khi lưu vào database
    handleDataWhenSave(data) {
      let result = '';
      if (data?.length > 0) {
        data.forEach(item => {
          result += item.value + " \n "
        })
      }
      return result === '' ? null : result
    },
    /**
     * xử lý hiển thị khu vực khi bấm chọn, không chọn được khu vực con khi chưa chọn cha
     * @param {*} parentAreaName 
     * @param {*} areaName 
     * @param {*} areaNames 
     * @param {*} actionFail 
     * Created by LVKIEN 28/08/2022
     */
    async handleGetAddress(parentAreaName, areaName, areaNames, actionFail) {
      try {
        if (this.$refs[parentAreaName] && this.$refs[parentAreaName].selected.id && this.$refs[parentAreaName].selected.id !== 0) {
          this.$refs.cityId.showLoading = true
          this.$refs.districtId.showLoading = true
          this.$refs.wardId.showLoading = true
          let areaResponse = await axios(`${rootApi}${areaNames}?${parentAreaName}=${this.$refs[parentAreaName].selected.id}`).then(res => res.data).catch(error => error.response.data)
          this.$refs.cityId.showLoading = false
          this.$refs.districtId.showLoading = false
          this.$refs.wardId.showLoading = false
          if (areaResponse.flag) {
            areaResponse = areaResponse.data
          } else {
            this.state = "fail"
            this.message = areaResponse.userMsg
            this.$refs.toast.isShow = true
            
            this.$refs.cityId.showNoValue = true
            this.$refs.districtId.showNoValue = true
            this.$refs.wardId.showNoValue = true
          }
          if (areaResponse.length > 0) {
            this.$refs[areaName].setOptions(handleTransferObject(areaResponse))
          }
        } else {
          this.$refs.cityId.optionsShown = false
          this.$refs.districtId.optionsShown = false
          this.$refs.wardId.optionsShown = false

          this.$refs[areaName].oldSearchFilter = false
          this.$refs[areaName].selected = {}
          this.state = "fail"
          this.message = actionFail
          this.$refs.toast.isShow = true
        }
      } catch (error) {
        this.state = "fail"
        this.message = error
        this.$refs.toast.isShow = true
      }
    },
    checkRequiredField(e) {
      if (!$(e.target).val()) {
        $(e.target).css('border-color', 'red')
        $(e.target).parent().after('<span style="color:red"}}>Tên không được phép để trống</span>')
      } else {
        $(e.target).css('border-color', '#D3D7DE')
        $(e.target).parent().next('span').remove()
      }
    },

    // format kiểu datetime từ backend thành format của frontend
    formatDateTime(dateString) {
      const rawResult = dateString.slice(0, 10)
      const dateStringArray = rawResult.split("-")
      return `${dateStringArray[2]}.${dateStringArray[1]}.${dateStringArray[0]}`
    },
    // format thời gian theo định dạng YYYY/MM/DD theo kiểu dữ liệu của Datetime
    formatDate(dateString) {
      if (dateString) {
        const result = dateString.split(".")
        return `${result[2]}-${result[1]}-${result[0]}`
      }
      return null
    },
    closeForm() {
      if (this.$route.name === "TiemNang") {
        this.$store.commit('setFormState', false)
      }
    },
    // ánh xạ dữ liệu vào đúng thuộc tính object
    handleMappingData(object) {
      const inputArr = $(".inputForm").toArray()
      const inputValueArr = inputArr.map(input => {
        return { [$(input).attr("id")]: $(input).val() }
      })
      const keysObject = Object.keys(object)
      keysObject.forEach(key => {
        const lowerCaseKey = this.lowerCaseFirstLetter(key);
        inputValueArr.forEach(input => {
          if (input[lowerCaseKey]) {
            object[key] = input[lowerCaseKey]
          }
        })
      })
      if (!this.editForm){
        const dropdownValueArr = dropdownField.map(dropdown => {
          if (this.$refs[dropdown]) {
            return { [dropdown]: this.$refs[dropdown].selected.id || null }
          }
        })
        keysObject.forEach(key => {
          const lowerCaseKey = this.lowerCaseFirstLetter(key);
          dropdownValueArr.forEach(dropdown => {
            if (dropdown[lowerCaseKey]) {
              object[key] = dropdown[lowerCaseKey]
            }
          })
        })
      }
    },
    // lấy dữ liệu form và gọi API
    // 15/08/2022 LVKien
    saveForm() {
      this.isSaveAndAdd = false
      this.$refs.showConfirm.isShow = true
    },
    async sendRequest(){
      if (this.isSaveAndAdd){
        await this.sendRequestSaveAndAdd()
      }else{
        await this.sendRequestAddOrUpdate()
      }
    },
    async sendRequestAddOrUpdate(){
      this.errorField = [{ firstName: '' }, { potentialCode: '' }]
      if (!this.editForm) {
        await this.saveInsertForm()
        this.closeForm()
      } else {
        await this.saveEditForm()
        if (!this.isSaveAndAdd){
          this.closeForm()
        }
      }
    },
    async saveInsertForm() {
      this.errorField = [{ firstName: '' }, { potentialCode: '' }]
      const potentialNames = this.handleDataWhenSave(this.$refs.potentialNames.value)
      const customer = new Customer()
      const careerNames = this.handleDataWhenSave(this.$refs.careerNames.value)
      const domainNames = this.handleDataWhenSave(this.$refs.domainNames.value)
      // Lấy dữ liệu các ô input trong form

      const address = new Address()
      const organization = new Organization()
      const potential = new Potential()

      organization.Career = careerNames
      organization.Domain = domainNames
      organization.CreatedAccountAt = this.formatDate(this.createdAccountAt)

      potential.PotentialName = potentialNames

      customer.Description = $("#addressName").val()
      customer.SharingUse = !!$("#sharingUse").attr("checked")

      address.AddressName = $("#addressName").val()

      this.handleMappingData(customer)
      this.handleMappingData(address)
      this.handleMappingData(organization)
      this.handleMappingData(potential)

      this.formatData(potential)
      this.formatData(address)
      this.formatData(organization)
      this.formatData(customer)

      console.log(customer, potential, address, organization)
      let resPotential, resAddress, resOrganization
      if (!customer.FirstName || customer.FirstName === '') {
        this.errorField = [...this.errorField, { firstName : 'Tên không được phép để trống' }]
        this.state = "fail"
        this.message = 'Tên không được phép để trống'
        this.$refs.toast.isShow = true
      } else {
        this.errorField.forEach(item=>item.firstName==='')
        if (!this.checkEmptyObject(potential)) {
          resPotential = await axios.post(`${rootApi}Potentials`, potential).then(res => res.data).catch(error => error.response.data)
          if (resPotential.flag) {
            console.log(resPotential)
          } else {
            // Nếu trùng mã tiềm năng
            if (resPotential?.devMsg === StatusCode.ErrorCode.DuplicatePotentialCode) {
              this.errorField = [...this.errorField, { potentialCode: resPotential?.userMsg }] 
              this.state = "fail"
              this.message = resPotential.userMsg
              this.$refs.toast.isShow = true
            } else {
              this.errorField.forEach(item => item.potentialCode === '')

            }
          }
        }
      }

      // nếu ko có lỗi
      if (this.checkEmptyError()) {
        if (!this.checkEmptyObject(address)) {
          resAddress = await axios.post(`${rootApi}Addresses`, address).then(res => res.data).catch(error => error.response.data)
          if (resAddress.flag) {
            console.log(resAddress)
          } else {
            console.log(resAddress.devMsg)
          }
        }
        if (!this.checkEmptyObject(organization)) {
          resOrganization = await axios.post(`${rootApi}Organizations`, organization).then(res => res.data).catch(error => error.response.data)
          if (resOrganization.flag) {
            console.log(resOrganization)
          } else {
            console.log(resOrganization.devMsg)
          }
        }
        // nếu ít nhất 1 trong 3 resPotential resAddress resOrganization thành công, gán các giá trị cho customer
        if (resPotential?.flag || resAddress?.flag || resOrganization?.flag) {
          customer.PotentialId = resPotential?.data.toString() || null
          customer.AddressId = resAddress?.data || null
          customer.OrganizationId = resOrganization?.data || null
          console.log(customer)
          this.isLoading = true;
          const resCustomer = await axios.post(`${rootApi}Customers`, customer).then(res => res.data).catch(error => error.response.data)
          this.isLoading = false;
          if (resCustomer.flag) {
            this.state = "success"
            this.message = "Thành công"
            this.$refs.toast.isShow = true
            this.$store.commit("setIsInserted", true)
          } else {
            this.state = "fail"
            this.message = resCustomer.userMsg
            this.$refs.toast.isShow = true
          }
          // nếu ko nhập address, potential, organization thì thêm luôn customer
        } else {
          this.isLoading = true;
          const resCustomer = await axios.post(`${rootApi}Customers`, customer).then(res => res.data).catch(error => error.response.data)
          this.isLoading = false;
          if (resCustomer.flag) {
            this.state = "success"
            this.message = "Thành công"
            this.$refs.toast.isShow = true

            this.$store.commit("setIsInserted", true)
          } else {
            this.state = "fail"
            this.message = resCustomer.userMsg
            this.$refs.toast.isShow = true
          }
        }
      }
    },
    async saveEditForm() {
      const potentialNames = this.handleDataWhenSave(this.$refs.potentialNames.value)
      const customerUpdate = new CustomerUpdate()
      if ($(this.$refs.container).find("#firstName").val() === '') {
        this.errorField = [...this.errorField, { firstName: 'Tên không được phép để trống' }]
        this.state = "fail"
        this.message = "Tên không được phép để trống"
        this.$refs.toast.isShow = true
      } else {
        this.errorField.forEach(item => item.firstName === '')
      }
      // xử lý khi không có lỗi
      if (this.checkEmptyError()) {
        this.handleMappingData(customerUpdate)
        this.getValueDropdown("Vocative", customerUpdate)
        this.getValueDropdown("DepartmentId", customerUpdate)
        this.getValueDropdown("PositionId", customerUpdate)
        this.getValueDropdown("SourceId", customerUpdate)
        this.getValueDropdown("Gender", customerUpdate)

        customerUpdate.PotentialName = potentialNames;
        customerUpdate.DisableCall = !!$(this.$refs.container).find("#disableCall").attr("checked")
        customerUpdate.DisableMail = !!$(this.$refs.container).find("#disableMail").attr("checked")
        customerUpdate.CustomerId = this.customerEdit.customerId || this.$store.state.customerUpdated.CustomerId
        customerUpdate.DateOfBirth = this.formatDate(this.dateOfBirth)
        customerUpdate.OrganizationId = this.customerEdit.organizationId
        customerUpdate.PotentialId = this.customerEdit.potentialId
        customerUpdate.ModifiedAt = this.customerEdit.modifiedAt
        this.formatData(customerUpdate)
        console.log(customerUpdate);
        this.isLoading = true;
        const resCustomer = await axios.put(`${rootApi}Customers/${customerUpdate.CustomerId}`, customerUpdate)
          .then(res => res.data).catch(error => error.response.data)
        this.isLoading = false;
        if (resCustomer.flag) {
          this.state = "success"
          this.message = "Thành công"
          this.$refs.toast.isShow = true
          this.$store.commit("setIsUpdated", true)
          this.$store.commit("setCustomerUpdated", customerUpdate)
        } else {
          this.state = "fail"
          this.message = resCustomer.userMsg
          this.$refs.toast.isShow = true
        }
      }
    },
    async sendRequestSaveAndAdd(){
      await this.sendRequestAddOrUpdate()
      if (!this.editForm) {
        this.openForm()
      }
      this.customerEdit= this.$store.state.customerUpdated
      console.log(this.customerEdit);
    },
    async saveAndAddForm(){
      this.isSaveAndAdd = true
      this.$refs.showConfirm.isShow = true
    },
    openForm() {
      if (this.$route.name === "TiemNang") {
        this.$store.commit('setFormState', true)
      }
    },
    /**
     * Lấy giá trị dropdown gán vào 1 object
     * @param {*} capitalizeValue 
     * @param {*} object 
     * created by LVKIEN 28/08/2022
     */
    getValueDropdown(capitalizeValue, object) {
      const toLowerCaseValue = this.lowerCaseFirstLetter(capitalizeValue)
      console.log(toLowerCaseValue);
      // nếu prop selected không có, tức là người dùng không sửa gì, lấy gtri current
      if (Object.keys(this.$refs[toLowerCaseValue].selected).length === 0) {
        object[capitalizeValue] = this.$refs[toLowerCaseValue].currentValue.id
      } else {
        // nếu id !== 0, tức là người dùng ko click vào ô "Không chọn"
        if (this.$refs[toLowerCaseValue].selected.id !== 0) {
          object[capitalizeValue] = this.$refs[toLowerCaseValue].selected.id
          // nếu id === 0, tức là người dùng click vào ô "Không chọn"
        } else {
          object[capitalizeValue] = null;
        }
      }
    },
    checkEmptyError(){
      let result = true;
      for (const key in this.errorField) {
        const itemKey = Object.keys(this.errorField[key]) 
        if (this.errorField[key][itemKey] !== '') {
          result = false
          break;
        }
      }
      return result;
    },
    capitalizeFirstLetter(string) {
      return string.charAt(0).toUpperCase() + string.slice(1);
    },
    lowerCaseFirstLetter(string) {
      return string.charAt(0).toLowerCase() + string.slice(1);
    },
    // check xem liệu object có trống hay ko
    checkEmptyObject(data) {
      let checkEmpty = true
      if (data) {
        const dataKeys = Object.keys(data)
        dataKeys.forEach(key => {
          if (data[key] !== null && data[key] !== '') {
            checkEmpty = false
          }
        })
      }
      return checkEmpty
    },
    formatData(data) {
      const dataKeys = Object.keys(data)
      dataKeys.forEach(key => {
        if (data[key] === 0 || data[key] === '') {
          data[key] = null
        }
      })
    }
  },
}
</script>

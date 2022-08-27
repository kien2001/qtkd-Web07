<template>
  <div class="form-submit-container" ref="container">
    <div class="form-submit">
      <div class="form-header">
        <div class="header-title">
          <div class="header-title-context">
            {{ editForm ? "Sửa tiềm năng" : "Thêm Tiềm năng" }}<span>- Mẫu tiêu chuẩn</span>
          </div>
          <div class="edit-btn">Sửa bố cục</div>
        </div>
        <div class="header-btn">
          <KienButton name="Huỷ bỏ" color="#fff" @clickBtn="closeForm" colorHover="#D0D8FB" />
          <KienButton name="Lưu và thêm" color="#fff" colorHover="#D0D8FB" />
          <KienButton name="Lưu" color="#4262F0" colorHover="#2B4EEE" @clickBtn="saveForm" colorText="#FFFFFF" />
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
                <DropDown @selected="validateSelection" id="vocative" :showInput="true" placeholder="-Không chọn-"
                  :fetchDataWhenClick="false" name="Vocatives" ref="vocative" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Họ và đệm</div>
              <div class="context-input">
                <InputFormVue id="lastMiddleName" @changeName="setLastMiddleName" ref="lastMiddleName" />
              </div>
            </div>
            <div class="context-item" required>
              <div class="context-label">Tên</div>
              <div class="context-input">
                <InputFormVue id="firstName" @changeName="setFirstName" ref="firstName" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Họ và tên</div>
              <div class="context-input">
                <InputFormVue id="fullName" :isDisabled="true" :handleFullName="handleFullName" ref="fullName" />
              </div>
            </div>
            <div class=" context-item">
              <div class="context-label">Phòng ban</div>
              <div class="context-input">
                <DropDown @selected="validateSelection" :fetchDataWhenClick="false" id="departmentName"
                  :showInput="true" placeholder="-Không chọn-" name="Departments" ref="department" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Chức danh</div>
              <div class="context-input">
                <DropDown @selected="validateSelection" :fetchDataWhenClick="false" id="positionName" :showInput="true"
                  placeholder="-Không chọn-" name="Positions" ref="position" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">
                ĐT di động
                <Tooltip tooltipText="Điện thoại di động" />
              </div>
              <div class="context-input">
                <InputFormVue id="customerPhoneNum" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">
                ĐT cơ quan
                <Tooltip tooltipText="Điện thoại cơ quan" />
              </div>
              <div class="context-input">
                <InputFormVue id="companyPhoneNum" />
              </div>
            </div>
            <div class="context-item" v-if="editForm">
              <div class="context-label">
                ĐT khác
                <Tooltip tooltipText="Điện thoại khác" />
              </div>
              <div class="context-input">
                <InputFormVue id="otherPhoneNum" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Nguồn gốc</div>
              <div class="context-input">
                <DropDown @selected="validateSelection" id="sourceName" :showInput="true" placeholder="-Không chọn-"
                  :fetchDataWhenClick="false" name="Sources" ref="source" />

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
                <InputFormVue id="zalo" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Tổ chức</div>
              <div class="context-input">
                <InputFormVue id="organizationName" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Email cá nhân</div>
              <div class="context-input">
                <InputFormVue id="customerEmail" />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Email cơ quan</div>
              <div class="context-input">
                <InputFormVue id="companyEmail" />
              </div>
            </div>
            <div class="context-item" v-if="editForm"></div>
            <div class="context-item">
              <div class="context-label">Mã số thuế</div>
              <div class="context-input">
                <InputFormVue id="taxCode" />
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
                <DropDown @selected="validateSelection" id="genderName" :showInput="true" placeholder="-Không chọn-"
                  :fetchDataWhenClick="false" name="Genders" ref="gender" />
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
                <InputFormVue id="facebook" />
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
                  <InputFormVue id="bankAccount" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Mở tại ngân hàng</div>
                <div class="context-input">
                  <InputFormVue id="bankName" />
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
                  <DropDown @selected="validateSelection" id="typeName" :showInput="true" placeholder="-Không chọn-"
                    :fetchDataWhenClick="false" name="Types" />
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
                  <DropDown @selected="validateSelection" id="revenueName" :showInput="true" placeholder="-Không chọn-"
                    :fetchDataWhenClick="false" name="Revenues" />
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
                  <DropDown @selected="validateSelection" id="country" :showInput="true" placeholder="-Không chọn-"
                    :fetchDataWhenClick="false" name="Countries" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Tỉnh/Thành phố</div>
                <div class="context-input">
                  <DropDown @selected="validateSelection" id="city" @click="handleShowCities" :showInput="true"
                    :fetchDataWhenClick="false" placeholder="-Không chọn-" name="Cities" ref="city" />

                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Quận/Huyện</div>
                <div class="context-input">
                  <DropDown @selected="validateSelection" id="district" @click="handleShowDistricts" :showInput="true"
                    :fetchDataWhenClick="false" placeholder="-Không chọn-" name="Districts" ref="district" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Phường/Xã</div>
                <div class="context-input">
                  <DropDown @selected="validateSelection" id="ward" @click="handleShowWards" :showInput="true"
                    :fetchDataWhenClick="false" placeholder="-Không chọn-" name="Wards" ref="ward" />

                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Số nhà, Đường phố</div>
                <div class="context-input">
                  <InputFormVue id="homeNumber" @changeName="setHomeNumber" />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Mã vùng</div>
                <div class="context-input">
                  <InputFormVue id="postalCode" />
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
                  <InputFormVue id="potentialCode" ref="potential-code" />
                </div>
              </div>
            </div>
          </div>
        </template>

      </div>
    </div>
  </div>

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
import { rootApi } from '@/js/config'
import Customer from '../entities/Customer'
import Organization from '../entities/Organization'
import Address from '../entities/Address'
import Potential from '../entities/Potential'
import handleClickFilterItem from '../js/checkbox'
import InputFormVue from './InputForm.vue'
import DatePicker from './DatePicker.vue'
export default {
  name: 'FormSubmit',
  data() {
    return {
      dropdownSelected: {},
      lastMiddleName: '',
      firstName: '',
      homeNumber: '',
      addressName: '',
      createdAccountAt: '',
      dateOfBirth: '',
      errorField: {
        firstName: '',
        potentialCode: ''
      },
      prefixPotentialName: "Potentials/PotentialNames",
      prefixCareerName: "Organizations/Careers",
      prefixDomainName: "Organizations/Domains",
      editForm: this.$store.state.editForm,
      customerEdit: this.$parent.customerEdit
    }
  },
  async mounted() {
    const checkboxItems = $('.context-input>.icon.icon-checkbox').toArray()
    console.log(checkboxItems)
    checkboxItems.forEach((item) => {
      $(item).click(this.checkInput)
      $(item).css("background-color", "#fff!important")
    })
    if (this.editForm) {
      console.log(this.customerEdit);

      this.$refs.vocative.oldSearchFilter = this.customerEdit.vocative ? this.customerEdit.vocativeName : "Không chọn"
      this.$refs.vocative.currentValue = { id: this.customerEdit.vocative || 0, name: this.customerEdit.vocative ? this.customerEdit.vocativeName : "Không chọn" }

      this.$refs.department.oldSearchFilter = this.customerEdit.departmentId ? this.customerEdit.departmentName : "Không chọn"
      this.$refs.department.currentValue = { id: this.customerEdit.departmentId || 0, name: this.customerEdit.departmentId ? this.customerEdit.departmentName : "Không chọn" }

      this.$refs.position.oldSearchFilter = this.customerEdit.positionId ? this.customerEdit.positionName : "Không chọn"
      this.$refs.position.currentValue = { id: this.customerEdit.positionId || 0, name: this.customerEdit.positionId ? this.customerEdit.positionName : "Không chọn" }

      this.$refs.source.oldSearchFilter = this.customerEdit.sourceId ? this.customerEdit.sourceName : "Không chọn"
      this.$refs.source.currentValue = { id: this.customerEdit.sourceId || 0, name: this.customerEdit.sourceId ? this.customerEdit.sourceName : "Không chọn" }

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

    const requiredFields = $('.context-item[required]>.context-input>input').toArray()
    requiredFields.forEach(field => {
      $(field).blur(this.checkRequiredField)
      $(field).focus(function (e) {
        $(e.target).css('border-color', '#D3D7DE')
        $(e.target).next('span').remove()
      })
    })
    // xử lý hiển thị cho checkbox
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
        console.log(data.data)
        console.log(this.$refs['potential-code'])

        this.$refs['potential-code'].name = data.data
      }
    }

  },
  computed: {
    handleFullName() {
      return `${this.lastMiddleName} ${this.firstName}`
    },
    getCountry() {
      return this.dropdownSelected?.country?.id
    },
    getCity() {
      return this.dropdownSelected?.city?.id
    },
    getDistrict() {
      return this.dropdownSelected?.district?.id
    },
    getWard() {
      return this.dropdownSelected?.ward?.id
    }
  },
  watch: {
    getCountry() {
      console.log(1)
      this.dropdownSelected.city = {}
      this.$refs.city.setOptions([])

      this.dropdownSelected.district = {}
      this.$refs.district.setOptions([])

      this.dropdownSelected.ward = {}
      this.$refs.ward.setOptions([])
      if (this.dropdownSelected?.country?.id !== 0) {
        const templateAddressName = `${this.homeNumber || ''}  ${this.dropdownSelected?.ward?.name || ''} ${this.dropdownSelected?.district?.name || ''} ${this.dropdownSelected?.city?.name || ''} ${this.dropdownSelected?.country?.name || ''}`
        this.addressName = templateAddressName.trim()
      }
    },
    getCity() {
      this.dropdownSelected.district = {}
      this.$refs.district.setOptions([])

      this.dropdownSelected.ward = {}
      this.$refs.ward.setOptions([])

      if (this.dropdownSelected?.city?.id !== 0) {
        const templateAddressName = `${this.homeNumber || ''}  ${this.dropdownSelected?.ward?.name || ''} ${this.dropdownSelected?.district?.name || ''} ${this.dropdownSelected?.city?.name || ''} ${this.dropdownSelected?.country?.name || ''}`
        this.addressName = templateAddressName.trim()
      }
    },
    getDistrict() {
      this.dropdownSelected.ward = {}
      this.$refs.ward.setOptions([])

      if (this.dropdownSelected?.district?.id !== 0) {
        const templateAddressName = `${this.homeNumber || ''}  ${this.dropdownSelected?.ward?.name || ''} ${this.dropdownSelected?.district?.name || ''} ${this.dropdownSelected?.city?.name || ''} ${this.dropdownSelected?.country?.name || ''}`
        this.addressName = templateAddressName.trim()
      }
    },
    getWard() {
      if (this.dropdownSelected?.ward?.id) {
        const templateAddressName = `${this.homeNumber || ''}  ${this.dropdownSelected?.ward?.name || ''} ${this.dropdownSelected?.district?.name || ''} ${this.dropdownSelected?.city?.name || ''} ${this.dropdownSelected?.country?.name || ''}`
        this.addressName = templateAddressName.trim()
      }
    },
    homeNumber() {
      const templateAddressName = `${this.homeNumber || ''}  ${this.dropdownSelected?.ward?.name || ''} ${this.dropdownSelected?.district?.name || ''} ${this.dropdownSelected?.city?.name || ''} ${this.dropdownSelected?.country?.name || ''}`
      this.addressName = templateAddressName.trim()
    }
  },
  components: { InputFormVue, DatePicker },
  template: 'FormSubmit',
  methods: {
    // set object customerEdit
    setCustomerEdit(customer) {
      this.customerEdit = customer
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
      const response = await axios.get(`${rootApi}${prefix}`).then(res => res.data).catch(error => error.response.data)
      if (response.flag) {

        result = this.formatDataComboBox(response.data)

      } else {
        console.log(response.userMsg);
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
    setHomeNumber(value) {
      this.homeNumber = value
    },
    // Xử lý hiển thị tỉnh thành phố chỉ khi chọn quốc gia
    handleShowCities() {
      this.handleGetAddress('country', 'city', 'Cities', 'Bạn chưa chọn quốc gia')
    },
    // Xử lý hiển thị quận/huyện chỉ khi chọn tỉnh thành phố
    handleShowDistricts() {
      this.handleGetAddress('city', 'district', 'Districts', 'Bạn chưa chọn tỉnh/thành phố')
    },
    // Xử lý hiển thị xã/ phường chỉ khi chọn quận/huyện
    handleShowWards() {
      this.handleGetAddress('district', 'ward', 'Wards', 'Bạn chưa chọn quận/huyện')
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
    // xử lý hiển thị khu vực khi bấm chọn, không chọn được khu vực con khi chưa chọn cha
    async handleGetAddress(parentAreaName, areaName, areaNames, actionFail) {
      try {
        if (this.dropdownSelected[parentAreaName] && this.dropdownSelected[parentAreaName].id && this.dropdownSelected[parentAreaName].id !== 0) {
          this.$refs.city.showLoading=true
          this.$refs.district.showLoading=true
          this.$refs.ward.showLoading=true
          let areaArray = await axios(`${rootApi}${areaNames}?${parentAreaName}Id=${this.dropdownSelected[parentAreaName].id}`).then(res => res.data).catch(error => error.response.data)
          this.$refs.city.showLoading = false
          this.$refs.district.showLoading = false
          this.$refs.ward.showLoading = false
          if (areaArray.flag) {
            areaArray = areaArray.data
          } else {
            this.$refs.city.showNoValue = true
            this.$refs.district.showNoValue = true
            this.$refs.ward.showNoValue = true
            console.log(areaArray.userMsg)
          }
          if (areaArray.length > 0) {
            this.$refs[areaName].setOptions(this.handleTransferObject(areaArray))
          }
          // else {
          //   console.log(areaArray);
          // }
        } else {
          this[areaName] = []
          this.$refs.city.optionsShown = false
          this.$refs.district.optionsShown = false
          this.$refs.ward.optionsShown = false
          alert(actionFail)
        }
      } catch (error) {
        console.log(error)
      }
    },
    // set giá trị hiện tại của dropdown
    validateSelection(selected) {
      if (typeof selected === 'object') {
        this.dropdownSelected.department = selected?.departmentName
        this.dropdownSelected.vocative = selected?.vocative
        this.dropdownSelected.position = selected?.positionName
        this.dropdownSelected.source = selected?.sourceName
        this.dropdownSelected.type = selected?.typeName
        this.dropdownSelected.country = selected?.country
        this.dropdownSelected.revenue = selected?.revenueName
        this.dropdownSelected.city = selected?.city
        this.dropdownSelected.district = selected?.district
        this.dropdownSelected.ward = selected?.ward
      }
    },
    checkRequiredField(e) {
      if (!$(e.target).val()) {
        $(e.target).css('border-color', 'red')
        $(e.target).after('<span style="color:red"}}>Tên không được phép để trống</span>')
      } else {
        $(e.target).css('border-color', '#D3D7DE')
        $(e.target).next('span').remove()
      }
    },
    // format dữ liệu theo định dạng của dropdown theo dạng mảng: {id:, name:}
    handleTransferObject(objectArr) {
      const objectKeys = Object.keys(objectArr[0])
      console.log(objectArr);
      let initResult = [{ id: 0, name: '-Không chọn-' }]
      const result = objectArr.map(item => {
        const resultObject = { id: item[objectKeys[0]], name: item[objectKeys[1]] }

        return resultObject
      })
      initResult = [...initResult, ...result]
      return initResult
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
        this.$store.commit('setEditForm', false)

      }
    },
    // lấy dữ liệu form và gọi API
    // 15/08/2022 LVKien
    async saveForm() {
      this.errorField.firstName = ''

      const potentialNames = this.handleDataWhenSave(this.$refs.potentialNames.value)
      const customer = new Customer()

      if (!this.editForm) {
        const careerNames = this.handleDataWhenSave(this.$refs.careerNames.value)
        const domainNames = this.handleDataWhenSave(this.$refs.domainNames.value)
        // set lại giá trị lỗi
        this.errorField.potentialCode = ''
        // Lấy dữ liệu các ô input trong form
        const formMainItems = $('.form-main-item').toArray()
        const arrayResult = formMainItems.map(item => {
          // lấy tất cả các ô input trong form
          const inputChild = $(item).children('.form-main-context').children('.context-item').children('.context-input').children(':not([disabled], .dropdown)').toArray()
          const inputChildValue = inputChild.map(itemChild => {
            const idItemChild = $(itemChild).attr('id')
            if ($(itemChild).attr('id') === 'sharingUse') {
              return { [idItemChild]: !!$(itemChild).attr('checked') }
            }
            return { [idItemChild]: $(itemChild).val() }
          })
          const idPartition = $(item).attr('id')
          return { [idPartition]: inputChildValue }
        })
        const address = new Address()
        const organization = new Organization()
        const potential = new Potential()
        console.log(arrayResult);
        customer.setInsertCustomer(this.dropdownSelected.vocative?.id || '', arrayResult[1]['customer-partition'][0].lastMiddleName, arrayResult[1]['customer-partition'][1].firstName, this.dropdownSelected.department?.id || '', this.dropdownSelected.position?.id || '', arrayResult[1]['customer-partition'][2].customerPhoneNum, arrayResult[1]['customer-partition'][3].companyPhoneNum, this.dropdownSelected.source?.id || '', arrayResult[1]['customer-partition'][5].zalo, arrayResult[1]['customer-partition'][6].customerEmail, arrayResult[1]['customer-partition'][7].companyEmail, arrayResult[1]['customer-partition'][8].taxCode, arrayResult[4]['description-partition'][0].description, arrayResult[5]['system-partition'][0].sharingUse)
        address.setInsertAddress(arrayResult[3]['address-partition'][2].addressName, arrayResult[3]['address-partition'][0].homeNumber, arrayResult[3]['address-partition'][1].postalCode, this.dropdownSelected.country?.id || '', this.dropdownSelected.city?.id || '', this.dropdownSelected.district?.id || '', this.dropdownSelected.ward?.id || '')
        organization.setInsertOrganization(arrayResult[2]['organization-partition'][0].bankAccount, arrayResult[2]['organization-partition'][1].bankName, this.formatDate(this.createdAccountAt), this.dropdownSelected.type?.id || '', domainNames, careerNames, this.dropdownSelected.revenue?.id || '')
        potential.setInsertPotential(potentialNames, arrayResult[5]['system-partition'][1].potentialCode)
        this.formatData(potential)
        this.formatData(address)
        this.formatData(organization)
        this.formatData(customer)
        console.log(customer, potential, address, organization)
        let resPotential, resAddress, resOrganization
        if (!customer.FirstName || customer.FirstName === '') {
          this.errorField.firstName = 'Tên không được phép để trống'
        } else {
          this.errorField.firstName = ''
          if (!this.checkEmptyObject(potential)) {
            resPotential = await axios.post(`${rootApi}Potentials`, potential).then(res => res.data).catch(error => error.response.data)
            if (resPotential.flag) {
              console.log(resPotential)
            } else {
              // Nếu trùng mã tiềm năng
              if (resPotential?.devMsg === 'E002') {
                this.errorField.potentialCode = resPotential?.userMsg
              } else {
                this.errorField.potentialCode = ''
              }
            }
          }
        }

        // nếu ko có lỗi
        if (this.checkEmptyObject(this.errorField)) {
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
          if (resPotential.flag || resAddress.flag || resOrganization.flag) {
            customer.PotentialId = resPotential?.data.toString() || null
            customer.AddressId = resAddress?.data || null
            customer.OrganizationId = resOrganization?.data || null
            console.log(customer)
            const resCustomer = await axios.post(`${rootApi}Customers`, customer).then(res => res.data).catch(error => error.response.data)
            if (resCustomer.flag) {
              alert('Thành công')
            } else {
              console.log(resCustomer.data)
            }
            // nếu ko nhập address, potential, organization thì thêm luôn customer
          } else {
            const resCustomer = await axios.post(`${rootApi}Customers`, customer).then(res => res.data).catch(error => error.response.data)
            if (resCustomer.flag) {
              alert('Thành công')
            } else {
              console.log(resCustomer.data)
            }
          }
          // nếu có lỗi
        } else {
          // xử lý hiển thị lỗi
          const potentialCodeField = $(this.$refs.container).find('#potentialCode')
          const firstNameField = $(this.$refs.container).find('#firstName')
          if (this.errorField.firstName !== '') {
            $(firstNameField).next('span').remove()
            $(firstNameField).after(`<span style="color:red"}}>${this.errorField.firstName}</span>`)
          } else {
            $(firstNameField).next('span').remove()
          }
          if (this.errorField.potentialCode !== '') {
            $(potentialCodeField).next('span').remove()
            $(potentialCodeField).after(`<span style="color:red"}}>${this.errorField.potentialCode}</span>`)
          } else {
            $(potentialCodeField).next('span').remove()
          }
        }
      } else {
        this.saveEditForm(customer, potentialNames)
      }
    },
    async saveEditForm(customer, potentialNames) {
      if ($(this.$refs.container).find("#firstName").val() === '') {
        this.errorField.firstName = 'Tên không được phép để trống'
      } else {
        this.errorField.firstName = ''
      }
      // xử lý khi không có lỗi
      if (this.checkEmptyObject(this.errorField)) {
        const fieldValues = $(this.$refs.container).find(".context-input").children("input:not([disabled])").toArray()
        const idFieldInput = fieldValues.map(item => {
          return this.capitalizeFirstLetter($(item).attr("id"))
        })
        idFieldInput.forEach(id => {
          const lowerCaseId = this.lowerCaseFirstLetter(id)
          customer[id] = $(this.$refs.container).find(`#${lowerCaseId}`).val()
        })
        this.getValueDropdown("Vocative", customer)
        this.getValueDropdown("Department", customer)
        this.getValueDropdown("Position", customer)
        this.getValueDropdown("Source", customer)
        this.getValueDropdown("Gender", customer)


        customer.PotentialName = potentialNames;
        customer.DisableCall = !!$(this.$refs.container).find("#disableCall").attr("checked")
        customer.DisableMail = !!$(this.$refs.container).find("#disableMail").attr("checked")
        customer.CustomerId = this.customerEdit.customerId
        customer.DateOfBirth = this.formatDate(this.dateOfBirth)
        customer.OrganizationId = this.customerEdit.organizationId
        customer.PotentialId = this.customerEdit.potentialId
        customer.ModifiedAt = this.customerEdit.modifiedAt
        this.formatData(customer)
        console.log(customer);
        const resCustomer = await axios.put(`${rootApi}Customers/${customer.CustomerId}`,customer)
          .then(res => res.data).catch(error => error.response.data)
        if (resCustomer.flag) {
          alert('Thành công')
        } else {
          console.log(resCustomer.data)
        }
      }
      // nếu có lỗi
      else {
        // xử lý hiển thị lỗi
        const firstNameField = $(this.$refs.container).find('#firstName')
        if (this.errorField.firstName !== '') {
          $(firstNameField).next('span').remove()
          $(firstNameField).after(`<span style="color:red"}}>${this.errorField.firstName}</span>`)
        } else {
          $(firstNameField).next('span').remove()
        }
      }
    },
    getValueDropdown(capitalizeValue, customer) {
      const toLowerCaseValue = this.lowerCaseFirstLetter(capitalizeValue)
      // nếu prop selected không có, tức là người dùng không sửa gì, lấy gtri current
      if (Object.keys(this.$refs[toLowerCaseValue].selected).length === 0) {
        customer[capitalizeValue] = this.$refs[toLowerCaseValue].currentValue.id
      } else {
        // nếu id !== 0, tức là người dùng ko click vào ô "Không chọn"
        if (this.$refs[toLowerCaseValue].selected.id !== 0) {
          customer[capitalizeValue] = this.$refs[toLowerCaseValue].selected.id
          // nếu id === 0, tức là người dùng click vào ô "Không chọn"
        } else {
          customer[capitalizeValue] = null;
        }
      }
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
      // console.log(checkEmpty);
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
  }
}
</script>

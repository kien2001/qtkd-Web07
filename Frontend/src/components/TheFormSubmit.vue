<template>
  <div class="form-submit-container" ref="container">
    <template v-if="getErrorField">
      <TheToastMessage
        v-for="error in getErrorField"
        :key="error"
        :state="error.state"
        :message="error.message"
        ref="errorField"
      />
    </template>
    <div class="form-submit">
      <div class="form-header">
        <div class="header-title">
          <div class="header-title-context">
            {{ editForm ? "Sửa tiềm năng" : "Thêm Tiềm năng"
            }}<span>- Mẫu tiêu chuẩn</span>
          </div>
          <div class="edit-btn">Sửa bố cục</div>
        </div>
        <div class="header-btn">
          <TheButton
            @keyup.enter="closeForm"
            name="Huỷ bỏ"
            color="#fff"
            @clickBtn="closeForm"
            colorHover="#D0D8FB"
            border="#fff"
            borderHover="#D3D7DE"
          />
          <TheButton
            @keyup.enter="saveAndAddForm"
            name="Lưu và thêm"
            color="#fff"
            colorHover="#D0D8FB"
            border="#fff"
            borderHover="#D3D7DE"
            @clickBtn="saveAndAddForm"
          />
          <TheButton
            @keyup.enter="saveForm"
            name="Lưu"
            color="#4262F0"
            colorHover="#2B4EEE"
            @clickBtn="saveForm"
            colorText="#FFFFFF"
          />
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
                <TheDropDown
                  id="vocative"
                  :showInput="true"
                  placeholder="-Không chọn-"
                  :fetchDataWhenClick="false"
                  name="Vocatives"
                  ref="vocative"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Họ và đệm</div>
              <div class="context-input">
                <TheInputForm
                  class="inputForm"
                  id="lastMiddleName"
                  @changeName="setLastMiddleName"
                  maxlength="50"
                  ref="lastMiddleName"
                />
              </div>
            </div>
            <div class="context-item" required>
              <div class="context-label">Tên</div>
              <div class="context-input">
                <TheInputForm
                  class="inputForm"
                  id="firstName"
                  @changeName="setFirstName"
                  ref="firstName"
                  maxlength="50"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Họ và tên</div>
              <div class="context-input">
                <TheInputForm
                  class="inputForm"
                  id="fullName"
                  :isDisabled="true"
                  ref="fullName"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Phòng ban</div>
              <div class="context-input">
                <TheDropDown
                  :fetchDataWhenClick="false"
                  id="departmentName"
                  :showInput="true"
                  placeholder="-Không chọn-"
                  name="Departments"
                  ref="departmentId"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Chức danh</div>
              <div class="context-input">
                <TheDropDown
                  :fetchDataWhenClick="false"
                  id="positionName"
                  :showInput="true"
                  placeholder="-Không chọn-"
                  name="Positions"
                  ref="positionId"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">
                ĐT di động
                <TheTooltip tooltipText="Điện thoại di động" />
              </div>
              <div class="context-input">
                <TheInputForm
                  class="inputForm"
                  id="customerPhoneNum"
                  ref="customerPhoneNum"
                  :isTel="true"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">
                ĐT cơ quan
                <TheTooltip tooltipText="Điện thoại cơ quan" />
              </div>
              <div class="context-input">
                <TheInputForm
                  class="inputForm"
                  id="companyPhoneNum"
                  ref="companyPhoneNum"
                  :isTel="true"
                />
              </div>
            </div>
            <div class="context-item" v-if="editForm">
              <div class="context-label">
                ĐT khác
                <TheTooltip tooltipText="Điện thoại khác" />
              </div>
              <div class="context-input">
                <TheInputForm
                  class="inputForm"
                  id="otherPhoneNum"
                  :isTel="true"
                  ref="otherPhoneNum"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Nguồn gốc</div>
              <div class="context-input">
                <TheDropDown
                  id="sourceName"
                  :showInput="true"
                  placeholder="-Không chọn-"
                  :fetchDataWhenClick="false"
                  name="Sources"
                  ref="sourceId"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Loại tiềm năng</div>
              <div class="context-input">
                <TheComboBox
                  :options="
                    async () => {
                      return await this.getDataComboBox(
                        this.prefixPotentialName
                      );
                    }
                  "
                  ref="potentialNames"
                />
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
                <TheInputForm
                  class="inputForm"
                  id="zalo"
                  :isTel="true"
                  ref="zalo"
                />
                <!-- @keyup.prevent="handleValidateTel" -->
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Tổ chức</div>
              <div class="context-input">
                <TheInputForm
                  class="inputForm"
                  id="organizationName"
                  ref="organizationName"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Email cá nhân</div>
              <div class="context-input">
                <TheInputForm
                  class="inputForm"
                  id="customerEmail"
                  :isEmail="true"
                  ref="customerEmail"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Email cơ quan</div>
              <div class="context-input">
                <TheInputForm
                  class="inputForm"
                  id="companyEmail"
                  :isEmail="true"
                  ref="companyEmail"
                />
              </div>
            </div>
            <div class="context-item" v-if="editForm"></div>
            <div class="context-item">
              <div class="context-label">Mã số thuế</div>
              <div class="context-input">
                <TheInputForm class="inputForm" id="taxCode" ref="taxCode" />
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
                <TheDropDown
                  id="genderName"
                  :showInput="true"
                  placeholder="-Không chọn-"
                  :fetchDataWhenClick="false"
                  name="Genders"
                  ref="gender"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Ngày sinh</div>
              <div class="context-input">
                <TheDatepicker
                  inputClassName="dateOfBirth"
                  ref="dateOfBirth"
                  @getDate="handleGetBirthDate"
                />
              </div>
            </div>
            <div class="context-item">
              <div class="context-label">Facebook</div>
              <div class="context-input">
                <TheInputForm class="inputForm" id="facebook" ref="facebook" />
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
                  <TheInputForm
                    class="inputForm"
                    id="bankAccount"
                    ref="bankAccount"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Mở tại ngân hàng</div>
                <div class="context-input">
                  <TheInputForm
                    class="inputForm"
                    id="bankName"
                    ref="bankName"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Ngày thành lập</div>
                <div class="context-input">
                  <TheDatepicker
                    inputClassName="createdAccountAt"
                    @getDate="handleGetCreatedAccountDate"
                    ref="createdAccountAt"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Loại hình</div>
                <div class="context-input">
                  <TheDropDown
                    id="typeName"
                    :showInput="true"
                    placeholder="-Không chọn-"
                    :fetchDataWhenClick="false"
                    name="Types"
                    ref="typeId"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Lĩnh vực</div>
                <div class="context-input">
                  <TheComboBox
                    :options="
                      async () => {
                        return await this.getDataComboBox(
                          this.prefixDomainName
                        );
                      }
                    "
                    ref="domainNames"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Ngành nghề</div>
                <div class="context-input">
                  <TheComboBox
                    :options="
                      async () => {
                        return await this.getDataComboBox(
                          this.prefixCareerName
                        );
                      }
                    "
                    ref="careerNames"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Doanh thu</div>
                <div class="context-input">
                  <TheDropDown
                    id="revenueName"
                    :showInput="true"
                    placeholder="-Không chọn-"
                    :fetchDataWhenClick="false"
                    name="Revenues"
                    ref="revenueId"
                  />
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
                  <TheDropDown
                    id="country"
                    @selected="getSelectedCountry"
                    :showInput="true"
                    placeholder="-Không chọn-"
                    :fetchDataWhenClick="false"
                    name="Countries"
                    ref="countryId"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Tỉnh/Thành phố</div>
                <div class="context-input">
                  <TheDropDown
                    id="city"
                    @selected="getSelectedCity"
                    @click="handleShowCities"
                    :showInput="true"
                    :fetchDataWhenClick="false"
                    placeholder="-Không chọn-"
                    name="Cities"
                    ref="cityId"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Quận/Huyện</div>
                <div class="context-input">
                  <TheDropDown
                    id="district"
                    @selected="getSelectedDistrict"
                    @click="handleShowDistricts"
                    :showInput="true"
                    :fetchDataWhenClick="false"
                    placeholder="-Không chọn-"
                    name="Districts"
                    ref="districtId"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Phường/Xã</div>
                <div class="context-input">
                  <TheDropDown
                    id="ward"
                    @selected="getSelectedWard"
                    @click="handleShowWards"
                    :showInput="true"
                    :fetchDataWhenClick="false"
                    placeholder="-Không chọn-"
                    name="Wards"
                    ref="wardId"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Số nhà, Đường phố</div>
                <div class="context-input">
                  <TheInputForm
                    class="inputForm"
                    id="homeNumber"
                    ref="homeNumber"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Mã vùng</div>
                <div class="context-input">
                  <TheInputForm
                    class="inputForm"
                    id="postalCode"
                    ref="postalCode"
                  />
                </div>
              </div>
              <div class="context-item">
                <div class="context-label">Địa chỉ</div>
                <div class="context-input">
                  <textarea
                    name=""
                    id="addressName"
                    class="text-area"
                    cols="30"
                    rows="10"
                    v-model="addressName"
                  ></textarea>
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
                  <textarea
                    name=""
                    id="description"
                    class="text-area"
                    cols="30"
                    rows="10"
                  ></textarea>
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
                  <TheInputForm
                    class="inputForm"
                    id="potentialCode"
                    ref="potentialCode"
                  />
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
  --width-column: 450px;
  --gap-column: 50px;
}

.form-submit {
  width: 100%;
  height: 100vh;
  background-color: #fff;
  /* max-height: 700px;
  border-radius: 4px; */
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

.header-title-context > span {
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
  height: calc(100% - 52px);
  display: flex;
  flex-direction: column;
  gap: 30px;
  padding: 20px 50px;
  border-radius: 0 0 4px 4px;
}
.form-main::-webkit-scrollbar-thumb:hover {
  background-color: #99a1b2;
}
.form-main::-webkit-scrollbar-thumb:active {
  background-color: #7c869c;
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
  grid-template-columns: repeat(2, var(--width-column));
  grid-template-rows: auto;
  column-gap: var(--gap-column);
  row-gap: 15px;
  grid-auto-rows: minmax(1px, auto);
}

#description-partition > .form-main-context {
  grid-template-columns: calc(var(--width-column) * 2 + var(--gap-column));
}

#description-partition .context-input {
  flex-basis: 86%;
}

#address-partition > .form-main-context > .context-item:last-child,
#description-partition > .form-main-context > .context-item:last-child {
  height: 100px;
}

.text-area {
  min-height: 100px;
  max-height: 100px;
  border-radius: 4px;
  border-width: 1px;
  border-style: solid;
  border-color: #d3d7de;
  outline: none;
  padding: 5px 10px;
  background-color: #fff;
  font-size: 13px;
  line-height: 16px;
  font-weight: 400;
  color: #1f2229;
  resize: none;
  transition: border-color 0.2s cubic-bezier(0.645, 0.045, 0.355, 1);
}
.text-area:not([disabled]):hover,
.text-area:not([disabled]):focus,
.text-area:not([disabled]):active {
  border-color: #4262f0;
}

.context-item {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
}

.context-item[required] > .context-label:after {
  content: "*";
  color: red;
  margin-left: 4px;
}

.context-input {
  position: relative;
  flex-basis: 70%;
  display: flex;
  gap: 8px;
  height: 100%;
}

.context-input:where(.context-input > #sharingUse) {
  display: block;
}

.context-input > * {
  width: 100%;
  height: fit-content;
}

.context-label {
  display: flex;
  align-items: center;
  margin-top: 16px; /* 32px input height / 2 */
  transform: translateY(-50%);
}
</style>
<script>
import $ from "jquery";
import axiosInstance from "@/js/axios";
import { dropdownField } from "@/js/config";
import StatusCode from "../js/StatusCode";
import Customer from "../entities/Customer";
import CustomerUpdate from "../entities/CustomerUpdate";
import Organization from "../entities/Organization";
import Address from "../entities/Address";
import Potential from "../entities/Potential";
import handleClickFilterItem from "../js/checkbox";
import { handleTransferObject, lowerCaseFirstLetter } from "../js/common";
import emitter from "@/js/emitter";
export default {
  name: "TheFormSubmit",
  data() {
    return {
      message: "",
      state: "",
      lastMiddleName: "",
      firstName: "",
      addressName: "",
      createdAccountAt: "",
      dateOfBirth: "",
      countryName: "",
      cityName: "",
      districtName: "",
      wardName: "",
      errorField: [],
      prefixPotentialName: "Potentials/PotentialNames",
      prefixCareerName: "Organizations/Careers",
      prefixDomainName: "Organizations/Domains",
      editForm: this.$store.state.editForm,
      customerEdit: this.$store.state.customerUpdated,
      isSaveAndAdd: false,
      timeOut: 0,
    };
  },
  async mounted() {
    try {
      const checkboxItems = $(".context-input>.icon.icon-checkbox").toArray();
      checkboxItems.forEach((item) => {
        $(item).click(this.checkInput);
      });
      if (this.customerEdit.firstName) {
        this.mountDataEditForm();
      }
      const requiredFields = $(".context-item[required] .input-text").toArray();
      requiredFields.forEach((field) => {
        $(field).blur(this.checkRequiredField);
        $(field).focus(function (e) {
          $(e.target).css("border-color", "#D3D7DE");
          $(e.target).parent().next("span").remove();
        });
      });
      if (!this.editForm) {
        const data = await axiosInstance
          .get("Potentials/MaxCode")
          .then((res) => res.data)
          .catch((error) => console.log(error));
        if (!data.flag) {
          this.$store.commit("setState", "fail");
          this.$store.commit("setMessage", data.userMsg[0]);
          emitter.emit("showToast");
        } else {
          this.$refs["potentialCode"].name = data.data;
        }
      }
    } catch (error) {
      console.log(error);
    }
  },
  computed: {
    handleFullName() {
      return `${this.lastMiddleName} ${this.firstName}`;
    },
    getErrorField() {
      return this.errorField;
    },
  },
  beforeUnmount() {
    $("#sharingUse").removeAttr("checked");
    $("#sharingUse").removeClass("icon-checkbox-checked");
    $("#sharingUse").addClass("icon-checkbox");
  },
  watch: {
    customerEdit(newValue) {
      if (newValue.firstName) {
        this.mountDataEditForm();
      }
    },
    countryName() {
      this.cityName = "";
      this.districtName = "";
      this.wardName = "";
      let addressArr = [];
      this.$refs.cityId.selected = {};
      this.$refs.cityId.setOptions([]);

      this.$refs.districtId.selected = {};
      this.$refs.districtId.setOptions([]);

      this.$refs.wardId.selected = {};
      this.$refs.wardId.setOptions([]);

      addressArr = [
        this.wardName.name,
        this.districtName.name,
        this.cityName.name,
        this.countryName.name,
      ];
      console.log(addressArr);
      this.addressName = addressArr.join(" ").trim();
    },
    cityName() {
      this.districtName = "";
      this.wardName = "";
      let addressArr = [];

      this.$refs.districtId.selected = {};
      this.$refs.districtId.setOptions([]);

      this.$refs.wardId.selected = {};
      this.$refs.wardId.setOptions([]);

      addressArr = [
        this.wardName.name,
        this.districtName.name,
        this.cityName.name,
        this.countryName.name,
      ];
      console.log(addressArr);
      this.addressName = addressArr.join(" ").trim();
    },
    districtName() {
      this.wardName = "";
      let addressArr = [];

      this.$refs.wardId.selected = {};
      this.$refs.wardId.setOptions([]);

      addressArr = [
        this.wardName.name,
        this.districtName.name,
        this.cityName.name,
        this.countryName.name,
      ];
      console.log(addressArr);
      this.addressName = addressArr.join(" ").trim();
    },
    wardName() {
      let addressArr = [];

      addressArr = [
        this.wardName.name,
        this.districtName.name,
        this.cityName.name,
        this.countryName.name,
      ];
      console.log(addressArr);
      this.addressName = addressArr.join(" ").trim();
    },
    firstName() {
      $("#fullName").val(`${this.lastMiddleName} ${this.firstName}`);
    },
    lastMiddleName() {
      $("#fullName").val(`${this.lastMiddleName} ${this.firstName}`);
    },
    // xử lý hiển thị lỗi
    errorField(newValue) {
      let me = this;
      let marginBottom = 0;
      if (newValue?.length > 0) {
        setTimeout(() => {
          me.$refs.errorField.forEach((error, index) => {
            $(error.$el).css("top", `calc(10% + ${marginBottom}px )`);
            error.delay = index * 100;
            console.log(error.delay);
            marginBottom += 50;
            error.showAnimateToast();
          });
        }, 100);
      }
    },
  },
  template: "TheFormSubmit",
  methods: {
    /**
     * Gán dữ liệu vào form khi đc mount trong form sửa
     * Created by LVKIEN 1/9/2022
     */
    mountDataEditForm() {
      try {
        if (this.editForm) {
          this.$refs.vocative.oldSearchFilter = this.customerEdit.vocative
            ? this.customerEdit.vocativeName
            : "Không chọn";
          this.$refs.vocative.selected = {
            id: this.customerEdit.vocative || 0,
            name: this.customerEdit.vocative
              ? this.customerEdit.vocativeName
              : "Không chọn",
          };

          this.$refs.departmentId.oldSearchFilter = this.customerEdit
            .departmentId
            ? this.customerEdit.departmentName
            : "Không chọn";
          this.$refs.departmentId.selected = {
            id: this.customerEdit.departmentId || 0,
            name: this.customerEdit.departmentId
              ? this.customerEdit.departmentName
              : "Không chọn",
          };

          this.$refs.positionId.oldSearchFilter = this.customerEdit.positionId
            ? this.customerEdit.positionName
            : "Không chọn";
          this.$refs.positionId.selected = {
            id: this.customerEdit.positionId || 0,
            name: this.customerEdit.positionId
              ? this.customerEdit.positionName
              : "Không chọn",
          };

          this.$refs.sourceId.oldSearchFilter = this.customerEdit.sourceId
            ? this.customerEdit.sourceName
            : "Không chọn";
          this.$refs.sourceId.selected = {
            id: this.customerEdit.sourceId || 0,
            name: this.customerEdit.sourceId
              ? this.customerEdit.sourceName
              : "Không chọn",
          };

          this.$refs.gender.oldSearchFilter = this.customerEdit.gender
            ? this.customerEdit.genderName
            : "Không chọn";
          this.$refs.gender.selected = {
            id: this.customerEdit.gender || 0,
            name: this.customerEdit.gender
              ? this.customerEdit.genderName
              : "Không chọn",
          };
          $(this.$refs.container)
            .find("#lastMiddleName")
            .val(this.customerEdit.lastMiddleName);
          $(this.$refs.container)
            .find("#firstName")
            .val(this.customerEdit.firstName);
          $(this.$refs.container)
            .find("#fullName")
            .val(this.customerEdit.fullName);

          $(this.$refs.container)
            .find("#customerPhoneNum")
            .val(this.customerEdit.customerPhoneNum);
          $(this.$refs.container)
            .find("#companyPhoneNum")
            .val(this.customerEdit.companyPhoneNum);
          $(this.$refs.container)
            .find("#otherPhoneNum")
            .val(this.customerEdit.otherPhoneNum);
          $(this.$refs.container).find("#zalo").val(this.customerEdit.zalo);
          $(this.$refs.container)
            .find("#organizationName")
            .val(this.customerEdit.organizationName);
          $(this.$refs.container)
            .find("#customerEmail")
            .val(this.customerEdit.customerEmail);
          $(this.$refs.container)
            .find("#companyEmail")
            .val(this.customerEdit.companyEmail);
          $(this.$refs.container)
            .find("#taxCode")
            .val(this.customerEdit.taxCode);
          this.customerEdit.disableCall &&
            $(this.$refs.container).find("#disableCall").trigger("click");
          this.customerEdit.disableMail &&
            $(this.$refs.container).find("#disableMail").trigger("click");
          if (this.customerEdit.potentialName) {
            const potentialArray = this.customerEdit.potentialName
              .split("\n ")
              .map((item) => {
                if (item !== "") {
                  return item.trim();
                }
              })
              .filter((element) => {
                return element !== undefined;
              });
            this.$refs.potentialNames.value =
              this.formatDataComboBox(potentialArray);
          }
          $(this.$refs.container)
            .find("#facebook")
            .val(this.customerEdit.facebook);

          // format dateOfBirth
          if (this.customerEdit?.dateOfBirth) {
            this.$refs.dateOfBirth.setDate(
              this.formatDateTime(this.customerEdit.dateOfBirth)
            );
          }
        }
      } catch (error) {
        console.log(error);
      }
    },

    getSelectedCountry(selected) {
      this.countryName = selected;
    },
    getSelectedCity(selected) {
      this.cityName = selected;
    },
    getSelectedDistrict(selected) {
      this.districtName = selected;
    },
    getSelectedWard(selected) {
      this.wardName = selected;
    },
    getSelectedWard(selected) {
      this.wardName = selected;
    },
    /**
     * TODO: xử lý dữ liệu theo format của combobox
     * @param {*} data
     * !Created by LVKIEN 10/09/2022
     */
    formatDataComboBox(data) {
      return data.map((item) => {
        return { value: item, label: item };
      });
    },
    /**
     * TODO: lấy về dữ liệu cho Combobox
     * @param {*} prefix
     * !Created by LVKIEN 10/09/2022
     */
    async getDataComboBox(prefix) {
      let result = null;
      try {
        const response = await axiosInstance
          .get(prefix)
          .then((res) => res.data)
          .catch((error) => error.response.data);
        if (response.flag) {
          result = this.formatDataComboBox(response.data);
        } else {
          console.log(response.userMsg[0]);
        }
      } catch (error) {
        console.log(error);
        result = null;
      }
      return result;
    },
    /**
     * TODO: lay du lieu datepicker
     * @param {*} date
     * ! Created by LVKIEN 10/09/2022
     */
    handleGetCreatedAccountDate(date) {
      this.createdAccountAt = date;
    },
    /**
     * TODO: lay du lieu datepicker
     * @param {*} date
     * ! Created by LVKIEN 10/09/2022
     */
    handleGetBirthDate(date) {
      this.dateOfBirth = date;
      console.log(date);
    },
    // xử lý checkbox
    checkInput(e) {
      e.preventDefault();
      handleClickFilterItem(e.target);
    },
    // set value cho lastMiddleName
    setLastMiddleName(value) {
      this.lastMiddleName = value;
    },
    // set value cho firstName
    setFirstName(value) {
      this.firstName = value;
    },
    /**
     * TODO: Xử lý hiển thị tỉnh thành phố chỉ khi chọn quốc gia
     * ! Creatd by LVKIEN 10/09/2022
     */
    handleShowCities() {
      this.handleGetAddress(
        "countryId",
        "cityId",
        "Cities",
        "Bạn chưa chọn quốc gia"
      );
    },
    /**
     * TODO: Xử lý hiển thị quận/huyện chỉ khi chọn tỉnh thành phố
     * ! Created by LVKIEN 10/09/2022
     */
    handleShowDistricts() {
      this.handleGetAddress(
        "cityId",
        "districtId",
        "Districts",
        "Bạn chưa chọn tỉnh/thành phố"
      );
    },
    /**
     * TODO: Xử lý hiển thị xã/ phường chỉ khi chọn quận/huyện
     * ! Created by LVKIEN 10/09/2022
     */
    handleShowWards() {
      this.handleGetAddress(
        "districtId",
        "wardId",
        "Wards",
        "Bạn chưa chọn quận/huyện"
      );
    },
    /**
     * TODO: xử lý format dữ liệu của tên tiềm năng, ngành nghề, lĩnh vực khi lưu vào database
     * @param {*} data
     * ! Created by LVKIEN 10/09/2022
     */
    handleDataWhenSave(data) {
      let result = "";
      if (data?.length > 0) {
        data.forEach((item) => {
          result += item.value + " \n ";
        });
      }
      return result === "" ? null : result;
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
        if (
          this.$refs[parentAreaName] &&
          this.$refs[parentAreaName].selected.id &&
          this.$refs[parentAreaName].selected.id !== 0
        ) {
          this.$refs.cityId.showLoading = true;
          this.$refs.districtId.showLoading = true;
          this.$refs.wardId.showLoading = true;
          let areaResponse = await axiosInstance({
            method: "get",
            url: areaNames,
            params: {
              [parentAreaName]: this.$refs[parentAreaName].selected.id,
            },
          })
            .then((res) => res.data)
            .catch((error) => error.response.data);
          this.$refs.cityId.showLoading = false;
          this.$refs.districtId.showLoading = false;
          this.$refs.wardId.showLoading = false;

          if (areaResponse.flag) {
            areaResponse = areaResponse.data;
          } else {
            this.$store.commit("setState", "fail");
            this.$store.commit("setMessage", areaResponse.userMsg[0]);
            emitter.emit("showToast");

            this.$refs.cityId.showNoValue = true;
            this.$refs.districtId.showNoValue = true;
            this.$refs.wardId.showNoValue = true;
          }
          if (areaResponse.length > 0) {
            this.$refs[areaName].setOptions(handleTransferObject(areaResponse));
          }
        } else {
          this.$refs.cityId.optionsShown = false;
          this.$refs.districtId.optionsShown = false;
          this.$refs.wardId.optionsShown = false;

          this.$refs[areaName].oldSearchFilter = false;
          this.$refs[areaName].selected = {};
          this.$store.commit("setState", "fail");
          this.$store.commit("setMessage", actionFail);
          emitter.emit("showToast");
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * TODO: Xử lý hiển thị lỗi cho required field
     * @param {*} e
     * ! Created by LVKIEN 10/09/2022
     */
    checkRequiredField(e) {
      try {
        if (!$(e.target).val().trim()) {
          $(e.target).css("border-color", "red");
          $(e.target)
            .parent()
            .after("<span>Tên không được phép để trống</span>");
        } else {
          $(e.target).css("border-color", "#D3D7DE");
          $(e.target).parent().next("span").remove();
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * TODO: format kiểu datetime từ backend thành format của frontend
     * @param {*} dateString
     * Created by LVKIEN 10/09/2022
     */
    formatDateTime(dateString) {
      const rawResult = dateString.slice(0, 10);
      const dateStringArray = rawResult.split("-");
      return `${dateStringArray[2]}.${dateStringArray[1]}.${dateStringArray[0]}`;
    },
    // format thời gian theo định dạng YYYY/MM/DD theo kiểu dữ liệu của Datetime
    formatDate(dateString) {
      if (dateString) {
        const result = dateString.split(".");
        return `${result[2]}-${result[1]}-${result[0]}`;
      }
      return null;
    },
    /**
     * TODO: đóng form
     * ! Created by LVKIEN 10/09/2022
     */
    closeForm() {
      if (this.$route.name === "Potential") {
        this.$store.commit("setFormState", false);
      }
    },
    /**
     * TODO: ánh xạ dữ liệu vào đúng thuộc tính object
     * @param {*} object
     * ! Created by LVKIEN 10/09/2022
     */
    handleMappingData(object) {
      try {
        const inputArr = $(".inputForm").toArray();
        const inputValueArr = inputArr.map((input) => {
          return { [$(input).attr("id")]: $(input).val() };
        });
        const keysObject = Object.keys(object);
        keysObject.forEach((key) => {
          const lowerCaseKey = lowerCaseFirstLetter(key);
          inputValueArr.forEach((input) => {
            if (input[lowerCaseKey]) {
              object[key] = input[lowerCaseKey];
            }
          });
        });
        if (!this.editForm) {
          const dropdownValueArr = dropdownField.map((dropdown) => {
            if (this.$refs[dropdown]) {
              return { [dropdown]: this.$refs[dropdown].selected.id || null };
            }
          });
          keysObject.forEach((key) => {
            const lowerCaseKey = lowerCaseFirstLetter(key);
            dropdownValueArr.forEach((dropdown) => {
              if (dropdown[lowerCaseKey]) {
                object[key] = dropdown[lowerCaseKey];
              }
            });
          });
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * TODO: lấy dữ liệu form và gọi API
     * ! Created by LVKIEN 10/09/2022
     */
    async saveForm() {
      this.isSaveAndAdd = false;
      this.errorField = [];
      if (this.isSaveAndAdd) {
        //click vào button Lưu và thêm
        await this.sendRequestSaveAndAdd();
      } else {
        //click vào button Lưu
        await this.sendRequestAddOrUpdate();
      }
    },
    /**
     * TODO: click vào button Lưu
     * ! Created by LVKIEN 10/09/2022
     */
    async sendRequestAddOrUpdate() {
      // Xử lý ở form thêm
      if (!this.editForm) {
        await this.saveInsertForm();
        // Xử lý ở form sửa
      } else {
        await this.saveEditForm();
      }
    },
    /**
     * TODO: Xử lý lưu ở form thêm
     * ! Created by LVKIEN 10/09/2022
     */
    async saveInsertForm() {
      try {
        const potentialNames = this.handleDataWhenSave(
          this.$refs.potentialNames.value
        );
        const careerNames = this.handleDataWhenSave(
          this.$refs.careerNames.value
        );
        const domainNames = this.handleDataWhenSave(
          this.$refs.domainNames.value
        );

        // Lấy dữ liệu các ô input trong form
        const customer = new Customer();
        const address = new Address();
        const organization = new Organization();
        const potential = new Potential();

        organization.Career = careerNames;
        organization.Domain = domainNames;
        organization.CreatedAccountAt = this.formatDate(this.createdAccountAt);

        potential.PotentialName = potentialNames;

        customer.Description = $("#description").val();
        customer.SharingUse = !!$("#sharingUse").attr("checked");

        address.AddressName = $("#addressName").val();

        this.handleMappingData(customer);
        this.handleMappingData(address);
        this.handleMappingData(organization);
        this.handleMappingData(potential);

        this.formatData(potential);
        this.formatData(address);
        this.formatData(organization);
        this.formatData(customer);

        console.log(customer, potential, address, organization);
        if (!customer.FirstName || customer.FirstName.trim() === "") {
          this.$store.commit("setState", "fail");
          this.$store.commit("setMessage", "Tên không được phép để trống");
          emitter.emit("showToast");
        } else {
          const customerInsert = { customer, potential, address, organization };
          const resCustomer = await axiosInstance
            .post("Customers", customerInsert)
            .then((res) => res.data)
            .catch((error) => error.response.data);
          if (resCustomer.flag) {
            this.$store.commit("setState", "success");
            this.$store.commit("setMessage", "Thành công");
            emitter.emit("showToast");
            this.$store.commit("setIsInserted", true);
            this.errorField = [];
            this.closeForm();
          } else {
            const potentialErrrorCode = resCustomer.devMsg.find(
              (item) => item === StatusCode.ErrorCode.DuplicatePotentialCode
            );
            this.errorField = resCustomer.userMsg.map((msg) => {
              return { state: "fail", message: msg };
            });
            if (potentialErrrorCode) {
              $("#potentialCode").css("border-color", "red");
              $("#potentialCode")
                .parent()
                .after(
                  `<span>${StatusCode.MessageError.DuplicatePotentialCode}</span>`
                );
            } else {
              $("#potentialCode").css("border-color", "#D3D7DE");
              $("#potentialCode").parent().next("span").remove();
            }
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * TODO: Xử lý khi lưu form sửa
     * !Created by LVKIEN 5/9/2022
     */
    async saveEditForm() {
      try {
        this.errorField = [];
        const potentialNames = this.handleDataWhenSave(
          this.$refs.potentialNames.value
        );
        const customerUpdate = new CustomerUpdate();
        if ($(this.$refs.container).find("#firstName").val().trim() === "") {
          this.errorField = [
            ...this.errorField,
            { firstName: "Tên không được phép để trống" },
          ];
          this.$store.commit("setState", "fail");
          this.$store.commit("setMessage", "Tên không được phép để trống");
          emitter.emit("showToast");
        } else {
          this.handleMappingData(customerUpdate);
          this.getValueDropdown("Vocative", customerUpdate);
          this.getValueDropdown("DepartmentId", customerUpdate);
          this.getValueDropdown("PositionId", customerUpdate);
          this.getValueDropdown("SourceId", customerUpdate);
          this.getValueDropdown("Gender", customerUpdate);

          customerUpdate.PotentialName = potentialNames;
          customerUpdate.DisableCall = !!$(this.$refs.container)
            .find("#disableCall")
            .attr("checked");
          customerUpdate.DisableMail = !!$(this.$refs.container)
            .find("#disableMail")
            .attr("checked");
          customerUpdate.CustomerId =
            this.customerEdit.customerId ||
            this.$store.state.customerUpdated.CustomerId;
          customerUpdate.DateOfBirth = this.formatDate(this.dateOfBirth);
          customerUpdate.OrganizationId = this.customerEdit.organizationId;
          customerUpdate.PotentialId = this.customerEdit.potentialId;
          customerUpdate.ModifiedAt = this.customerEdit.modifiedAt;
          this.formatData(customerUpdate);
          console.log(customerUpdate);
          const resCustomer = await axiosInstance
            .put(`Customers/${customerUpdate.CustomerId}`, customerUpdate)
            .then((res) => res.data)
            .catch((error) => error.response.data);
          if (resCustomer.flag) {
            this.$store.commit("setState", "success");
            this.$store.commit("setMessage", "Thành công");
            emitter.emit("showToast");

            this.$store.commit("setCustomerUpdated", customerUpdate);
            this.$store.commit("setIsUpdated", true);
            if (!this.isSaveAndAdd) {
              this.closeForm();
            }
          } else {
            this.errorField = resCustomer.userMsg.map((msg) => {
              return { state: "fail", message: msg };
            });
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * TODO: click vào button Lưu và thêm
     * ! Created by LVKIEN 10/09/2022
     */
    async sendRequestSaveAndAdd() {
      await this.sendRequestAddOrUpdate();
      if (!this.editForm) {
        this.openForm();
      }
      this.customerEdit = this.$store.state.customerUpdated;
      console.log(this.customerEdit);
    },
    /**
     * TODO: click vào button Lưu và thêm
     * ! Created by LVKIEN 10/09/2022
     */
    async saveAndAddForm() {
      this.isSaveAndAdd = true;
      await this.sendRequestSaveAndAdd();
    },
    /**
     * TODO: Mở form
     * ! Created by LVKIEN 10/09/2022
     */
    openForm() {
      if (this.$route.name === "TiemNang") {
        this.$store.commit("setFormState", true);
      }
    },
    /**
     * Lấy giá trị dropdown gán vào 1 object
     * @param {*} capitalizeValue
     * @param {*} object
     * created by LVKIEN 28/08/2022
     */
    getValueDropdown(capitalizeValue, object) {
      try {
        const toLowerCaseValue = lowerCaseFirstLetter(capitalizeValue);
        // nếu prop selected không có, tức là người dùng không sửa gì, lấy gtri current
        if (Object.keys(this.$refs[toLowerCaseValue].selected).length === 0) {
          object[capitalizeValue] = this.$refs[toLowerCaseValue].selected.id;
        } else {
          // nếu id !== 0, tức là người dùng ko click vào ô "Không chọn"
          if (this.$refs[toLowerCaseValue].selected.id !== 0) {
            object[capitalizeValue] = this.$refs[toLowerCaseValue].selected.id;
            // nếu id === 0, tức là người dùng click vào ô "Không chọn"
          } else {
            object[capitalizeValue] = null;
          }
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * TODO: Kiểm tra xem có lỗi nào không
     * ! Created by LVKIEN 10/09/2022
     */
    checkEmptyError() {
      let result = true;
      for (const key in this.errorField) {
        const itemKey = Object.keys(this.errorField[key]);
        if (this.errorField[key][itemKey] !== "") {
          result = false;
          break;
        }
      }
      return result;
    },
    // check xem liệu object có trống hay ko
    checkEmptyObject(data) {
      let checkEmpty = true;
      if (data) {
        const dataKeys = Object.keys(data);
        dataKeys.forEach((key) => {
          if (data[key] !== null && data[key] !== "") {
            checkEmpty = false;
          }
        });
      }
      return checkEmpty;
    },
    formatData(data) {
      const dataKeys = Object.keys(data);
      dataKeys.forEach((key) => {
        if (data[key] === 0 || data[key] === "") {
          data[key] = null;
        }
      });
    },
  },
};
</script>

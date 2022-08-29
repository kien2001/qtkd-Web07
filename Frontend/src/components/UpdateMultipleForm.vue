<template>
    <div class="box-container" ref="container">
        <div class="box-main">
            <div class="box-header">
                <div class="box-header-title">
                    Cập nhật thông tin
                </div>
                <div class="box-header-btn">
                    <div class="btn-icon tronchanhoi"></div>
                    <div class="btn-icon close" @click="closeUpdateMultipleForm"></div>
                </div>
            </div>
            <div class="box-content">
                <DropDown @selected="getSelectedUpdateFilter" id="field" :showInput="true" placeholder="-Không chọn-"
                    :fetchDataWhenClick="false" ref="listField" />
                <InputForm :isDisabled="isDisabled" id="input" ref="input" v-if="typeUpdateData === 0" />
                <DropDown @selected="getUpdatedOption" :showInput="true" placeholder="-Không chọn-" :id="typeDropdown"
                    :fetchDataWhenClick="true" :name="typeDropdown" ref="dropdown" v-else-if="typeUpdateData === 1" />
                <ComboBox :options="async () => {
                    return await this.getDataComboBox(this.prefixDomainName)
                }" ref="domainNames" v-else />
            </div>
            <div class="box-footer">
                <Button name="Huỷ bỏ" color="#fff" @clickBtn="closeUpdateMultipleForm" colorHover="#D0D8FB" />
                <Button name="Cập nhật" color="#4262F0" colorHover="#2B4EEE" colorText="#FFFFFF" :disabled="isDisabled"
                    @clickBtn="updateMultiple" />
            </div>
        </div>
    </div>
    <PopUp text="Bạn có chắc chắn muốn sửa các tiềm năng này không?" colorBtn="#31B491" colorHoverBtn="#2EA888"
        ref="showConfirm" @handlePopUp="sendRequestUpdateMultiple" />
    <Loading v-if="isLoading" />
    <ToastMessage :message="message" :state="state" ref="toast" />

</template>

<style scoped>
.box-container {
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

.box-main {
    min-width: 600px;
    height: fit-content;
    background-color: #fff;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    border-radius: 4px;
}

.box-header {
    padding: 16px 16px 16px 16px;
    display: flex;
    justify-content: space-between;
}

.box-header-title {
    font-weight: 600;
    font-size: 16px;
    line-height: 24px;
    color: #1F2229;
    letter-spacing: 0;
}

.box-header-btn {
    min-width: 40px;
    display: flex;
    align-items: center;
    gap: 8px;
}

.btn-icon.tronchanhoi {
    width: 16px;
    height: 16px;
    background-size: contain;
}

.btn-icon.close {
    width: 12px;
    height: 12px;
    background-size: contain;
}

.box-content {
    display: flex;
    gap: 10px;
    padding: 16px;
}

.box-content>:first-child {
    flex-basis: 40%;
}

.box-content>:last-child {
    flex-basis: 58%
}

.box-footer {
    height: 56px;
    background-color: #F0F2F4;
    padding: 12px 16px 12px 0;
    border-radius: 0 0 4px 4px;
    display: flex;
    justify-content: flex-end;
    gap: 8px;
}
</style>

<script>
import $ from 'jquery'
import axios from 'axios'
import InputForm from "./InputForm.vue";
import Button from "./Button.vue";
import { fieldOptions, fieldDropdownOptions, rootApi } from '../js/config'
import PopUp from './PopUp.vue';
import ToastMessage from './ToastMessage.vue';
export default {
    name: "UpdateMultipleForm",
    data() {
        return {
            message: '',
            state: '',
            selectedUpdateFilter: {},
            selectedUpdateOption: {},
            isDisabled: true,
            typeUpdateData: 0, // typeUpdateData = 0 =>Input, =1 => Dropdown, =2 => ComboBox
            typeDropdown: '',
            prefixDomainName: "Organizations/Domains",
            isLoading: false
        }
    },
    components: { InputForm, Button, PopUp, ToastMessage },
    methods: {
        closeUpdateMultipleForm() {
            this.$store.commit('setEditMultipleRow', false)
        },
        getSelectedUpdateFilter(selected) {
            if (selected) {
                this.selectedUpdateFilter = selected
            }
        },
        updateMultiple() {
            this.$refs.showConfirm.isShow = true

        },
        /**
         * Gọi api để update nhiều bản ghi 
         * Created by LVKIEn 29/08/2022
         */
        async sendRequestUpdateMultiple() {
            try {
                if (!this.isDisabled) {
                    let resultUpdateObj = null
                    if (this.typeUpdateData === 0) {
                        const inputValue = $(this.$refs.container).find("#input").val()
                        switch (this.selectedUpdateFilter?.name) {

                            case "Họ và tên":
                                resultUpdateObj = { customer: { FullName: inputValue } }
                                break;

                            case "ĐT di động":
                                resultUpdateObj = { customer: { CustomerPhoneNum: inputValue } }
                                break;

                            case "ĐT cơ quan":
                                resultUpdateObj = { customer: { CompanyPhoneNum: inputValue } }
                                break;

                            case "Email cá nhân":
                                resultUpdateObj = { customer: { CustomerEmail: inputValue } }
                                break;

                            case "Email cơ quan":
                                resultUpdateObj = { customer: { CompanyEmail: inputValue } }
                                break;

                            case "Địa chỉ":
                                resultUpdateObj = { address: { AddressName: inputValue } }
                                break;

                            case "Mô tả":
                                resultUpdateObj = { customer: { Description: inputValue } }
                                break;
                            case "Bố cục":
                                resultUpdateObj = { customer: { Layout: inputValue } }
                                break;
                            case "Chủ sở hữu":
                                resultUpdateObj = { customer: { Owner: inputValue } }
                                break;
                            case "Facebook":
                                resultUpdateObj = { customer: { Facebook: inputValue } }
                                break;
                            case "Dùng chung":
                                resultUpdateObj = { customer: { SharingUse: inputValue } }
                                break;
                            case "Tổ chức":
                                resultUpdateObj = { organization: { OrganizationName: inputValue } }
                                break;
                            default:
                                break;
                        }
                    } else if (this.typeUpdateData === 1) {
                        switch (this.selectedUpdateFilter?.name) {
                            case "Xưng hô":
                                resultUpdateObj = { customer: { Vocative: this.selectedUpdateOption?.id } }
                                break;

                            case "Chức danh":
                                resultUpdateObj = { customer: { PositionId: this.selectedUpdateOption?.id } }
                                break;

                            case "Tỉnh/Thành phố":
                                resultUpdateObj = { address: { CityId: this.selectedUpdateOption?.id } }
                                break;

                            case "Quận/Huyện":
                                resultUpdateObj = { address: { DistrictId: this.selectedUpdateOption?.id } }
                                break;

                            case "Phường/Xã":
                                resultUpdateObj = { address: { WardId: this.selectedUpdateOption?.id } }
                                break;

                            case "Nguồn gốc":
                                resultUpdateObj = { customer: { SourceId: this.selectedUpdateOption?.id } }
                                break;

                            case "Loại hình":
                                resultUpdateObj = { organization: { TypeId: this.selectedUpdateOption?.id } }
                                break;
                            case "Doanh thu":
                                resultUpdateObj = { organization: { RevenueId: this.selectedUpdateOption?.id } }
                                break;
                            default:
                                break;
                        }
                    } else {
                        resultUpdateObj = { organization: { Domain: this.handleDataWhenSave(this.$refs.domainNames.value) } }
                    }

                    //xử lý gọi api update dữ liệu
                    const nameTable = Object.keys(resultUpdateObj)[0];
                    const listId = this.getListIdChecked[`listId${this.capitalizeFirstLetter(nameTable)}`].filter(id => {
                        return id !== null
                    })
                    const dataUpdate = {
                        ListId: listId,
                        FieldUpdateName: Object.keys(resultUpdateObj[nameTable])[0],
                        FieldUpdateValue: resultUpdateObj[nameTable][Object.keys(resultUpdateObj[nameTable])[0]].toString()
                    }
                    console.log(dataUpdate);
                    // xử lý lấy tên bảng
                    const capitalizeTable = this.capitalizeFirstLetter(nameTable)
                    console.log(capitalizeTable);
                    const rootUrlTable = capitalizeTable !== "Address" ? `${capitalizeTable}s` : `${capitalizeTable}es`
                    this.isLoading = true;
                    const response = await axios.post(`${rootApi}${rootUrlTable}/MultipleRows`, dataUpdate).then(res => res.data).catch(error => error.response.data)
                    this.isLoading = false;
                    if (response.flag) {
                        this.state = "success"
                        this.message = "Thành công"
                        this.$refs.toast.isShow = true
                        this.$store.commit("setIsUpdated", true)
                    } else {
                        this.state = "fail"
                        this.message = response.userMsg
                        this.$refs.toast.isShow = true
                    }
                }
            }catch(error){
                this.state = "fail"
                this.message = error
                this.$refs.toast.isShow = true
            }
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
        // xử lý dữ liệu theo format của combobox
        formatDataComboBox(data) {
            return data.map(item => {
                return { value: item, label: item }
            })
        },
        getUpdatedOption(selected) {
            console.log(selected);
            if (selected) {
                this.selectedUpdateOption = {}
                // const formatKey = this.typeDropdown.split("/")[0]
                this.selectedUpdateOption = selected
            }
        },
        // xử lý format dữ liệu của tên tiềm năng, ngành nghề, lĩnh vực khi lưu vào database
        handleDataWhenSave(data) {
            let result = '';
            if (data?.length > 0) {
                data.forEach(item => {
                    result += item.value.trim() + "\n"
                })
            }
            return result === '' ? null : result
        },
        // xử lý format họ và tên
        handleGetName(name) {
            const nameArr = name.split(" ");
            if (nameArr.length === 1) {
                return { FirstName: nameArr[0] }

            } else if (nameArr.length === 2) {
                return {
                    LastMiddleName: nameArr[0],
                    FirstName: nameArr[1]
                }
            } else {
                let lastMiddleName = '';
                for (let i = 0; i < nameArr.length - 1; i++) {
                    lastMiddleName += nameArr[i] + ' '
                }
                return {
                    LastMiddleName: lastMiddleName.trim(),
                    FirstName: nameArr[nameArr.length - 1]
                }
            }
        },
        capitalizeFirstLetter(string) {
            return string.charAt(0).toUpperCase() + string.slice(1);
        },
    },
    mounted() {
        fieldOptions.sort((a, b) => a.localeCompare(b))
        let count = 0;
        const options = fieldOptions.map(option => {
            count++;
            return { id: count, name: option }
        })
        this.$refs.listField.setOptions(options)
    },
    computed: {
        getListIdChecked() {
            const listCheckedCustomer = this.$store.state.listCheckedCustomer.map(customer => {
                return { customerId: customer.customerId, addressId: customer.addressId, organizationId: customer.organizationId }
            })
            const listIdCustomer = listCheckedCustomer.map(id => id.customerId)
            const listIdAddress = listCheckedCustomer.map(id => id.addressId)
            const listIdOrganization = listCheckedCustomer.map(id => id.organizationId)
            return { listIdCustomer, listIdAddress, listIdOrganization }
        }
    },
    watch: {
        selectedUpdateFilter(newValue, oldValue) {
            if (newValue) {
                this.isDisabled = false
                if (newValue.name) {
                    if (fieldDropdownOptions.includes(newValue.name)) {
                        this.typeUpdateData = 1
                        switch (newValue.name) {
                            case "Xưng hô":
                                this.typeDropdown = "Vocatives"
                                break;
                            case "Chức danh":
                                this.typeDropdown = "Positions"
                                break;
                            case "Tỉnh/Thành phố":
                                this.typeDropdown = "Cities/AllCities"
                                break;
                            case "Quận/Huyện":
                                this.typeDropdown = "Districts/AllDistricts"
                                break;
                            case "Phường/Xã":
                                this.typeDropdown = "Wards/AllWards"
                                break;
                            case "Nguồn gốc":
                                this.typeDropdown = "Sources"
                                break;
                            case "Loại hình":
                                this.typeDropdown = "Types"
                                break;
                            case "Doanh thu":
                                this.typeDropdown = "Revenues"
                                break;
                            default:
                                break;
                        }
                    } else if (newValue.name === "Lĩnh vực") {
                        this.typeUpdateData = 2
                    } else {
                        this.typeUpdateData = 0
                    }
                }

            }
            // reset value khi thay đổi selectedOption
            if (this.$refs.dropdown) {
                this.$refs.dropdown.setOptions([])
                this.$refs.dropdown.oldSearchFilter = ''
                this.$refs.dropdown.selected = {}
            }
            if (this.$refs.domainNames) {
                this.$refs.domainNames.value = []
            }
            if (this.$refs.input) {
                this.$refs.input.name = ""
            }

        }
    }
}
</script>
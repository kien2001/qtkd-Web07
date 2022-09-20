<template>
  <Datepicker
    v-model="date"
    :format="format"
    :clearable="false"
    :enableTimePicker="false"
    placeholder="DD/MM/YYYY"
    modelType="dd.MM.yyyy"
    @update:modelValue="setDate"
  >
    <template #calendar-icon>
      <img class="slot-icon" src="../assets/img/3dot.svg" />
    </template>
  </Datepicker>
</template>
<script>
import Datepicker from '@vuepic/vue-datepicker'
import '@vuepic/vue-datepicker/dist/main.css'
import StatusCode from '@/config/StatusCode';
import { ref } from "vue";
import emitter from "@/js/emitter";

export default {
    name: "BaseDatePicker",
    /**
     * TODO: Khởi tạo format ngày tháng năm, hàm set giá trị và value của date
     * !Created by LVKIEN 10/9/2022
     */
    setup() {
        const date = ref(new Date());
        const format = (date) => {
            let day = date.getDate();
            if (day < 10) {
                day = `0${day}`;
            }
            let month = date.getMonth() + 1;
            if (month < 10) {
                month = `0${month}`;
            }
            const year = date.getFullYear();
            return `${day}/${month}/${year}`;
        };
        const setDate = (value) => {
            date.value = value;
        };
        return {
            date,
            format,
            setDate,
        };
    },
    watch: {
        /**
         * TODO: Check xem liệu giá trị người dùng nhập vào có lớn hơn hiện tại hay ko
         * @param {*} newValue
         * @param {*} oldValue
         * !Created by LVKIEN 10/9/2022
         */
        date(newValue, oldValue) {
            if (newValue) {
                const dateArr = newValue.split(".");
                const chosenDate = new Date(dateArr[2], dateArr[1] - 1, dateArr[0]);
                const currentDate = new Date();
                if (chosenDate.getTime() > currentDate.getTime()) {
                    this.$store.commit("setState", "fail");
                    this.$store.commit("setMessage", StatusCode.MessageError.NotValidDateError);
                    emitter.emit("showToast");
                    if (oldValue.toString().length > 10) {
                        let day = oldValue.getDate();
                        if (day < 10) {
                            day = `0${day}`;
                        }
                        let month = oldValue.getMonth() + 1;
                        if (month < 10) {
                            month = `0${month}`;
                        }
                        const year = oldValue.getFullYear();
                        this.setDate(`${day}.${month}.${year}`);
                    }
                    else {
                        this.setDate(oldValue);
                    }
                }
            }
        },
    },
    updated() {
        this.$emit("getDate", this.date);
    },
    components: { Datepicker }
};
</script>
<style>
.slot-icon {
  height: 20px;
  width: 20px;
}

.dp__pointer.dp__input_readonly.dp__input.dp__input_icon_pad.dp__input_reg {
  font-size: 13px;
  height: 32px;
}
.dp__input_icons {
  padding: 0 !important;
}
.dp__input_icon {
  right: 8px;
  left: auto !important;
}

.dp__input_icon_pad {
  padding-left: 0 !important;
}

.dp__input {
  padding: 6px 8px 6px 16px !important;
}

.dp__input:hover {
  border-color: #7189f4 !important;
}

.dp__input:active,
.dp__input:target,
.dp__input:visited,
.dp__input:focus
 {
  border-color: #4262f0 !important;
}
</style>

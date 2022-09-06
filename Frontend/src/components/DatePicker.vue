<template>
    <Datepicker v-model="date" :format="format" :clearable="false" :enableTimePicker="false" placeholder="DD/MM/YYYY"
        modelType="dd.MM.yyyy" @update:modelValue="setDate">
        <template #calendar-icon>
            <img class="slot-icon" src="../assets/img/3dot.svg" />
        </template>
    </Datepicker>
</template>
<script>
import { ref } from 'vue'
export default {
    name: 'DatePicker',
    data() {
        return {
            dates: ''
        }
    },
    setup() {
        const date = ref(new Date())
        // In case of a range picker, you'll receive [Date, Date]
        const format = (date) => {
            const day = date.getDate()
            const month = date.getMonth() + 1
            const year = date.getFullYear()
            return `${day}-${month}-${year}`
        }
        const setDate = (value) => {
            date.value = value
        }
        return {
            date,
            format,
            setDate
        }
    },
    updated() {
        this.$emit('getDate', this.date)
    },
    methods: {
        setDate(value) {
            this.date = value
        }
    }
}
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

.dp__input_icon {
    right: 0;
    left: auto !important;
    ;
}

.dp__input_icon_pad {
    padding-left: 0 !important;
}

.dp__input {
    padding: 6px 10px !important;
}

.dp__input:hover {
    border-color: #7189F4 !important;
}

.dp__input:active,
.dp__input:target,
.dp__input:visited {
    border-color: #4262F0 !important;
}
</style>

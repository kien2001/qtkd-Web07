<template>
  <Multiselect
    mode="tags"
    :isTag="true"
    :close-on-select="false"
    v-model="value"
    :create-option="true"
    placeholder="-Không chọn-"
    noResultsText="Không còn dữ liệu"
    class="multiselect"
    :hideSelected="false"
    :object="true"
    :resolve-on-load="false"
    :infinite="true"
    :limit="10"
    :delay="0"
    @open="
      (select$) => {
        if (select$.noOptions) {
          select$.resolveOptions();
        }
      }
    "
    ref="multiselect"
  >
    <template v-slot:spinner="{}">
      <div class="spinner-custom multiselect-dropdown">
        <img class="spinner-icon" src="../assets/img/loadding-icon.svg" />
      </div>
    </template>
  </Multiselect>
</template>
<script>
import Multiselect from "@vueform/multiselect";
export default {
  name: "BaseComboBox",
  components: { Multiselect },
  data() {
    return {
      value: null,
    };
  },
};
</script>
<style src="@vueform/multiselect/themes/default.css">
</style>

<style>
.multiselect {
  --ms-option-bg-selected-pointed: #f0f2f4;
  --ms-option-color-selected-pointed: #4262f0;
  --ms-option-bg-selected: #fff;
  --ms-option-color-selected: #4262f0;
  --ms-font-size: 13px;
  --ms-tag-bg: #e2e4e9;
  --ms-tag-color: #1f2229;
  --ms-tag-font-size: 13px;
  --ms-option-font-size: 13px;
  --ms-tag-font-weight: 500;
  --ms-option-bg-pointed: #f0f2f4;
  min-height: 32px !important;
  transition: border-color 0.2s cubic-bezier(0.645, 0.045, 0.355, 1);
}

.multiselect:hover {
  border-color: #7189f4;
}

.multiselect-caret {
  display: none;
}

.multiselect-clear {
  display: none;
}
.multiselect-tag-remove{
  border-radius: 50%;
}
.multiselect-tag-remove-icon {
  background-color: #7c869c;
}
.multiselect.is-active {
  box-shadow: none;
  border-color: #7189f4;
}

.multiselect-dropdown {
  padding-top: 8px;
  padding-bottom: 8px;
  overflow-y: auto;
  min-height: 100px;
}

.multiselect-option.is-selected {
  background-image: url("../assets/img/blue-active/tich.svg");
  background-repeat: no-repeat;
  background-size: 16px 16px;
  background-position-y: center;
  background-position-x: calc(100% - 10px);
}

.multiselect-option.is-selected.is-pointed {
  background-image: url("../assets/img/blue-active/tich.svg");
  background-repeat: no-repeat;
  background-size: 16px 16px;
  background-position-y: center;
  background-position-x: calc(100% - 10px);
}

.spinner-icon {
  width: 100px;
  height: 100px;
}

.spinner-icon {
  margin: 0 auto;
}
.multiselect-tags-search-wrapper {
  display: none !important;
}
.multiselect-tags {
  max-height: 52px;
  overflow-y: auto;
  align-items: flex-start !important;
  padding-left: 14px !important;
}
.multiselect-tag {
  height: 22px;
}
</style>
<template>
    <div class="tooltip">
        <div class="tooltip-icon"></div>
        <span class="tooltip-text">{{tooltipText}}</span>
    </div>
</template>

<script>
import $ from 'jquery'
export default {
  name: 'Tooltip',
  props: {
    tooltipText: {
      type: String,
      required: true
    }
  },
  mounted () {
    const tooltipArr = $('.tooltip-text').toArray()
    tooltipArr.forEach(item => {
      const widthTooltip = -($(item).width() + 2 * 16 - 15) / 2
      $(item).css('margin-left', `${widthTooltip}px`)
    })
  }
}
</script>

<style scoped>

.tooltip {
    position: relative;
    display: inline-block;
    margin-left: 8px;
}

.tooltip-icon{
    width: 16px;
    height: 16px;
    background-image: url("../assets/img/tronchamthan.svg");
    background-repeat: no-repeat;
    background-size: 16px;
    background-position: center;
}

.tooltip .tooltip-text {
    visibility: hidden;
    max-height: 32px;
    min-width: max-content;
    background-color: #424857;
    color: #FFFFFF;
    text-align: left;
    font-weight: 400;
    font-size: 13px;
    line-height: 16px;
    letter-spacing: 0;
    border-radius: 4px;
    padding: 8px 16px;
    position: absolute;
    z-index: 1;
    bottom: 150%;
    margin-left: -50%;
    opacity: 0;
        transition: opacity 1s;
}

.tooltip .tooltip-text::after {
    content: "";
    position: absolute;
    top: 100%;
    left: 50%;
    margin-left: -5px;
    border-width: 5px;
    border-style: solid;
    border-color: #424857 transparent transparent transparent;
}

.tooltip:hover .tooltip-text {
    visibility: visible;
    opacity: 1;
}
</style>

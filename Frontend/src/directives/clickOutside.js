const clickOutside = {
  beforeMount: (el, binding) => {
    el.clickOutsideEvent = function (event) {
      // here I check that click was outside the el and his children
      if (!(el === event.target || el.contains(event.target))) {
        // and if it did, call method provided in attribute value
        binding.value(event, el);
      }
    };
    document.getElementById("app").addEventListener('click', el.clickOutsideEvent, true)
  },
  unmounted: el => {
    document.getElementById("app").removeEventListener('click', el.clickOutsideEvent)
  },
};
export default clickOutside;
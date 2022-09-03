import $ from 'jquery'
// A method that is called when the user clicks on a filter option checkbox item in the list.
const handleClickFilterItem = (checkedItem) => {
  let listCheckedInput = []
  // if checked and click again, remove checked
  if ($(checkedItem).attr('checked')) {
    if ($(checkedItem).children('.icon').length > 0) {
      $(checkedItem).children('.icon').removeClass('icon-checkbox-checked')
      $(checkedItem).children('.icon').addClass('icon-checkbox')
      $(checkedItem).removeAttr('checked')
    } else {
      $(checkedItem).removeClass('icon-checkbox-checked')
      $(checkedItem).addClass('icon-checkbox')
      $(checkedItem).removeAttr('checked')
    }
    listCheckedInput = $('div[checked]').toArray()
    return
  }
  // add checked
  if ($(checkedItem).length > 0) {
    handleCheckInput(checkedItem, listCheckedInput)
  }
}
// This method is called when the user clicks on a filter option checkbox checked item in the list.
const handleCheckInput = (element, listCheckedInput) => {
  $(element).attr('checked', '')
  listCheckedInput = $('div[checked]').toArray()
  listCheckedInput?.forEach(item => {
    if ($(item).children('.icon').length > 0) {
      $(item).children('.icon').removeClass('icon-checkbox')
      $(item).children('.icon').addClass('icon-checkbox-checked')
    } else {
      $(item).removeClass('icon-checkbox')
      $(item).addClass('icon-checkbox-checked')
    }
  })
}

export default handleClickFilterItem

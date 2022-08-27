import $ from 'jquery'
const handleToggleSideBar = (otherSideBar, e, widthToggle) => {
  const calculateWidth = $(e.target).attr('toggle') ? widthToggle : '0px'
  const widthSideBar = $(`.${otherSideBar}`).css('width')
  otherSideBar === 'sidebar-left' ? $('.main').css('grid-template-columns', `${widthSideBar} auto ${calculateWidth}`) : $('.main').css('grid-template-columns', `${calculateWidth} auto ${widthSideBar}`)
  if (!$(e.target).attr('toggle')) {
    $(e.target).attr('toggle', 'toggle')
  } else if ($(e.target).attr('toggle')) {
    // $("#app").css("grid-template-columns", `${widthSideBar} auto ${calculateWidth}`)
    $(e.target).removeAttr('toggle')
  }
}
export { handleToggleSideBar }

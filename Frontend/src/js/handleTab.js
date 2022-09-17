// export const handlePressTab= ()=>{
//     let btns = document.querySelectorAll(".context-input");
//     btns.forEach((btn, index) => {
//         // set first button tabindex to 0
//         // and set every other button tabindex to -1
//         if (index == btns.length - 1) {
//             btn.setAttribute("tabindex", 0);
//         } else {
//             btn.setAttribute("tabindex", -1);
//         }

//         // add an event listener when tab key is pressed
//         btn.addEventListener("keydown", (e) => {
//             if (e.keyCode == 9) {
//                 // prevent the default behaviour
//                 e.preventDefault();
//                 // set current button tabindex to 0
//                 btn.setAttribute("tabindex", -1);
//                 // if not last button keep setting tabindex to 0
//                 console.log(btn.parentElement);
//                 if (btn.parentElement.previousElementSibling !== null) {
                    
//                     let nextEl = btn.parentElement.previousElementSibling;
//                     nextEl.children[1].setAttribute("tabindex", 0);
//                     nextEl.children[1].focus();
//                 } else {
//                     // when we get to last element set first element to tabindex 0
//                     // and call focus method on it.
//                     // note the .lastElementChild the last element becomes our first 
//                     // that's because of the direction we changed

//                     let firstEl = document.querySelector(".input-container:last-child")
//                     console.log(firstEl);
//                     firstEl.children[0].setAttribute("tabindex", 0);
//                     firstEl.children[0].focus();
//                 }
//             }
//         });
//     });
// }
 


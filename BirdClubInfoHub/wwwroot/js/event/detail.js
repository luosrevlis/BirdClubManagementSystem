function initRegister() {
    document.querySelector(".register-event-container").style.display = "flex";
}

function cancelRegister() {
    document.querySelector(".register-event-container").style.display = "none";
}

let btnList = document.querySelectorAll(".navtab-btn");
let tabContent = document.querySelectorAll(".tab-content-item")
btnList.forEach((btn) => {
    btn.addEventListener("click", (event) => {
        // duyệt lại danh sách các btn và xóa actived cho từng btn
        btnList.forEach((_btn) => {
            _btn.classList.remove("active");
        });
        // thêm actived cho thằng vừa nhận
        event.target.classList.add("active");
        // duyệt danh sách các tabContent và xóa actived
        tabContent.forEach((content) => {
            content.classList.remove("active")
        });
        // thêm actived cho tab-content có data-id trùng với id của btn vừa nhận
        let id = event.target.id; //lấy ra id của nút vừa nhấn
        document
            .querySelector(`.tab-content-item[data-id='${id}']`)
            .classList.add("active")
    });
});

// edit for select box in table ranking
const dropdowns = document.querySelectorAll(".select-menu");

dropdowns.forEach(dropdown => {
    // lấy các thành phần từ dropdown
    const select = dropdown.querySelector('.select');
    const caret = dropdown.querySelector('.caret');
    const menu = dropdown.querySelector('.menu');
    const options = dropdown.querySelectorAll('.menu li');
    const selected = dropdown.querySelector('.selected');

    // thêm click event
    select.addEventListener('click', ()=>{
        // Add the clicked select styles to the select element
        select.classList.toggle('select-clicked');
        //Add rotate style to caret
        caret.classList.toggle('caret-rotate');
        // Add the open styles to menu element
        menu.classList.toggle('menu-open');

    });
    options.forEach(option =>{
        option.addEventListener('click', () => {
            selected.innerText = option.innerText;
            // Add the clicked select styles to to select element
            select.classList.remove('select-clicked');
            // Add the rotate styles to the caret element
            caret.classList.remove('caret-rotate');
            // Add the open styles to the menu element
            menu.classList.remove('menu-open');
            // Remove active class from all option elements
            options.forEach(option => {
                option.classList.remove('active');
            });
            option.classList.add('active')

        });
    });
});
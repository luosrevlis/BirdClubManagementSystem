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
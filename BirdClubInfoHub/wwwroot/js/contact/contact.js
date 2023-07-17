function send() {
    alert("Information sent successfully")

}
function prevent() {
    $('#contact-form').submit(function (event) {
        event.preventDefault();
    }
    )
};



function submitForm() {
     // Ngăn chuyển trang mặc định

    // Xử lý dữ liệu form tại đây, ví dụ: gửi dữ liệu bằng AJAX
    // ...

    // Sau khi xử lý thành công, chờ 3 giây và chuyển trang
    window.setTimeout(function () {
        window.location.href = "contact.html"; // Thay đổi thành đường dẫn trang mới
    }, 2000); // 3000 milliseconds = 3 seconds
}

function preventFormSubmit() {
    var forms = document.querySelectorAll('form');
    for (var i = 0; i < forms.length; i++) {
        forms[i].addEventListener('submit', function (event) {
            event.preventDefault();
        })
    }
}
// window.addEventListener('load', preventFormSubmit);
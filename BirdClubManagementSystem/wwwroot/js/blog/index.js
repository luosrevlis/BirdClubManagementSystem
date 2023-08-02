function initDelete(blogId) {
    Array.from(document.getElementsByClassName('delete-container')).forEach(e => {
        e.style.display = "flex";
        Array.from(e.getElementsByClassName('blog-id')).forEach(b => {
            b.value = blogId;
        })
    })
}

function cancelDelete() {
    Array.from(document.getElementsByClassName('delete-container')).forEach(e => {
        e.style.display = "none";
        Array.from(e.getElementsByClassName('blog-id')).forEach(b => {
            b.value = 0;
        })
    })
}

function initAccept(blogId) {
    Array.from(document.getElementsByClassName('accept-container')).forEach(e => {
        e.style.display = "flex";
        Array.from(e.getElementsByClassName('blog-id')).forEach(b => {
            b.value = blogId;
        })
    })
}

function cancelAccept() {
    Array.from(document.getElementsByClassName('accept-container')).forEach(e => {
        e.style.display = "none";
        Array.from(e.getElementsByClassName('blog-id')).forEach(b => {
            b.value = 0;
        })
    })
}

function initReject(blogId) {
    Array.from(document.getElementsByClassName('reject-container')).forEach(e => {
        e.style.display = "flex";
        Array.from(e.getElementsByClassName('blog-id')).forEach(b => {
            b.value = blogId;
        })
    })
}

function cancelReject() {
    Array.from(document.getElementsByClassName('reject-container')).forEach(e => {
        e.style.display = "none";
        Array.from(e.getElementsByClassName('blog-id')).forEach(b => {
            b.value = 0;
        })
    })
}

var statusList = document.querySelector(".status-list");
var selected = statusList.getAttribute("data-selected");
var options = statusList.getElementsByTagName("option");
for (option of options) {
    if (option.getAttribute("value") == selected) {
        option.setAttribute("selected", "true");
    }
}
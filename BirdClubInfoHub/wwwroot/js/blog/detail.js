function initEdit(commentId) {
    Array.from(document.getElementsByClassName("edit-comment-container"))
        .filter(e => e.getAttribute("data-comment-edit-id") == commentId)
        .forEach(e => e.style.display = "flex");
    Array.from(document.getElementsByClassName("comment-content-container"))
        .filter(e => e.getAttribute("data-comment-id") == commentId)
        .forEach(e => e.style.display = "none");
    Array.from(document.getElementsByClassName("update-comment-container")).filter(e => e.parentElement.getAttribute("data-comment-id") != commentId).forEach(e => e.style.display = "none");
}

function cancelEdit(commentId) {
    Array.from(document.getElementsByClassName("edit-comment-container"))
        .filter(e => e.getAttribute("data-comment-edit-id") == commentId)
        .forEach(e => e.style.display = "none");
    Array.from(document.getElementsByClassName("comment-content-container"))
        .forEach((e) => e.style.display = "flex");
    Array.from(document.getElementsByClassName("update-comment-container")).filter(e => e.parentElement.getAttribute("data-comment-id") != commentId).forEach(e => e.style.display = "flex");
}

function initDelete(commentId) {
    const comment = Array.from(document.getElementsByClassName("delete-comment-container"))
        .filter(e => e.getAttribute("data-comment-delete-id") == commentId)
        .forEach(e => e.style.display = "flex");
}

function cancelDelete(commentId) {
    const comment = Array.from(document.getElementsByClassName("delete-comment-container"))
        .filter(e => e.getAttribute("data-comment-delete-id") == commentId)
        .forEach(e => e.style.display = "none");
}

// remember the location
const path = "/Blogs/Details/"
window.addEventListener("DOMContentLoaded", (e) => {
    if (window.location.pathname.includes(path) == true) {
        const scrollPosition = sessionStorage.getItem("prevScrollPosition");
        if (scrollPosition != null) {
            window.scrollTo(0, scrollPosition);
            sessionStorage.removeItem("prevScrollPosition");
        }
    }
});

// scroll to prev location
window.addEventListener("beforeunload", (e) => {
    if (window.location.pathname.includes(path)) {
        sessionStorage.setItem("prevScrollPosition", window.scrollY);
    }
});
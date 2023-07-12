function initDelete(blogId) {
    document.querySelector(".delete-container").style.display = "flex";
    document.querySelector("#blog-id").value = blogId;
}

function cancelDelete() {
    document.querySelector(".delete-container").style.display = "none";
    document.querySelector("#blog-id").value = "";
}
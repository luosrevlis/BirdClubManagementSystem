function initDelete(blogId) {
    document.querySelector(".delete-container").style.display = "flex";
    document.querySelector("#blog-id").value = blogId;
}

function cancelDelete() {
    document.querySelector(".delete-container").style.display = "none";
    document.querySelector("#blog-id").value = "";
}

var statusList = document.querySelector(".status-list");
var selected = statusList.getAttribute("data-selected");
var options = statusList.getElementsByTagName("option");
for (option of options) {
    if (option.getAttribute("value") == selected) {
        option.setAttribute("selected", "true");
    }
}
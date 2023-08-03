function initDelete(userId) {
    document.querySelector(".delete-container").style.display = "flex";
    document.querySelector("#user-id").value = userId;
}

function cancelDelete() {
    document.querySelector(".delete-container").style.display = "none";
    document.querySelector("#user-id").value = "";
}

var roleList = document.querySelector(".role-list");
var selected = roleList.getAttribute("data-selected");
var options = roleList.getElementsByTagName("option");
for (option of options) {
    if (option.getAttribute("value") == selected) {
        option.setAttribute("selected", "true");
    }
}
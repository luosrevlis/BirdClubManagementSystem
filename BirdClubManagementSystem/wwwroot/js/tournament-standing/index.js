function initDelete(tsId) {
    document.querySelector(".delete-container").style.display = "flex";
    document.querySelector("#ts-id").value = tsId;
}

function cancelDelete() {
    document.querySelector(".delete-container").style.display = "none";
    document.querySelector("#ts-id").value = "";
}

var placementList = document.querySelector(".placement-list");
var selected = placementList.getAttribute("data-selected");
var options = placementList.getElementsByTagName("option");
for (option of options) {
    if (option.getAttribute("value") == selected) {
        option.setAttribute("selected", "true");
    }
}
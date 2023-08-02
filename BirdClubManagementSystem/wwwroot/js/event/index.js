function updateController() {
    var controller = document.querySelector(".event-type-list").value;
    var searchForm = document.getElementById("search-form")
    searchForm.setAttribute("action", "/" + controller);
}

var statusList = document.querySelector(".status-list");
var selected = statusList.getAttribute("data-selected");
var options = statusList.getElementsByTagName("option");
for (option of options) {
    if (option.getAttribute("value") == selected) {
        option.setAttribute("selected", "true");
    }
}
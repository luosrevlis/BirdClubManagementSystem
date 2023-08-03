var placementList = document.querySelector(".placement-list");
var selected = placementList.getAttribute("data-selected");
var options = placementList.getElementsByTagName("option");
for (option of options) {
    if (option.getAttribute("value") == selected) {
        option.setAttribute("selected", "true");
    }
}
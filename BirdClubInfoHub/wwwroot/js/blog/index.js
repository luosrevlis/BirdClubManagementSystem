var categoryList = document.querySelector(".category-list");
var selected = categoryList.getAttribute("data-selected");
var options = categoryList.getElementsByTagName("option");
for (option of options) {
    if (option.getAttribute("value") == selected) {
        option.setAttribute("selected", "true");
    }
}
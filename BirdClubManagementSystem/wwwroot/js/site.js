window.onload = (event) => {
    init();
}

// add functions that you want to run after the page is FULLY loaded here
function init() {
    setNavBarHover();
}

// add multiple event listeners to one element

function addEventListeners(elements, events, func, funcArgs) {
    var functionCompiled = function () {
        func.apply(this, funcArgs);
    }

    elements.forEach(element => {
        events.forEach(event => {
            element.addEventListener(event, functionCompiled);
        });
    });
}

function setNavBarHover() {
    const navToggle = document.getElementsByClassName("nav-toggle").item(0);
    const navBar = document.getElementsByClassName("nav-bar-container").item(0);
    const navHeight = getComputedStyle(document.documentElement).getPropertyValue("--navHeight");
    const navHeightNum = parseFloat(navHeight.replace(/[^\d.]/g, ""));
    const navBarHeight = getComputedStyle(document.documentElement).getPropertyValue("--navBarHeight");
    const navBarHeightNum = parseFloat(navBarHeight.replace(/[^\d.]/g, ""));

    addEventListeners(
        [navToggle, navBar],
        ["pointerenter"],
        () => {
            navBar.style.cssText += "top: " + navHeightNum + "rem";
            navToggle.style.top = navHeightNum + navBarHeightNum + "rem";
        },
        [navBar]);

    addEventListeners(
        [navToggle, navBar],
        ["pointerleave"],
        () => {
            navBar.style.cssText += "top: " + (navHeightNum - navBarHeightNum) + "rem";
            navToggle.style.top = navHeightNum + "rem";
        },
        [navBar]);
}
// js for dropdown
let subMenu = document.getElementById("subMenu");
function toggleMenu(){
    subMenu.classList.toggle("open-menu")
}

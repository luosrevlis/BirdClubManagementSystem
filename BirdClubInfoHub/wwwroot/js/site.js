window.onload = (event) => {
    init();
}

// add functions that you want to run after the page is FULLY loaded here
function init() {
    setUpNavBar();
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

function setUpNavBar() {
    window.addEventListener("resize", e => {
        Array.from(document.getElementsByClassName("nav-bar-container")).forEach(nav => {
            if (window.innerWidth <= 992) {
                nav.style.display = "none";
            } else {
                nav.style.display = "flex";
            }
        })
    })
    Array.from(document.getElementsByClassName("nav-menu-icon-container")).forEach((e) => {
        e.addEventListener("pointerdown", () => {
            Array.from(document.getElementsByClassName("nav-bar-container")).forEach(nav => {
                nav.style.display = nav.style.display == "none" ? "flex" : "none";
            });
        });
    })
}

// js for dropdown
let subMenu = document.getElementById("subMenu");
function toggleMenu(){
    subMenu.classList.toggle("open-menu")
}

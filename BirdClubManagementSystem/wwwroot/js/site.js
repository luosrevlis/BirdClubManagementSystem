window.onload = (event) => {
    init();
}

// add functions that you want to run after the page is FULLY loaded here
function init() {
    setUpNavBar();
    setUpActiveNavLink();
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
function toggleMenu() {
    subMenu.classList.toggle("open-menu")
}

// set up active nav link
function setUpActiveNavLink() {
    const path = window.location.pathname.toLowerCase();
    const navLinks = document.getElementsByClassName("nav-link");

    if (path.includes("home") && path.includes("index")) {
        navLinks.item(0).classList.add("nav-active");
    }  else if (path.includes("blogs")) {
        navLinks.item(1).classList.add("nav-active");
    } else if (path.includes("clubevents")) {
        navLinks.item(2).classList.add("nav-active");
    } else if (path.includes("home") && path.includes("about")) {
        navLinks.item(3).classList.add("nav-active");
    } else if (path.includes("home") && path.includes("contact")) {
        navLinks.item(4).classList.add("nav-active");
    } else {
        navLinks.item(0).classList.add("nav-active");
    }
}
const sideMenu = document.querySelector("aside");
const menuBtn =document.querySelector("#menu-btn");
const closeBtn = document.querySelector("#close-btn");

menuBtn.addEventListener('click', () => {
    sideMenu.style.display = 'block';
});

closeBtn.addEventListener('click', () => {
    sideMenu.style.display = 'none';
});

window.addEventListener('resize', () => {
    if (window.outerWidth >= 768) {
        sideMenu.style.display = 'block';
    }
})
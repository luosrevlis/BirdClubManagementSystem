function initDelete() {
    document.querySelector(".delete-event-container").style.display = "flex";
}

function cancelDelete() {
    document.querySelector(".delete-event-container").style.display = "none";
}

function initStatus() {
    document.querySelector(".update-status-container").style.display = "flex";
}

function cancelStatus() {
    document.querySelector(".update-status-container").style.display = "none";
}

function showDropdown() {
    document.getElementById("dropdown-content").classList.toggle("dropdown-show");
}
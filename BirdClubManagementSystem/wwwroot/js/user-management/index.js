function initDelete(userId) {
    document.querySelector(".delete-container").style.display = "flex";
    document.querySelector("#user-id").value = userId;
}

function cancelDelete() {
    document.querySelector(".delete-container").style.display = "none";
    document.querySelector("#user-id").value = "";
}
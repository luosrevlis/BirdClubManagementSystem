function initDelete(regId) {
    document.querySelector(".delete-container").style.display = "flex";
    document.querySelector("#reg-id").value = regId;
}

function cancelDelete() {
    document.querySelector(".delete-container").style.display = "none";
    document.querySelector("#reg-id").value = "";
}
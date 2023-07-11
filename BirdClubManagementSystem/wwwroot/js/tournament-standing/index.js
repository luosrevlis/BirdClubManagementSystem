function initDelete(tsId) {
    document.querySelector(".delete-container").style.display = "flex";
    document.querySelector("#ts-id").value = tsId;
}

function cancelDelete() {
    document.querySelector(".delete-container").style.display = "none";
    document.querySelector("#ts-id").value = "";
}
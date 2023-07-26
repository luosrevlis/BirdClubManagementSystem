const deleteContainer = document.querySelector(".delete-container"),
    toggleContainer = document.querySelector(".toggle-container");

function initDelete(regId) {
    deleteContainer.style.display = "flex";
    deleteContainer.querySelector("#reg-id").value = regId;
}

function cancelDelete() {
    deleteContainer.style.display = "none";
    deleteContainer.querySelector("#reg-id").value = "";
}

function initToggle(regId) {
    toggleContainer.style.display = "flex";
    toggleContainer.querySelector("#reg-id").value = regId;
}

function cancelToggle() {
    toggleContainer.style.display = "none";
    toggleContainer.querySelector("#reg-id").value = "";
}
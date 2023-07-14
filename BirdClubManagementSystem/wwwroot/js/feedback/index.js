function initDelete(feedbackId) {
    document.querySelector(".delete-container").style.display = "flex";
    document.querySelector("#feedback-id").value = feedbackId;
}

function cancelDelete() {
    document.querySelector(".delete-container").style.display = "none";
    document.querySelector("#feedback-id").value = "";
}
const acceptContainer = document.querySelector(".accept-container"),
    rejectContainer = document.querySelector(".delete-container");

function initAccept(reqId) {
    acceptContainer.style.display = "flex";
    acceptContainer.querySelector("#req-id").value = reqId;
}

function cancelAccept() {
    acceptContainer.style.display = "none";
    acceptContainer.querySelector("#req-id").value = "";
}

function initReject(reqId) {
    rejectContainer.style.display = "flex";
    rejectContainer.querySelector("#req-id").value = reqId;
}

function cancelReject() {
    rejectContainer.style.display = "none";
    rejectContainer.querySelector("#req-id").value = "";
}
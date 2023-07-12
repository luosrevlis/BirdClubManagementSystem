const form = document.querySelector(".password-form"),
    newPassField = form.querySelector(".new-password-field"),
    newPassInput = form.querySelector(".new-password"),
    confirmPassField = form.querySelector(".confirm-password-field"),
    confirmPassInput = form.querySelector(".confirm-password"),
    oldPassField = form.querySelector(".old-password-field"),
    oldPassInput = form.querySelector(".old-password");

//Old password validation
function checkOldPass() {
    if (oldPassInput.value === "") {
        oldPassField.classList.add("invalid");
        return false;
    }
    oldPassField.classList.remove("invalid");
    return true;
}

//New password validation
function checkNewPass() {
    const passPattern = /^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])[A-Za-z0-9!@#$%^&*]{8,}$/;

    if (!newPassInput.value.match(passPattern)) {
        newPassField.classList.add("invalid");
        return false; //add invalid class if password does not match with pattern
    }
    newPassField.classList.remove("invalid");
    return true;
}

//Confirm password validation
function checkConfirmPass() {
    if (confirmPassInput.value === "" || newPassInput.value !== confirmPassInput.value) {
        confirmPassField.classList.add("invalid");
        return false;
    }
    confirmPassField.classList.remove("invalid");
    return true;
}

function validateNew() {
    return (checkNewPass() && checkConfirmPass());
}

function validateAll() {
    return (checkOldPass() && checkNewPass() && checkConfirmPass());
}

//calling function on key up
newPassInput.addEventListener("keyup", checkNewPass);
confirmPassInput.addEventListener("keyup", checkConfirmPass);
oldPassInput.addEventListener("keyup", checkOldPass);

const form = document.querySelector("form"),
    passField = form.querySelector(".create-password"),
    passInput = passField.querySelector(".password"),
    cPassField = form.querySelector(".confirm-password"),
    cPassInput = cPassField.querySelector(".cPassword"),
    oPassField = form.querySelector(".old-password"),
    oPassInput = oPassField.querySelector(".oPassword");

//Old password validation
function oldPassword() {
    if (oPassInput.value === "") {
        oPassField.classList.add("invalid");
        return false;
    }
    oPassField.classList.remove("invalid");
    return true;
}

//Password Validation
function createPass() {
    const passPattern = /^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])[A-Za-z0-9!@#$%^&*]{8,}$/;

    if (!passInput.value.match(passPattern)) {
        passField.classList.add("invalid");
        return false; //add invalid class if password does not match with pattern
    }
    passField.classList.remove("invalid");
    return true;
}

//Confirm password validation
function confirmPass() {
    if (passInput.value !== cPassInput.value || cPassInput.value === "") {
        cPassField.classList.add("invalid");
        return false;
    }
    cPassField.classList.remove("invalid");
    return true;
}

function validateNew() {
    return (createPass() && confirmPass());
}

function validateAll() {
    return (oldPassword() && createPass() && confirmPass());
}

//calling function on key up
oPassInput.addEventListener("keyup", oldPassword);
passInput.addEventListener("keyup", createPass);
cPassInput.addEventListener("keyup", confirmPass);


////Calling function on Form Submit
//form.addEventListener("submit", (e) => {
//    e.preventDefault(); //preventing form submitting
//    oldPassword();
//    createPass();
//    confirmPass();

//    if(!passField.classList.contains("invalid") && !cPassField.classList.contains("invalid")){
//            location.href = form.getAttribute("action of form") //lay action cua form
//    }
//});
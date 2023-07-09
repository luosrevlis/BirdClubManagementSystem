const form = document.querySelector(".form-pass"), 
        oPassField = form.querySelector(".old-password"),
        oPassInput = oPassField.querySelector(".oPassword"),
        passField = form.querySelector(".create-password"),
        passInput = passField.querySelector(".password"),
        cPassField = form.querySelector(".confirm-password"),
        cPassInput = cPassField.querySelector(".cPassword");

//Old password validation
function oldPassword(){
    if(oPassInput.value === ""){
        return oPassField.classList.add("invalid");
    }
    oPassField.classList.remove("invalid");
}

//Password Validation
function createPass(){
    const passPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

    if(!passInput.value.match(passPattern)){
        return passField.classList.add("invalid"); //add invalid class if password does not match with pattern
    }
    passField.classList.remove("invalid");
}

//Confirm password validation
function confirmPass(){
    if(passInput.value !== cPassInput.value || cPassInput.value ===""){
        return cPassField.classList.add("invalid");
    }
    cPassField.classList.remove("invalid")
}

//Calling function on Form Submit
form.addEventListener("submit", (e) => {
    e.preventDefault(); //preventing form submitting
    oldPassword();
    createPass();
    confirmPass();

    //calling function on key up
    oPassInput.addEventListener("keyup", oldPassword);
    passInput.addEventListener("keyup", createPass);
    cPassInput.addEventListener("keyup", confirmPass);

    if(!passField.classList.contains("invalid") && !cPassField.classList.contains("invalid")){
            location.href = form.getAttribute("action of form") //lay action cua form
    }
});
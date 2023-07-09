const form = document.querySelector("form"),
    emailField = form.querySelector(".email-field"),
    emailInput = emailField.querySelector(".email"),
    passwordField = form.querySelector(".password-field"),
    passwordInput = passwordField.querySelector(".password"),
    fullnameField = form.querySelector(".fullname-field"),
    fullnameInput = fullnameField.querySelector(".fullname"),
    addressField = form.querySelector(".address-field"),
    addressInput = addressField.querySelector(".address"),
    phonenumberField = form.querySelector(".phonenumber-field"),
    phonenumberInput = phonenumberField.querySelector(".phonenumber");

//Email validation 
function checkEmail() {
    const emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;

    if (!emailInput.value.match(emailPattern)) {
        emailField.classList.add("invalid");//adding invalid class if email value do not matched with email pattern
        return false;
    }
    emailField.classList.remove("invalid");//remove invalid class if email value matched with email pattern
    return true;
}

//Password Validation
function checkPassword() {
    const passPattern = /^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])[A-Za-z0-9!@#$%^&*]{8,}$/;

    if (!passwordInput.value.match(passPattern)) {
        passwordField.classList.add("invalid");
        return false;
    }
    passwordField.classList.remove("invalid");
    return true;
}

// Fullname validation
function checkFullname() {
    if (fullnameInput.value === "") {
        fullnameField.classList.add("invalid");
        return false;
    }
    fullnameField.classList.remove("invalid");
    return true;
}

// Address validation
function checkAddress() {
    if (addressInput.value === "") {
        addressField.classList.add("invalid");
        return false;
    }
    addressField.classList.remove("invalid");
    return true;
}

// Phone number validation
function checkPhoneNumber() {
    const phonenumberPattern = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/;

    if (!phonenumberInput.value.match(phonenumberPattern)) {
        phonenumberField.classList.add("invalid");
        return false;
    }
    phonenumberField.classList.remove("invalid");
    return true;
}

function validateAll() {
    return (checkEmail() && checkPassword() && checkFullname() && checkAddress() && checkPhoneNumber());
}

emailInput.addEventListener("keyup", checkEmail);
passwordInput.addEventListener("keyup", checkPassword);
fullnameInput.addEventListener("keyup", checkFullname);
addressInput.addEventListener("keyup", checkAddress);
phonenumberInput.addEventListener("keyup", checkPhoneNumber);
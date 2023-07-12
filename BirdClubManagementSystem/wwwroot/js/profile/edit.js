const form = document.querySelector("form"),
    fullnameField = form.querySelector(".fullname-field"),
    fullnameInput = fullnameField.querySelector(".fullname"),
    addressField = form.querySelector(".address-field"),
    addressInput = addressField.querySelector(".address"),
    phonenumberField = form.querySelector(".phonenumber-field"),
    phonenumberInput = phonenumberField.querySelector(".phonenumber");

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
    return (checkFullname() && checkAddress() && checkPhoneNumber());
}

fullnameInput.addEventListener("keyup", checkFullname);
addressInput.addEventListener("keyup", checkAddress);
phonenumberInput.addEventListener("keyup", checkPhoneNumber);
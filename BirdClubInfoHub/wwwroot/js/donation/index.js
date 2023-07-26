const form = document.querySelector(".form"),
    nameField = form.querySelector(".name-field"),
    nameInput = form.querySelector(".name"),
    emailField = form.querySelector(".email-field"),
    emailInput = form.querySelector(".email"),
    amountField = form.querySelector(".amount-field"),
    amountInput = form.querySelector(".amount");

function checkName() {
    if (nameInput.value === "") {
        nameField.classList.add("invalid");
        return false;
    }
    nameField.classList.remove("invalid");
    return true;
}

function checkEmail() {
    const emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    if (!emailInput.value.match(emailPattern)) {
        emailField.classList.add("invalid");
        return false;
    }
    emailField.classList.remove("invalid");
    return true;
}

function checkAmount() {
    var parseValue = parseInt(amountInput.value);
    amountInput.value = parseValue;
    if (parseValue < 1000 || parseValue > 999999999) {
        amountField.classList.add("invalid");
        return false;
    }
    amountField.classList.remove("invalid");
    return true;
}

function validateAll() {
    return (checkName() && checkEmail() && checkAmount());
}

nameInput.addEventListener("keyup", checkName);
emailInput.addEventListener("keyup", checkEmail);
amountInput.addEventListener("keyup", checkAmount);
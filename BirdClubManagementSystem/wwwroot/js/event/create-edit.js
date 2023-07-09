const form = document.querySelector("form"),
    nameField = form.querySelector(".name-field"),
    nameInput = form.querySelector(".name"),
    dateField = form.querySelector(".date-field"),
    dateInput = form.querySelector(".date"),
    regDateField = form.querySelector(".reg-date-field"),
    regDateInput = form.querySelector(".reg-date"),
    feeField = form.querySelector(".fee-field"),
    feeInput = form.querySelector(".fee");

// Name validation
function checkName() {
    if (nameInput.value === "") {
        nameField.classList.add("invalid");
        return false;
    }
    nameField.classList.remove("invalid");
    return true;
}

// Registration date validation
function checkRegDate() {
    if (dateInput.value < regDateInput.value) {
        dateField.classList.add("invalid");
        return false;
    }
    dateField.classList.remove("invalid");
    return true;
}

// Fee validation
function checkFee() {
    if (feeInput.value === "") {
        feeField.classList.add("invalid");
        return false;
    }
    feeField.classList.remove("invalid");
    return true;
}

function validateAll() {
    return (checkName() && checkRegDate() && checkFee());
}

nameInput.addEventListener("keyup", checkName);
dateInput.addEventListener("change", checkRegDate);
regDateInput.addEventListener("change", checkRegDate);
feeInput.addEventListener("keyup", checkFee);
const form = document.querySelector("form"),
    nameField = form.querySelector(".name-field"),
    nameInput = form.querySelector(".name"),
    startDateField = form.querySelector(".start-date-field"),
    startDateInput = form.querySelector(".start-date"),
    startDateError = startDateField.querySelector(".error-text"),
    endDateField = form.querySelector(".end-date-field"),
    endDateInput = form.querySelector(".end-date"),
    endDateError = endDateField.querySelector(".error-text"),
    regOpenDateField = form.querySelector(".reg-open-date-field"),
    regOpenDateInput = form.querySelector(".reg-open-date"),
    regOpenDateError = regOpenDateField.querySelector(".error-text"),
    regCloseDateField = form.querySelector(".reg-close-date-field"),
    regCloseDateInput = form.querySelector(".reg-close-date"),
    regCloseDateError = regCloseDateField.querySelector(".error-text"),
    regLimitField = form.querySelector(".reg-limit-field"),
    regLimitInput = form.querySelector(".reg-limit"),
    feeField = form.querySelector(".fee-field"),
    feeInput = form.querySelector(".fee");

const today = new Date();

// Name validation
function checkName() {
    if (nameInput.value.length < 1 || nameInput.value.length > 255) {
        nameField.classList.add("invalid");
        return false;
    }
    nameField.classList.remove("invalid");
    return true;
}

// Subroutine to check if a date value is in the past
function isPastDate(date) {
    return (date.getTime() < today.getTime());
}

// Subroutine to check if a date value is more than 1 year into the future
function isOneYearLater(date) {
    // Remove 1 year from date
    var tempDate = new Date(date.getTime());
    tempDate.setFullYear(tempDate.getFullYear() - 1);
    return (tempDate.getTime() > today.getTime());
}

// Start date validation
function checkStartDate() {
    var date = new Date(startDateInput.value);
    if (isPastDate(date) || isOneYearLater(date)) {
        startDateField.classList.add("invalid");
        startDateError.innerHTML = "Must be within 1 year from now.";
        return false;
    }
    // Order
    if (date.getTime() < new Date(regCloseDateInput.value).getTime()) {
        startDateField.classList.add("invalid");
        startDateError.innerHTML = "Event cannot start before registrations are closed!";
        return false;
    }
    startDateField.classList.remove("invalid");
    return true;
}

// Expected end date validation
function checkEndDate() {
    var date = new Date(endDateInput.value);
    if (isPastDate(date) || isOneYearLater(date)) {
        endDateField.classList.add("invalid");
        endDateError.innerHTML = "Must be within 1 year from now.";
        return false;
    }
    // Order
    if (date.getTime() < new Date(startDateInput.value).getTime()) {
        endDateField.classList.add("invalid");
        endDateError.innerHTML = "Event cannot end before it is started!";
        return false;
    }
    endDateField.classList.remove("invalid");
    return true;
}

function checkRegOpenDate() {
    var date = new Date(regOpenDateInput.value);
    if (isOneYearLater(date)) {
        regOpenDateField.classList.add("invalid");
        regOpenDateError.innerHTML = "Must be between last 4 months and next year.";
        return false;
    }
    // Allow 4 months into the past
    date.setMonth(date.getMonth() + 4);
    if (isPastDate(date)) {
        regOpenDateField.classList.add("invalid");
        regOpenDateError.innerHTML = "Must be between last 4 months and next year.";
        return false;
    }
    regOpenDateField.classList.remove("invalid");
    return true;
}

// Registration close date validation
function checkRegCloseDate() {
    var date = new Date(regCloseDateInput.value);
    if (isPastDate(date) || isOneYearLater(date)) {
        regCloseDateField.classList.add("invalid");
        regCloseDateError.innerHTML = "Must be within 1 year from now.";
        return false;
    }
    // Order
    if (date.getTime() < new Date(regOpenDateInput.value).getTime()) {
        regCloseDateField.classList.add("invalid");
        regCloseDateError.innerHTML = "Reg. close date must be after reg. open date!";
        return false;
    }
    regCloseDateField.classList.remove("invalid");
    return true;
}

// Reg limit validation
function checkRegLimit() {
    var parseValue = parseInt(regLimitInput.value);
    regLimitInput.value = parseValue;
    if (parseValue < 1 || parseValue > 200) {
        regLimitField.classList.add("invalid");
        return false;
    }
    regLimitField.classList.remove("invalid");
    return true;
}

// Fee validation
function checkFee() {
    var parseValue = parseInt(feeInput.value);
    feeInput.value = parseValue;
    if (parseValue < 0 || parseValue > 100000000) {
        feeField.classList.add("invalid");
        return false;
    }
    feeField.classList.remove("invalid");
    return true;
}

function validateAll() {
    return (checkName()
        && checkStartDate()
        && checkEndDate()
        && checkRegOpenDate()
        && checkRegCloseDate()
        && checkRegLimit()
        && checkFee());
}

nameInput.addEventListener("keyup", checkName);
startDateInput.addEventListener("change", checkStartDate);
endDateInput.addEventListener("change", checkEndDate);
regOpenDateInput.addEventListener("change", checkRegOpenDate);
regCloseDateInput.addEventListener("change", checkRegCloseDate);
regLimitInput.addEventListener("keyup", checkRegLimit);
feeInput.addEventListener("keyup", checkFee);
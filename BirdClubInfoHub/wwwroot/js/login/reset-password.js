const form = document.querySelector(".email-form"),
    emailField = form.querySelector(".email-field"),
    emailInput = form.querySelector(".email");

//Email validation 
function checkEmail() {
    const emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    if (!emailInput.value.match(emailPattern)) {
        emailField.classList.add("invalid");//adding invalid class if email value do not matched with email pattern
        return false;
    }
    emailField.classList.remove("invalid");
    return true;
}

//calling function on key up
emailInput.addEventListener("keyup", checkEmail);
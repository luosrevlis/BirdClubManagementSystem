const form = document.querySelector("form"),
    emailField = form.querySelector(".email-field"),
    emailInput = emailField.querySelector(".email");

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


////Calling function on Form Submit
//form.addEventListener("submit", (e) => {
//    e.preventDefault(); //preventing form submitting
//    checkEmail();

//    if (!emailField.classList.contains("invalid")) {
//        location.href = form.getAttribute("action") //lay action cua form
//    }
//});
const form = document.querySelector("form"),
    emailField = form.querySelector(".email-field"),
    emailInput = emailField.querySelector(".email");

//Email validation 
function checkEmail() {
    const emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    if (!emailInput.value.match(emailPattern)) {
        return emailField.classList.add("invalid");//adding invalid class if email value do not matched with email pattern
    }
    emailField.classList.remove("invalid");//remove invalid class if email value matched with email pattern
}





//Calling function on Form Submit
form.addEventListener("submit", (e) => {
    e.preventDefault(); //preventing form submitting
    checkEmail();

    //calling function on key up
    emailInput.addEventListener("keyup", checkEmail);

    if (!emailField.classList.contains("invalid")) {
        location.href = form.getAttribute("action of form") //lay cation cua form
    }
});
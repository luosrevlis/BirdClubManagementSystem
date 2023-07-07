const form = document.querySelector("form"),
    emailField = form.querySelector(".email-field"),
    emailInput = emailField.querySelector(".email"),
    fullnameField = form.querySelector(".fullname-field"),
    fullnameInput = form.querySelector(".fullname"),
    addressField = form.querySelector(".address-field"),
    addressInput = form.querySelector(".address"),
    phonenumberField = form.querySelector(".phonenumber-field"),
    phonenumberInput = form.querySelector(".phonenumber");

    var popup = document.getElementById("popup");
function closeSlide(){
    popup.classList.remove(".open-slide");
}
//Email validation
function checkEmail() {
    const emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
    if (!emailInput.value.match(emailPattern)) {
        return emailField.classList.add("invalid");//adding invalid class if email value do not matched with email pattern
    }
    emailField.classList.remove("invalid");//remove invalid class if email value matched with email pattern
}
function checkFullname(){
    if(fullnameInput.value === ""){
        return fullnameField.classList.add("invalid");
    }
    fullnameField.classList.remove("invalid");
}
function checkAddress(){
    if(addressInput.value === ""){
        return addressField.classList.add("invalid");
    }
    addressField.classList.remove("invalid")
}
function checkPhonenumber(){
    if(phonenumberInput.value === ""){
        return phonenumberField.classList.add("invalid");
    }
    phonenumberField.classList.remove("invalid");
}






//Calling function on Form Submit
form.addEventListener("submit", (e) => {
    e.preventDefault(); //preventing form submitting
    checkEmail();
    checkFullname();
    checkAddress();
    checkPhonenumber();

    //calling function on key up
    emailInput.addEventListener("keyup", checkEmail);
    fullnameInput.addEventListener("keyup", checkFullname);
    addressInput.addEventListener("keyup", checkAddress);
    phonenumberInput.addEventListener("keyup", checkPhonenumber);
    

    if (!emailField.classList.contains("invalid") && !fullnameField.classList.contains("invalid") && !addressField.classList.contains("invalid") && !phonenumberField.classList.contains("invalid")) {
        popup.classList.add(".open-slide")
        // location.href = form.getAttribute("action of form") //lay action cua form
    }
});


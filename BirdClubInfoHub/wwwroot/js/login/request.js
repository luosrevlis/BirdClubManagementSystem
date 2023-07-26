const form = document.querySelector("form"),
    emailField = form.querySelector(".email-field"),
    emailInput = emailField.querySelector(".email"),
    fullnameField = form.querySelector(".fullname-field"),
    fullnameInput = form.querySelector(".fullname"),
    addressField = form.querySelector(".address-field"),
    addressInput = form.querySelector(".address"),
    phonenumberField = form.querySelector(".phonenumber-field"),
    phonenumberInput = form.querySelector(".phonenumber");

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

function checkFullname(){
    if(fullnameInput.value === ""){
        fullnameField.classList.add("invalid");
        return false;
    }
    fullnameField.classList.remove("invalid");
    return true;
}

function checkAddress(){
    if(addressInput.value === ""){
        addressField.classList.add("invalid");
        return false;
    }
    addressField.classList.remove("invalid")
    return true;
}

function checkPhonenumber(){
    const phonenumberPattern = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/;

    if (!phonenumberInput.value.match(phonenumberPattern)) {
        phonenumberField.classList.add("invalid");
        return false;
    }
    phonenumberField.classList.remove("invalid");
    return true;
}

function validateAll() {
    return (checkEmail() && checkFullname() && checkAddress() && checkPhonenumber());
}

//calling function on key up
emailInput.addEventListener("keyup", checkEmail);
fullnameInput.addEventListener("keyup", checkFullname);
addressInput.addEventListener("keyup", checkAddress);
phonenumberInput.addEventListener("keyup", checkPhonenumber);




////Calling function on Form Submit
//form.addEventListener("submit", (e) => {
//    e.preventDefault(); //preventing form submitting
//    checkEmail();
//    checkFullname();
//    checkAddress();
//    checkPhonenumber();

    
    

//    if (!emailField.classList.contains("invalid") && !fullnameField.classList.contains("invalid") && !addressField.classList.contains("invalid") && !phonenumberField.classList.contains("invalid")) {
//        popup.classList.add(".open-slide")
//        // location.href = form.getAttribute("action of form") //lay action cua form
//    }
//});


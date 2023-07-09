const inputs = document.querySelectorAll(".input-field");
const bullets = document.querySelectorAll(".bullets span");
const images = document.querySelectorAll(".image")

inputs.forEach((inp) => {
    inp.addEventListener("focus", () => {
        inp.classList.add("active")
    });
    inp.addEventListener("blur", () => {
        if (inp.value != "") return
        inp.classList.remove("active");
    });
});

function moveSlider() {
    let index = this.dataset.value;

    let currentImage = document.querySelector(`.img-${index}`);
    images.forEach((img) => img.classList.remove("show"));
    currentImage.classList.add("show");

    const textSlider = document.querySelector(".text-group");
    textSlider.style.transform = `translateY(${-(index - 1) * 2.6}rem)`;

    bullets.forEach(bull => bull.classList.remove("active"));
    this.classList.add("active");
}
// *name, địa chỉ, sđt, email,password

bullets.forEach((bullet) => {
    bullet.addEventListener("click", moveSlider)
})

//validation email and password
const emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
function checkAccount(){
    if (!document.Formfill.Email.value.match(emailPattern)) {
        document.getElementById("result").innerHTML = "Please enter a valid email.";
        return false;
    }else if(document.Formfill.Password.value == ""){
        document.getElementById("result").innerHTML = "Password cannot be empty."
        return false;
    }
}

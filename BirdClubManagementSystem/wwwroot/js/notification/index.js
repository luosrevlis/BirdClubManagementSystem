const form = document.querySelector(".form"),
    contentsField = form.querySelector(".contents-field"),
    contentsInput = form.querySelector(".contents");

function checkContents() {
    if (contentsInput.value === "") {
        contentsField.classList.add("invalid");
        return false;
    }
    contentsField.classList.remove("invalid");
    return true;
}

contentsInput.addEventListener("keyup", checkContents);
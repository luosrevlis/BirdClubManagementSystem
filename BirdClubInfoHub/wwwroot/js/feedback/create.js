const form = document.querySelector(".form"),
    titleField = form.querySelector(".title-field"),
    titleInput = form.querySelector(".title"),
    contentsField = form.querySelector(".contents-field"),
    contentsInput = form.querySelector(".contents");

function checkTitle() {
    if (titleInput.value === "") {
        titleField.classList.add("invalid");
        return false;
    }
    titleField.classList.remove("invalid");
    return true;
}

function checkContents() {
    if (contentsInput.value === "") {
        contentsField.classList.add("invalid");
        return false;
    }
    contentsField.classList.remove("invalid");
    return true;
}

function validateAll() {
    return (checkTitle() && checkContents());
}

titleInput.addEventListener("keyup", checkTitle);
contentsInput.addEventListener("keyup", checkContents);
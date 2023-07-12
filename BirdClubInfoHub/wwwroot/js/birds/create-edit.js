const form = document.querySelector(".form"),
    nameField = form.querySelector(".name-field"),
    nameInput = form.querySelector(".name");

var loadFile = function (event) {
	var image = document.getElementById('preview-pfp');
	image.src = URL.createObjectURL(event.target.files[0]);
};

function checkName() {
    if (nameInput.value === "") {
        nameField.classList.add("invalid");
        return false;
    }
    nameField.classList.remove("invalid");
    return false;
}

nameInput.addEventListener("keyup", checkName);
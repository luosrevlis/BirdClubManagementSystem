var loadFile = function (event) {
	var image = document.getElementById('preview-thumbnail');
	image.src = URL.createObjectURL(event.target.files[0]);
};
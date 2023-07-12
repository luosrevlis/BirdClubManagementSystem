var loadFile = function(event) {
	var image = document.getElementById('preview-pfp');
	image.src = URL.createObjectURL(event.target.files[0]);
};
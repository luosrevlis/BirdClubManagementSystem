document.getElementById("blog-thumbnail").onchange = (e) => {
    const fileName = e.target.files[0].name;
    document.getElementById("thumbnail-name").textContent = "You selected: " + fileName;
};

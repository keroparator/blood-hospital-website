// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    var topBar = document.querySelector(".top-bar");
    
    function toggleTopBar() {
        if (window.scrollY > 50) {
            topBar.classList.add("visible");
        } else {
            topBar.classList.remove("visible");
        }
    }

    // Initial check
    toggleTopBar();

    // Scroll listener
    window.addEventListener("scroll", toggleTopBar);
});

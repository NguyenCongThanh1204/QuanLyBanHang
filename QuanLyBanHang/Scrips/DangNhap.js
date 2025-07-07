// File: Scripts/DangNhap.js
window.addEventListener("DOMContentLoaded", () => {
    const signUpButton = document.getElementById("signUp");
    const signInButton = document.getElementById("signIn");
    const container = document.getElementById("auth-wrapper");


    signUpButton.addEventListener("click", () => {
        container.classList.add("right-panel-active");
        console.log("Switched to SignUp");
    });

    signInButton.addEventListener("click", () => {
        container.classList.remove("right-panel-active");
        console.log("Switched to SignIn");
    });
});

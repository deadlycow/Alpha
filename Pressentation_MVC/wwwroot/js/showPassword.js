document.addEventListener('DOMContentLoaded', () => {

    document.querySelectorAll(".toggle-password").forEach(icon => {
        icon.addEventListener("click", togglePasswordShow);
    });

})
function togglePasswordShow(event) {
    const eyeIcon = event.currentTarget;
    const passwordInput = eyeIcon.closest(".password-container").querySelector(".input-field");
    if (passwordInput.type === "password") {
        passwordInput.type = "text";
        eyeIcon.src = "/images/eye-open.svg";
    } else {
        passwordInput.type = "password";
        eyeIcon.src = "/images/eye-closed.svg";
    }
}
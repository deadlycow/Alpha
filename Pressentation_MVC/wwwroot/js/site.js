const togglePassword = (event) => {
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

const toggleShow = (e) => {
    let targetClass = "";
    let containerClass = "";

    if (e.currentTarget.classList.contains("profile-container")) {
        targetClass = ".options-container";
    }
    if (e.currentTarget.classList.contains("project-more")) {
        const parent = e.currentTarget.closest(".project-card");
        if (!parent) return;
        targetClass = ".edit-container";
        containerClass = parent;
    }

    const div = containerClass ? containerClass.querySelector(targetClass) : document.querySelector(targetClass);
    if (div) {
        div.classList.toggle("show");
    }
};

document.querySelectorAll(".toggle-password").forEach(icon => {
    icon.addEventListener("click", togglePassword);
});

document.querySelectorAll(".project-more").forEach(more => {
    more.addEventListener("click", toggleShow);
});
document.querySelector(".profile-container").addEventListener("click", toggleShow);


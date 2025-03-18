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

    if (e.currentTarget.classList.contains("settings")) {
        targetClass = ".options-container";
    }

    const div = containerClass ? containerClass.querySelector(targetClass) : document.querySelector(targetClass);
    if (div) {
        div.classList.toggle("show");
    }
};

document.querySelectorAll(".toggle-password").forEach(icon => {
    icon.addEventListener("click", togglePassword);
});

document.querySelector(".settings").addEventListener("click", toggleShow);

const menuButtons = document.querySelectorAll('[data-menu="true"]');
menuButtons.forEach(button => {
    button.addEventListener('click', (event) => {
        const currentCard = event.currentTarget.closest('[data-menu]');
        if (currentCard) {
            const menu = currentCard.querySelector('[data-display]');
            if (menu) {
                menu.style.display = (menu.style.display === 'grid') ? 'none' : 'grid';
            }
        }
    })
})

const modalButtons = document.querySelectorAll('[data-modal="true"]');
console.log(modalButtons)
modalButtons.forEach(button => {
    button.addEventListener('click', () => {
        const modalTarget = button.getAttribute('data-target');
        const modal = document.querySelector(modalTarget);

        if (modal)
            modal.style.display = 'flex';
    })
})

const closeButtons = document.querySelectorAll('[data-close="true"');
closeButtons.forEach(button => {
    button.addEventListener('click', () => {
        const modal = button.closest('.modal-overlay');
        if (modal)
            modal.style.display = 'none';
    })
})



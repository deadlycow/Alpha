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

document.querySelectorAll(".toggle-password").forEach(icon => {
    icon.addEventListener("click", togglePassword);
});

const menuButtons = document.querySelectorAll('[data-menu="true"]');
menuButtons.forEach(button => {
    button.addEventListener('click', (event) => {


        const currentCard = event.currentTarget.closest('[data-menu]');
        if (currentCard) {

            const menu = currentCard.querySelector('[data-display]');

            const isOpen = menu.style.display === 'grid';

            document.querySelectorAll('[data-display]').forEach(menu => {
                menu.style.display = 'none';
            });

            if (!isOpen) {
                menu.style.display = 'grid';
            }
        }
    });
});

const modalButtons = document.querySelectorAll('[data-modal="true"]');
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



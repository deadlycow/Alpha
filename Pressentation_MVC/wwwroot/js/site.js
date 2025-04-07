document.addEventListener('DOMContentLoaded', () => {

    const menuButtons = document.querySelectorAll('[data-menu="true"]');
    const modalButtons = document.querySelectorAll('[data-modal="true"]');

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

    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);

            if (modal)
                modal.style.display = 'flex';
        })
    })
})



document.addEventListener('DOMContentLoaded', () => {

    const cardMenuBtns = document.querySelectorAll('[data-card-menu="true"]');
    const modalButtons = document.querySelectorAll('[data-modal="true"]');
    const utilBtns = document.querySelectorAll('[data-util="true"]');

    cardMenuBtns.forEach(button => {
        button.addEventListener('click', (event) => {

            const currentCard = event.currentTarget.closest('[data-card-menu]');
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

    utilBtns.forEach(button => {
        button.addEventListener('click', (event) => {

            const currentCard = event.currentTarget.closest('[data-util]');
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



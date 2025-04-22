import { resetImagePreview } from './imageHandler.js'
import { clearList } from './compact-search.js'
import { clearErrorMessages } from './utils.js'

document.addEventListener('DOMContentLoaded', () => {

    const cardMenuBtns = document.querySelectorAll('[data-card-menu="true"]')
    const openModalBtns = document.querySelectorAll('[data-modal="true"]')
    const closeModalBtns = document.querySelectorAll('[data-close="true"]')
    const utilBtns = document.querySelectorAll('[data-util="true"]')
    const deleteBtns = document.querySelectorAll('[data-delete="true"]')

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
    })

    utilBtns.forEach(button => {
        button.addEventListener('click', (event) => {
            event.stopPropagation();

            document.querySelectorAll('[data-util-display]').forEach(menu => {
                menu.style.display = 'none';
            });

            const currentCard = event.currentTarget.closest('[data-util]');
            const menu = currentCard.querySelector('[data-util-display]');
            menu.style.display = 'grid';
        });
    })

    openModalBtns.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);

            if (modal)
                modal.style.display = 'flex';
        })
    })

    closeModalBtns.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal-overlay');
            const form = modal.querySelector('form');
            if (form) {
                resetImagePreview(form)
                clearErrorMessages(form)
                clearList(form)
                form.reset();
            }
            if (modal)
                modal.style.display = 'none';
        })
    })

    deleteBtns.forEach(button => {
        button.addEventListener('click', async (event) => {
            event.stopPropagation();

            const id = event.currentTarget.getAttribute('data-id');
            const target = event.currentTarget.getAttribute('data-controller');
            try {
                const res = await fetch(`/${target}/delete/${id}`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ id })
                })
                if (res.ok) {
                    window.location.reload();
                }
            }
            catch (error) {
                console.error(`Error deleting ${target}:`, error);
            }

        })
    })

    const themeSwitch = document.getElementById('theme-switch')
    if (localStorage.getItem('theme') === 'dark') {
        document.body.setAttribute('data-theme', 'dark')
        themeSwitch.checked = true
    }

    themeSwitch.addEventListener('change', () => {
        if (themeSwitch.checked) {
            document.body.setAttribute('data-theme', 'dark')
            localStorage.setItem('theme', 'dark')
        }
        else {
            document.body.removeAttribute('data-theme')
            localStorage.removeItem('theme')
        }
    })

    document.addEventListener('click', (event) => {
        const isMenuClick = event.target.closest('[data-card-menu]')
        const isUtilClick = event.target.closest('[data-util]')
        if (!isMenuClick) {
            document.querySelectorAll('[data-display]').forEach(menu => {
                menu.style.display = 'none'
            })
            document.querySelectorAll('.member-list').forEach(menu => {
                menu.style.display = 'none'
            })
        }
        if (!isUtilClick) {
            document.querySelectorAll('[data-util-display]').forEach(menu => {
                menu.style.display = 'none'
            })
        }
    })
})



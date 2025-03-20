document.addEventListener('DOMContentLoaded', () => {
    const previewSize = 150;

    const menuButtons = document.querySelectorAll('[data-menu="true"]');
    const modalButtons = document.querySelectorAll('[data-modal="true"]');
    const closeButtons = document.querySelectorAll('[data-close="true"]');

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

    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal-overlay');
            if (modal)
                modal.style.display = 'none';
        })
    })

    document.querySelectorAll(".image-previewer").forEach(previewer => {
        const fileInput = previewer.querySelector('input[type="file"]');
        const imagePreview = previewer.querySelector('.image-preview');

        previewer.addEventListener('click', () => fileInput.click())

        fileInput.addEventListener('change', ({ target: { files } }) => {
            const file = files[0];
            if (file)
                processImage(file, imagePreview, previewer, previewSize)

        })
    })


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



    const loadImage = async (file) => {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();

            reader.onerror = () => reject(new Error("Failed to load file."));
            reader.onload = (e) => {
                const img = new Image();
                img.onerror = () => reject(new Error("Failed to load image "));
                img.onload = () => resolve(img);
                img.src = e.target.result;
            }
            reader.readAsDataURL(file);
        })
    }

    const processImage = async (file, imagePreveiwer, previewer, previewSize = 250) => {
        try {
            const img = await loadImage(file);
            const canvas = document.createElement('canvas');
            canvas.width = previewSize;
            canvas.height = previewSize;

            const ctx = canvas.getContext('2d');
            ctx.drawImage(img, 0, 0, previewSize, previewSize);
            imagePreveiwer.src = canvas.toDataURL('image/jpeg');
            previewer.classList.add('selected');
        }
        catch (error) {
            console.error('Failed on image-processing:', error);
        }
    }
})
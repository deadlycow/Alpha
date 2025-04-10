import { setupImagePreviewer, resetImagePreview, getPreviewImagePath } from './imageHandler.js'

document.addEventListener('DOMContentLoaded', () => {
    const closeButtons = document.querySelectorAll('[data-close="true"]')
    const forms = document.querySelectorAll('#form-reg');

    setupImagePreviewer()

    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal-overlay')
            const form = modal.querySelector('form')

            if (form) {
                resetImagePreview(form)
                form.reset()
            }

            if (modal)
                modal.style.display = 'none'
        })
    })

    forms.forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault()
            clearErrorMessages(form)

            const formData = new FormData(form)

            const fileInput = form.querySelector('input[type="file"]')
            if (!fileInput.files.length) {
                const imagePath = getPreviewImagePath(form)
                if (imagePath) {
                    formData.append('ProfileImage', imagePath)
                }
            }

            try {
                const res = await fetch(form.action, {
                    method: 'post',
                    body: formData,
                })

                if (res.ok) {
                    const modal = form.closest('.modal')
                    if (modal)
                        modal.style.display = 'none'
                    else
                        window.location.reload()
                }
                else if (res.status === 400) {
                    const data = await res.json()

                    if (data.errors) {
                        Object.keys(data.errors).forEach(key => {
                            let input = form.querySelector(`[name="${key}"]`)
                            if (input) {
                                input.classList.add('input-validation-error')
                            }

                            let span = form.querySelector(`[data-valmsg-for="${key}"]`)
                            if (span) {
                                span.innerText = data.errors[key].join('\n')
                                span.classList.add('field-validation-error')
                            }
                        })
                    }
                }
            }
            catch {
                console.log('error submitting the form')
            }
        })
    })
})

function clearErrorMessages(form) {
    form.querySelectorAll('[data-val="true"]').forEach(input => {
        input.classList.remove('input-validation-error')
    })

    form.querySelectorAll('[data-valmsg-for]').forEach(span => {
        span.innerText = ''
        span.classList.remove('field-validation-error')
    })
}
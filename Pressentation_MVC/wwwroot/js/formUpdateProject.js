﻿import { clearErrorMessages } from './utils.js'
import { getPreviewImagePath } from './imageHandler.js'
import { selectedItems } from './compact-search.js'
import { quillInstances } from './initQuill.js'

document.addEventListener('DOMContentLoaded', () => {

    const modal = document.querySelector('#editProjectModal')
    const form = modal.querySelector('form')

    form.addEventListener('submit', async (e) => {
        e.preventDefault()
        clearErrorMessages(form)

        const instance = quillInstances.find(x => x.form === form)
        if (instance) {
            const input = form.querySelector('.content-input')
            input.value = instance.quill.root.innerHTML
        }
        const formData = new FormData(form)
        const fileInput = form.querySelector('input[type="file"]')
        if (!fileInput.files.length) {
            const imagePath = getPreviewImagePath(form)
            if (imagePath) {
                formData.append('ProjectImage', imagePath)
            }
        }
        var test = form.querySelector('#clients')
        selectedItems.forEach(item => {
            formData.append(`MembersId`, item.id)
        })

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
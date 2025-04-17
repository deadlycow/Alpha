import { clearErrorMessages } from './utils.js'

document.addEventListener('DOMContentLoaded', () => {

    const forms = document.querySelectorAll('#auth-form');

    forms.forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault()
            //clearErrorMessages(form)
            const formData = new FormData(form)

            try {
                const res = await fetch(form.action, {
                    method: 'post',
                    body: formData,
                })

                //if (res.status === 400) {
                //    const data = await res.json()

                //    if (data.errors) {
                //        Object.keys(data.errors).forEach(key => {
                //            let input = form.querySelector(`[name="${key}"]`)
                //            if (input) {
                //                input.classList.add('input-validation-error')
                //            }

                //            let span = form.querySelector(`[data-valmsg-for="${key}"]`)
                //            if (span) {
                //                span.innerText = data.errors[key].join('\n')
                //                span.classList.add('field-validation-error')
                //            }
                //        })
                //    }
                //}
            }
            catch {
                console.log('error submitting the auth-form')
            }
        })
    })
})